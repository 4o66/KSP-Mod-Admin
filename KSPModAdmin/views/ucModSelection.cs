using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FolderSelect;
using KSPModAdmin.Utils;
using KSPModAdmin.Utils.CommonTools;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using System.Net;

namespace KSPModAdmin.Views
{
    /// <summary>
    /// The mod selection view.
    /// </summary>
    public partial class ucModSelection : ucBase
    {
        #region Members

        /// <summary>
        /// The backgroud worker.
        /// </summary>
        AsyncTask<bool> m_RunningTask = null;

        /// <summary>
        /// Flag to determine if we already processing.
        /// </summary>
        bool m_Processing = false;

        /// <summary>
        /// Count of the processed nodes.
        /// </summary>
        int m_ProcessedNodesCount = 0;

        /// <summary>
        /// Holds all not processd directories to delete.
        /// </summary>
        List<TreeNodeMod> m_NotDeletedDirs = new List<TreeNodeMod>();

        /// <summary>
        /// A instance of the search dialog.
        /// </summary>
        frmSearchDLG m_SearchDLG = null;

        /// <summary>
        /// The result of the CheckedChangingDialog.
        /// </summary>
        DialogResult m_CheckedStateDialogResult = DialogResult.None;

        #endregion

        #region Properties

        /// <summary>
        /// Flag to determine if existing files shoud be replaced.
        /// </summary>
        public bool Override
        {
            get { return cbOverride.Checked; }
            set { cbOverride.Checked = value; }
        }

        /// <summary>
        /// The currently selected node.
        /// </summary>
        public TreeNodeMod SelectedNode
        {
            get { return (TreeNodeMod)tvModSelection.FocusedNode; }
            set { tvModSelection.FocusedNode = value; }
            //get { return (TreeNodeMod)tvModSelection.SelectedNode; }
            //set { tvModSelection.SelectedNode = value; }
        }

        /// <summary>
        /// The currently selected nodes.
        /// </summary>
        public List<TreeNodeMod> SelectedNodes
        {
            get
            {
                return tvModSelection.NodesSelection.Cast<TreeNodeMod>().ToList();
            }
        }

        /// <summary>
        /// The currently selected root TreeNode.
        /// </summary>
        public List<TreeNodeMod> SelectedRootNodes
        {
            get
            {
                //foreach (TreeNodeMod node in SelectedNodes)
                //{
                //    TreeNodeMod tempNode = node;
                //    while (tempNode.Parent != null)
                //        tempNode = (TreeNodeMod)tempNode.Parent;

                //    if (!result.Contains((TreeNodeMod)tempNode))
                //        result.Add(tempNode);
                //}

                return SelectedNodes.Select(node => node.ZipRoot).ToList();
            }
        }

        /// <summary>
        /// The root nodes of the mod selection tree.
        /// </summary>
        public TreeNodeMod[] Nodes
        {
            get
            {
                return tvModSelection.Nodes.Cast<TreeNodeMod>().ToArray();

                //TreeNodeMod[] nodes = new TreeNodeMod[tvModSelection.Nodes.Count];
                //for (int i = 0; i < tvModSelection.Nodes.Count; ++i)
                //    nodes[i] = (TreeNodeMod)tvModSelection.Nodes[i];

                //return nodes;
            }
        }

        /// <summary>
        /// Gets or sets the enable states of the mod selection controls.
        /// </summary>
        public bool EnableControls
        {
            get
            {
                return grpModSelection.Enabled;
            }
            set
            {
                grpModSelection.Enabled = value;
                grpProceed.Enabled = value;
                btnLunchKSP.Enabled = value;
                btnScanGameData.Enabled = value;
                cbBorderlessWin.Enabled = value;
            }
        }

        /// <summary>
        /// Flag to determine if the KPS should be started with a BorderLess windows.
        /// </summary>
        public bool BorderlessWin
        {
            get { return cbBorderlessWin.Checked; }
            set { cbBorderlessWin.Checked = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for VS Designer only!
        /// </summary>
        public ucModSelection()
        {
            InitializeComponent();
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Downloads and adds a mod from ksp spaceport.
        /// </summary>
        /// <param name="spaceportURL">URL to the spaceport mod.</param>
        /// <param name="modName">Name of the mod (leave blank for auto fill).</param>
        /// <param name="installAfterAdd">Flag that determines if the mod should be installed after adding to the ModSelection.</param>
        /// <returns>The new added mod (maybe null).</returns>
        public TreeNodeMod HandleModAddViaSpaceport(string spaceportURL, string modName = "", bool installAfterAdd = false, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
            ModInfo modInfo = SpaceportHelper.GetModInfo(spaceportURL);
            if (modInfo == null)
                return null;

            if (!string.IsNullOrEmpty(modName))
                modInfo.Name = modName;

            TreeNodeMod newMod = null;
            if (SpaceportHelper.DownloadMod(ref modInfo, downloadProgressHandler))
            {
                List<TreeNodeMod> addedMods = AddModToTreeView(new ModInfo[] { modInfo });
                if (installAfterAdd)
                    ProcessNodes(addedMods.ToArray());

                if (addedMods.Count > 0)
                    newMod = addedMods[0];
            }

            return newMod;
        }

        /// <summary>
        /// Downloads and adds a mod from ksp forum.
        /// </summary>
        /// <param name="kspForumURL">URL to the ksp forum mod.</param>
        /// <param name="modName">Name of the mod (leave blank for auto fill).</param>
        /// <param name="installAfterAdd">Flag that determines if the mod should be installed after adding to the ModSelection.</param>
        /// <returns>The new added mod (maybe null).</returns>
        public TreeNodeMod HandleModAddViaKSPForum(string kspForumURL, string modName = "", bool installAfterAdd = false, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
            ModInfo modInfo = KSPForumHelper.GetModInfo(kspForumURL);
            if (modInfo == null)
                return null;

            if (!string.IsNullOrEmpty(modName))
                modInfo.Name = modName;

            string downloadURL = KSPForumHelper.GetDownloadURL(kspForumURL);

            if (downloadURL == string.Empty)
                return null;

            TreeNodeMod newMod = null;
            if (KSPForumHelper.DownloadMod(downloadURL, ref modInfo, downloadProgressHandler))
            {
                List<TreeNodeMod> addedMods = AddModToTreeView(new ModInfo[] { modInfo });
                if (installAfterAdd)
                    ProcessNodes(addedMods.ToArray());

                if (addedMods.Count > 0)
                    newMod = addedMods[0];
            }

            return newMod;
        }

        /// <summary>
        /// Downloads and adds a mod from CurseForge.
        /// </summary>
        /// <param name="curseForgeURL">URL to the CurseForge mod.</param>
        /// <param name="modName">Name of the mod (leave blank for auto fill).</param>
        /// <param name="installAfterAdd">Flag that determines if the mod should be installed after adding to the ModSelection.</param>
        /// <returns>The new added mod (maybe null).</returns>
        public TreeNodeMod HandleModAddViaCurseForge(string curseForgeURL, string modName = "", bool installAfterAdd = false, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
            curseForgeURL = CurseForgeHelper.GetCurseForgeModURL(curseForgeURL);

            ModInfo modInfo = CurseForgeHelper.GetModInfo(curseForgeURL);
            if (modInfo == null)
                return null;

            if (!string.IsNullOrEmpty(modName))
                modInfo.Name = modName;

            TreeNodeMod newMod = null;
            if (CurseForgeHelper.DownloadMod(ref modInfo, downloadProgressHandler))
            {
                List<TreeNodeMod> addedMods = AddModToTreeView(new ModInfo[] { modInfo });
                if (installAfterAdd)
                    ProcessNodes(addedMods.ToArray());

                if (addedMods.Count > 0)
                    newMod = addedMods[0];
            }

            return newMod;
        }

        public TreeNodeMod HandleModAddViaKerbalStuff(string kerbalStuffURL, string modName = "", bool installAfterAdd = false, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
            kerbalStuffURL = KerbalStuffHelper.GetKerbalStuffModURL(kerbalStuffURL);
            
            ModInfo modInfo = KerbalStuffHelper.GetModInfo(kerbalStuffURL);
            if (modInfo == null)
                return null;

            if (!string.IsNullOrEmpty(modName))
                modInfo.Name = modName;

            TreeNodeMod newMod = null;
            if (KerbalStuffHelper.DownloadMod(ref modInfo, downloadProgressHandler))
            {
                List<TreeNodeMod> addedMods = AddModToTreeView(new ModInfo[] { modInfo });
                if (installAfterAdd)
                    ProcessNodes(addedMods.ToArray());

                if (addedMods.Count > 0)
                    newMod = addedMods[0];
            }

            return newMod;
        }

        /// <summary>
        /// Adds a mod from HD.
        /// </summary>
        /// <param name="path">PAth to the mod.</param>
        /// <param name="modName">Name of the mod (leave blank for auto fill).</param>
        /// <param name="installAfterAdd">Flag that determines if the mod should be installed after adding to the ModSelection.</param>
        /// <returns>The new added mod (maybe null).</returns>
        public TreeNodeMod HandleModAddViaPath(string path, string modName = "", bool installAfterAdd = false)
        {
            TreeNodeMod newMod = null;
            List<TreeNodeMod> addedMods = AddModToTreeView(new string[] { path });
            if (addedMods.Count > 0 && !string.IsNullOrEmpty(modName))
                addedMods[0].Text = modName;

            if (installAfterAdd)
                ProcessNodes(addedMods.ToArray());

            if (addedMods.Count > 0)
                newMod = addedMods[0];

            return newMod;
        }

        /// <summary>
        /// Creates nodes from the ModInfos and adds the nodes to the ModSelection.
        /// </summary>
        /// <param name="modInfos">The nodes to add.</param>
        public List<TreeNodeMod> AddMods(ModInfo[] modInfos, bool showCollisionDialog = true)
        {
            return AddModToTreeView(modInfos, showCollisionDialog);
        }

        /// <summary>
        /// Creates nodes from the ModInfos and adds the nodes to the ModSelection.
        /// </summary>
        /// <param name="modInfos">The nodes to add.</param>
        public void AddModsAsync(ModInfo[] modInfos, bool showCollisionDialog = true)
        {
            AddModToTreeViewAsync(modInfos, showCollisionDialog);
        }

        /// <summary>
        /// Adds MOD(s) to the TreeView.
        /// </summary>
        /// <param name="fileNames">Paths to the Zip-Files of the KSP mods.</param>
        public List<TreeNodeMod> AddMods(string[] fileNames, bool showCollisionDialog = true)
        {
            return AddModToTreeView(fileNames, showCollisionDialog);
        }

        /// <summary>
        /// Adds MOD(s) to the TreeView.
        /// </summary>
        /// <param name="fileNames">Paths to the Zip-Files of the KSP mods.</param>
        public void AddModsAsync(string[] fileNames, bool showCollisionDialog = true)
        {
            AddModToTreeViewAsync(fileNames, showCollisionDialog);
        }

        /// <summary>
        /// Adds the nodes to the mod selection tree.
        /// </summary>
        /// <param name="nodes">The nodes to add.</param>
        public void AddNodes(TreeNodeMod[] nodes)
        {
            foreach (TreeNodeMod node in nodes)
            {
                tvModSelection.Nodes.Add(node);
                SetToolTips(node);
                CheckNodesWithDestination(node);
            }
        }

        /// <summary>
        /// Removes the first node with the bassed text from the mod selection tree.
        /// </summary>
        /// <param name="nodeText">The text of the node to remove.</param>
        public void RemoveNodeByNodeText(string nodeText)
        {
            TreeNodeMod result = null;
            foreach (TreeNodeMod node in tvModSelection.Nodes)
            {
                result = SearchNode(nodeText, node);
                if (result != null)
                {
                    tvModSelection.ChangeCheckedState(result, false);
                    result.NodeType = NodeType.UnknownFile;
                    break;
                }
            }
        }

        /// <summary>
        /// Clears the mod selection tree.
        /// </summary>
        public void ClearNodes()
        {
            tvModSelection.Nodes.Clear();
            ModRegister.Clear();
        }

        /// <summary>
        /// Traversing the complete tree and renews the checked state of all nodes.
        /// </summary>
        public void RenewCheckedStateAllNodes()
        {
            foreach (TreeNodeMod node in tvModSelection.Nodes)
                RenewCheckedState(node);

            tvModSelection.Invalidate();
        }

        /// <summary>
        /// Removes all MODs from the TreeView and KSP.
        /// </summary>
        public void RemoveAllMods(bool askUser = true, bool silent = false)
        {
            if (!askUser || DialogResult.Yes == MessageBox.Show(this, Constants.MESSAGE_MOD_DELETE_ALL_QUESTION, "", MessageBoxButtons.YesNo))
            {
                // prepare to deinstall all mods
                foreach (TreeNodeMod childs in tvModSelection.Nodes)
                    tvModSelection.ChangeCheckedState(childs, false, true, true);

                // deinstall all mods
                ProcessSelectionAsync(tvModSelection.Nodes, silent);

                ClearNodes();

                MainForm.SaveKSPConfig();
            }
        }

        /// <summary>
        /// Creates a zip file of the craft and adds it to the ModSelection.
        /// </summary>
        /// <param name="fullpath">Full path of the craft-file.</param>
        public string CreateZipOfCraftFile(string fullpath)
        {
            string zipPath = string.Empty;

            try
            {
                string dir = string.Empty;
                using (StreamReader sr = new StreamReader(fullpath))
                {
                    string line = sr.ReadToEnd();
                    int index = line.IndexOf("type = ");
                    if (index != -1)
                    {
                        string shipType = line.Substring(index + 7, 3);
                        if (shipType.ToLower() == Constants.SPH)
                            dir = "Ships\\SPH\\";
                        else
                            dir = "Ships\\VAB\\";
                    }
                }

                zipPath = Path.Combine(Path.GetDirectoryName(fullpath), Path.GetFileNameWithoutExtension(fullpath) + Constants.EXT_ZIP);
                using (var archive = ZipArchive.Create())
                {
                    archive.AddEntry(Path.Combine(dir, Path.GetFileName(fullpath)), fullpath);
                    archive.SaveTo(zipPath, CompressionType.Deflate);
                }

                try
                {
                    File.Delete(fullpath);
                }
                catch (Exception ex)
                {
                    MainForm.AddError(string.Format("Can't delete. \"{0}\"", fullpath), ex);
                }
            }
            catch (Exception ex)
            {
                MainForm.AddError("ZipFile creation for download faild.", ex);
            }

            return zipPath;
        }

        /// <summary>
        /// Sorts the nodes of the ModSelection depending on the passed SortType.
        /// </summary>
        /// <param name="sortType">Determines the property to use for the sort.</param>
        /// <param name="desc">Determines if the sorting should be descending or ascending.</param>
        public void SortModSelection(SortType sortType = SortType.ByName, bool desc = true)
        {
            List<TreeNodeMod> allNodes = tvModSelection.Nodes.Cast<TreeNodeMod>().ToList();
            switch (sortType)
            {
                case SortType.ByName:
                    SortByName(ref allNodes, desc);
                    break;
                case SortType.ByAddDate:
                    SortByAddDate(ref allNodes, desc);
                    break;
                case SortType.ByState:
                    SortByAddDate(ref allNodes, desc);
                    break;
                case SortType.ByVersion:
                    SortByAddDate(ref allNodes, desc);
                    break;
            }

            ClearNodes();
            foreach (TreeNodeMod node in allNodes)
            {
                ModRegister.RegisterMod(node);
                tvModSelection.Nodes.Add(node);
            }
        }

        /// <summary>
        /// Searches the complete TreeView for a node that display text matches to the search string.
        /// </summary>
        /// <param name="searchText">The search string.</param>
        /// <returns>The first found node or null.</returns>
        public TreeNodeMod SearchNode(string searchText)
        {
            TreeNodeMod result = null;
            foreach (TreeNodeMod node in tvModSelection.Nodes)
            {
                if ((result = SearchNode(searchText, node)) != null)
                    break;
            }

            return result;
        }

        /// <summary>
        /// Splits the filename (at '\') and searches the tree for the Node where name and path matches.
        /// </summary>
        /// <param name="filename">Zip-File path of the node to search for.</param>
        /// <param name="startNode">Node to start the search from.</param>
        /// <param name="pathSeperator">Seperator of the path in filename.</param>
        /// <returns>The matching TreeNodeMod.</returns>
        public TreeNodeMod SearchNodeByPath(string filename, TreeNodeMod startNode, char pathSeperator)
        {
            string[] dirs = filename.Split(pathSeperator);

            TreeNodeMod result = null;
            foreach (string dir in dirs)
            {
                startNode = SearchNode(dir, startNode);

                if (startNode == null)
                    break;
                else
                    result = startNode;
            }

            if (result != null && result.FullPath.Contains(filename))
                return result;
            else
                return null;
        }

        /// <summary>
        /// Returns a list of TreeNodeMod that represents a file entry.
        /// </summary>
        /// <param name="node">The node to start the search from.</param>
        /// <param name="fileNodes">For recursive calls! List of already found file nodes.</param>
        /// <returns>A list of TreeNodeMod that represents a file entry.</returns>
        public List<TreeNodeMod> GetAllFileNodes(TreeNodeMod node, List<TreeNodeMod> fileNodes = null)
        {
            if (fileNodes == null)
                fileNodes = new List<TreeNodeMod>();

            if (node.IsFile)
                fileNodes.Add(node);

            foreach (TreeNodeMod childNode in node.Nodes)
                GetAllFileNodes(childNode, fileNodes);

            return fileNodes;
        }

        /// <summary>
        /// Builds and sets the destination path to the passed node and its childs.
        /// </summary>
        /// <param name="srcNode">Node to set the destination path.</param>
        /// <param name="destPath">The destination path.</param>
        /// <param name="copyContent"></param>
        public void SetDestinationPaths(TreeNodeMod srcNode, string destPath, bool copyContent = false)
        {
            if (!copyContent)
            {
                //if (srcNode.Text.ToLower() == Constants.GAMEDATA)
                //    srcNode.Destination = destPath;
                //else
                srcNode.Destination = (destPath != string.Empty) ? Path.Combine(destPath, srcNode.Text) : "";

                destPath = (srcNode.Destination != string.Empty) ? srcNode.Destination : string.Empty;
            }

            SetToolTips(srcNode);

            foreach (TreeNodeMod child in srcNode.Nodes)
                SetDestinationPaths(child, destPath);
        }

        /// <summary>
        /// Sets the tooltip text of the node and all its childs.
        /// </summary>
        /// <param name="node">The node to set the tooltip to.</param>
        public void SetToolTips(TreeNodeMod node)
        {
            // TODO:!!!
            //if (node.Destination != string.Empty)
            //    node.ToolTipText = node.Destination.ToLower().Replace(MainForm.GetPath(KSP_Paths.KSP_Root).ToLower(), "KSP install folder");
            //else
            //    node.ToolTipText = "<No path selected>";

            //foreach (TreeNodeMod child in node.Nodes)
            //    SetToolTips(child);
        }

        #endregion

        #region Private

        #region Event handling

        /// <summary>
        /// Handels the Load event of the ucModSelection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucModSelection_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            tvModSelection.AddColumn("name", "Mod/Subfolder/File", 220, 220);
            tvModSelection.AddColumn("addDate", "AddDate", 60, 60, true, true);
            tvModSelection.AddColumn("version", "Version", 45, 45, true);
            tvModSelection.AddColumn("notes", "Notes", 220, 220);
            tvModSelection.AddColumn("productID", "ProductID", 60, 60, true, true);
            tvModSelection.AddColumn("creationDate", "CreationDate", 60, 60, true, true);
            tvModSelection.AddColumn("author", "Author", 100, 100, true, true);
            tvModSelection.AddColumn("rating", "Rating", 60, 60, true, true);
            tvModSelection.AddColumn("downloads", "Downloads", 60, 60, true, true);
            tvModSelection.AddColumn("curseForgeURL", "CurseForgeURL", 220, 220, true, true);
            tvModSelection.AddColumn("forumURL", "ForumURL", 220, 220, true, true);
            tvModSelection.AddActionKey(VirtualKey.VK_DELETE, HandleDeleteKey, null, true);
            tvModSelection.AddActionKey(VirtualKey.VK_BACK, HandleDeleteKey, null, true);
        }
        
        #region Buttons

        /// <summary>
        /// Handels the Click event of the btnAdd button.
        /// Opens a file dilaog and adds the selected file to the selected Mods tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddMod dlg = new frmAddMod();
            dlg.ShowDialog(this);
            tvModSelection.Invalidate();

            //OpenFileDialog ofDLG = new OpenFileDialog();
            //ofDLG.Filter = Constants.ADD_DLG_FILTER;
            //ofDLG.Multiselect = true;
            //if (!string.IsNullOrEmpty(MainForm.Options.DownloadPath) && Directory.Exists(MainForm.Options.DownloadPath))
            //    ofDLG.InitialDirectory = MainForm.Options.DownloadPath;
            //if (DialogResult.OK == ofDLG.ShowDialog(this))
            //    AddModToTreeViewAsync(ofDLG.FileNames);
        }

        /// <summary>
        /// Handels the Click event of the btnRemove button.
        /// Deletes the selected mod and removes it from the selected mods tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveMod(SelectedRootNodes);
            tvModSelection.Invalidate();
        }

        /// <summary>
        /// Handels the Click event of the btnClear button.
        /// Deletes all installed mods and clears the selected mods tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            RemoveAllMods();
            tvModSelection.Invalidate();
        }

        /// <summary>
        /// Handels the Click event of the btnImExport button.
        /// Opens the Im/Export dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImExport_Click(object sender, EventArgs e)
        {
            frmImExport dlg = new frmImExport();
            dlg.ShowDialog(this);
        }

        /// <summary>
        /// Handels the Click event of the btnScanGameData button.
        /// Starts the scan of the KSP gameData folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScanGameData_Click(object sender, EventArgs e)
        {
            ScanGameData();
            tvModSelection.Invalidate();
        }

        /// <summary>
        /// Handels the Click event of the deInstallMod ToolStripMenuItem.
        /// Processes the selected mod. Installs or removes of parts of the mod depanding on the checked state of the tree nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecuteMod_Click(object sender, EventArgs e)
        {
            if (m_Processing) return;
            m_Processing = true;

            if (SelectedNode != null)
            {
                List<TreeNodeMod> nodesToProceed = SelectedNodes;

                TreeNodeMod lastNode = null;
                List<TreeNodeMod> list = new List<TreeNodeMod>();
                for (int i = 0; i < nodesToProceed.Count; ++i)
                {
                    TreeNodeMod root = nodesToProceed[i].ZipRoot;
                    if (lastNode != nodesToProceed[i] && lastNode != root && !list.Contains(root))
                        list.Add(root);

                    lastNode = root;
                }

                ProcessSelectionAsync(list.ToArray());

                // TODO: Save ist hier falsch plaziert!
                MainForm.SaveKSPConfig();
            }
            else
                MessageBox.Show(this, Constants.MESSAGE_SELECT_MOD);

            m_Processing = false;
        }

        /// <summary>
        /// Handels the Click event of the btnExecute button.
        /// Processes every mod in the selected mod tree. Installs or removes of parts of these mods depanding on the checked state of the tree nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecuteAllMods_Click(object sender, EventArgs e)
        {
            if (m_Processing) return;
            m_Processing = true;

            ProcessSelectionAsync(tvModSelection.Nodes);

            // TODO: Save ist hier falsch plaziert!
            MainForm.SaveKSPConfig();
            //MainForm.SaveKSPConfig();

            m_Processing = false;
        }

        /// <summary>
        /// Handels the Click event of the btnLunchKSP button.
        /// Starts KSP.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLunchKSP_Click(object sender, EventArgs e)
        {
            grpModSelection.Enabled = false;
            grpProceed.Enabled = false;

            string fullpath = MainForm.GetPath(KSP_Paths.KSPExe);
            string fullpath64 = MainForm.GetPath(KSP_Paths.KSP64Exe);
            try
            {
                if (File.Exists(fullpath64))
                    fullpath = fullpath64;

                if (File.Exists(fullpath))
                {
                    System.Diagnostics.Process kspexe = new System.Diagnostics.Process();
                    kspexe.StartInfo.FileName = fullpath;
                    kspexe.StartInfo.WorkingDirectory = Path.GetDirectoryName(fullpath);
                    if (cbBorderlessWin.Checked)
                        kspexe.StartInfo.Arguments = "-popupwindow";
                    kspexe.Start();

                    if (MainForm.Options.BackupOnKSPLaunch)
                        MainForm.Backup.KSPLaunchBackup();
                }
            }
            catch (Exception ex)
            {
                MainForm.AddError("Launching KSP faild.", ex);
            }

            grpModSelection.Enabled = true;
            grpProceed.Enabled = true;
        }

        #endregion

        #region tvModSelection

        /// <summary>
        /// Handles the Click event of the tvSelectMODs TreeView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvModSelection_Click(object sender, EventArgs e)
        {
            m_CheckedStateDialogResult = DialogResult.None;
        }

        /// <summary>
        /// Handles the DoubleClick event of the tvSelectMODs TreeView.
        /// (Trys to oben and display the doubleclicked file).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvModSelection_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            m_CheckedStateDialogResult = DialogResult.None;

            HitInfo hitInfo = tvModSelection.CalcColumnHit(args.Location);
            if (hitInfo != null && hitInfo.Column != null)
            {
                switch (hitInfo.Column.Index)
                {
                    case 0: // Mod/SubFolder/File
                        if (SelectedNode != null && !TryOpenFile(SelectedNode))
                            SelectedNode.Expanded = !SelectedNode.Expanded;
                        break;
                    case 1: // AddDate
                        if (SelectedNode != null)
                            SelectedNode.Expanded = !SelectedNode.Expanded;
                        break;
                    case 2: // Version
                    case 3: // Note
                        tvModSelection.OpenEditorAtLocation(args.Location);
                        break;
                }
            }
        }

        /// <summary>
        /// Handels the DragEnter event of the tvSelectedMODs.
        /// Identifies the draged object and sets the DragDropEffect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvModSelection_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];

                foreach (string filename in filenames)
                {
                    string ext = System.IO.Path.GetExtension(filename).ToLower();
                    if (ext == Constants.EXT_ZIP || ext == Constants.EXT_RAR || ext == Constants.EXT_7ZIP || ext == Constants.EXT_CRAFT)
                    {
                        e.Effect = DragDropEffects.Copy; // Okay
                        break;
                    }
                }
            }
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        /// <summary>
        /// Handels the DragDrop event of the tvSelectedMODs.
        /// Adds the droped archive file to the selected mods tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvModSelection_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                List<string> accepted = new List<string>();
                foreach (string filename in filenames)
                {
                    string ext = System.IO.Path.GetExtension(filename).ToLower();
                    if (ext == Constants.EXT_ZIP || ext == Constants.EXT_RAR || ext == Constants.EXT_7ZIP || ext == Constants.EXT_CRAFT)
                        accepted.Add(filename);
                }

                if (accepted.Count > 0)
                    AddModToTreeViewAsync(accepted.ToArray());
            }
        }

        /// <summary>
        /// Handels the DrawNode event of the tvModSelection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvModSelection_CustomDrawCell(object o, CustomDrawCellEventArgs e)
        {
            TreeNodeMod node = (TreeNodeMod)e.Node;

            Color textColor = MainForm.Options.ColorDestinationDetected; // SystemColors.WindowText;
            if (e.Column.Index == 0)
            {
                if (node.IsInstalled || node.HasInstalledChilds)
                {
                    if (node.Text.ToLower() == Constants.GAMEDATA.ToLower())
                    {
                        if (node.HasInstalledChilds)
                            textColor = MainForm.Options.ColorModInstalled;
                    }
                    else
                        textColor = MainForm.Options.ColorModInstalled;
                }
                else if (!node.HasDestination && !node.HasDestinationForChilds)
                    textColor = MainForm.Options.ColorDestinationMissing; //Color.FromArgb(130, 130, 130);
                if (node.IsOutdated)
                    textColor = MainForm.Options.ColorModOutdated; // Color.FromArgb(0, 0, 200);
                if (!node.ZipExists)
                    textColor = MainForm.Options.ColorModArchiveMissing; // Color.FromArgb(255, 0, 0);
                if (node.HasChildCollision)
                    textColor = MainForm.Options.ColorDestinationConflict; // Color.Orange;
                //if (e.Selected)
                //    textColor = SystemColors.HighlightText;
            }

            TextRenderer.DrawText(e.Graphics,
                                  (string)node.Data[e.Column.Index],
                                  tvModSelection.Font,
                                  e.Bounds,
                                  textColor,
                                  Color.Empty,
                                  TextFormatFlags.VerticalCenter);
        }

        /// <summary>
        /// Handels the CheckedStateChanging event of the tvModSelection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>Returns a value that determines if the changing should be continued (true) or cancled (false).</returns>
        private bool tvModSelection_CheckedChanging(object o, CheckedChangingEventArgs e)
        {
            TreeNodeMod node = (TreeNodeMod)e.Node;
            if (e.NewCheckedState)
            {
                if (!node.ZipExists && m_CheckedStateDialogResult != DialogResult.Ignore)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("The Zip file of this mod is missing.");
                    sb.AppendLine("Checking of this mod isn't possible cause there is no Arichve to install from.");
                    MessageBox.Show(this, sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_CheckedStateDialogResult = DialogResult.Ignore;
                    return false;
                }

                else if (!node.HasDestination && !node.HasDestinationForChilds)
                {
                    if (SelectDestFolder(node))
                    {
                        tvModSelection.ChangeCheckedState(node, node.Checked, true, true);
                        tvModSelection.Invalidate();
                    }
                    else
                        return false;
                }

                return (node.HasDestination || node.HasDestinationForChilds);
            }
            else
            {
                if (!node.ZipExists && m_CheckedStateDialogResult == DialogResult.None)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("You are unchecking a mod part where the ZipArchiv is missing.");
                    sb.AppendLine("Do you want to make a ZipArchiv of this mod?");
                    sb.AppendLine();
                    sb.AppendLine("Yes    - Creates Zip of the mod and saves it to the downloads folder.");
                    sb.AppendLine("No     - Proceed with unchecking (Mod part will be lost!).");
                    sb.AppendLine("Cancel - Abort unchecking.");
                    m_CheckedStateDialogResult = MessageBox.Show(MainForm, sb.ToString(), "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                    switch (m_CheckedStateDialogResult)
                    {
                        case DialogResult.Yes:
                            CreateZip(new List<TreeNodeMod>(new TreeNodeMod[] { node }));
                            break;

                        case DialogResult.No:
                            break;

                        case DialogResult.Cancel:
                            return false;
                    }
                }

                return true;
            }
        }

        #endregion

        #region ContextMenu

        /// <summary>
        /// Handels the Opening event of the ctxtMenuModSelection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxtMenuModSelection_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tvModSelection.ReadOnly) return;

            Point cPos = tvModSelection.PointToClient(System.Windows.Forms.Cursor.Position);
            TreeNodeMod node = (TreeNodeMod)tvModSelection.CalcHitNode(cPos);
            if (node == null)
            {
                //e.Cancel = true;
                tsmiMod_Destination.Enabled = true;
                tsmiMod_Path.Enabled = false;
                tsmiMod_Path.Text = Constants.DEFAULT_TEXT_DESTINATION_PATH_CONTEXTMENU;
                tsmiMod_SelectDestination.Enabled = false;
                tsmiMod_ResetDestination.Enabled = false;

                tsmiMod_ZipSource.Enabled = true;
                tsmiMod_SourcePath.Enabled = false;
                tsmiMod_SelectNewSourceFolder.Enabled = false;
                tsmiMod_SelectNewSourceFolderAllMods.Enabled = true;
                tsmiMod_CreateZipOfInstalledItems.Enabled = false;

                tsmiMod_EditModInfos.Enabled = false;
                tsmiMod_EditModInfos.Enabled = false;
                tsmiMod_CopyModInfos.Enabled = false;

                tsmiMod_SolveConflicts.Enabled = false;

                tsmiMod_VisitSpaceport.Enabled = false;
                tsmiMod_VisitSpaceport.Enabled = false;
                tsmiMod_VisitForum.Enabled = false;

                tsmiMod_ProceedSelectedMod.Enabled = false;

                tsmiMod_ExpandAllNodes.Enabled = true;
                tsmiMod_CollapsNodes.Enabled = true;

                tsmiMod_CheckAllMods.Enabled = true;
                tsmiMod_UncheckAllMods.Enabled = true;

                tsmiMod_Search.Enabled = false;
            }
            else
            {
                tsmiMod_Destination.Enabled = true;
                tsmiMod_Path.Enabled = false;
                if (node.HasDestination)
                {
                    string install = MainForm.GetPath(KSP_Paths.KSPRoot).ToLower();
                    string dest = node.Destination.ToLower().Replace(install, "KSP install folder");
                    tsmiMod_Path.Text = dest;
                }
                else
                    tsmiMod_Path.Text = Constants.DEFAULT_TEXT_DESTINATION_PATH_CONTEXTMENU;
                tsmiMod_SelectDestination.Enabled = true;
                tsmiMod_ResetDestination.Enabled = true;

                tsmiMod_ZipSource.Enabled = true;
                tsmiMod_SourcePath.Enabled = false;
                if (File.Exists(node.ZipRoot.Name))
                    tsmiMod_SourcePath.Text = Path.GetDirectoryName(node.ZipRoot.Name);
                else
                    tsmiMod_SourcePath.Text = "<Zip not found>";
                tsmiMod_DefaultName.Enabled = true;
                tsmiMod_chooseName.Enabled = true;
                tsmiMod_SelectNewSourceFolder.Enabled = true;
                tsmiMod_SelectNewSourceFolderAllMods.Enabled = true;

                tsmiMod_EditModInfos.Enabled = true;
                tsmiMod_CopyModInfos.Enabled = true;

                tsmiMod_SolveConflicts.Enabled = node.ZipRoot.HasChildCollision;

                tsmiMod_VisitSpaceport.Enabled = !string.IsNullOrEmpty(node.ZipRoot.SpaceportURL);
                tsmiMod_VisitForum.Enabled = !string.IsNullOrEmpty(node.ZipRoot.ForumURL);
                tsmiMod_VisitCurseForge.Enabled = !string.IsNullOrEmpty(node.ZipRoot.CurseForgeURL);

                if ((node.HasDestination || node.HasDestinationForChilds) && (!tvModSelection.ReadOnly))
                    tsmiMod_ProceedSelectedMod.Enabled = true;
                else
                    tsmiMod_ProceedSelectedMod.Enabled = false;

                tsmiMod_ExpandAllNodes.Enabled = true;
                tsmiMod_CollapsNodes.Enabled = true;

                tsmiMod_CheckAllMods.Enabled = true;
                tsmiMod_UncheckAllMods.Enabled = true;

                tsmiMod_Search.Enabled = true;
            }
        }

        /// <summary>
        /// Handels the Click event of the selectDestToolStripMenuItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_SelectDestination_Click(object sender, EventArgs e)
        {
            TreeNodeMod node = SelectedNode;
            if (node != null)
            {
                if (node.IsInstalled || node.HasInstalledChilds)
                    MessageBox.Show(ParentForm, "The folder or file is installed.\r\nUnistall it befor select a new destination.", "Attantion!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    SelectDestFolder(node);
            }
        }

        /// <summary>
        /// Handels the Click event of the resetDestinationToolStripMenuItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_ResetDestination_Click(object sender, EventArgs e)
        {
            TreeNodeMod node = SelectedNode;
            if (node != null)
            {
                if (node.IsInstalled || node.HasInstalledChilds)
                    MessageBox.Show(ParentForm, "The folder or file is installed.\r\nUnistall it befor reset the destination.", "Attantion!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    SetDestinationPaths(node, "");
            }
        }

        /// <summary>
        /// Handels the Click event of the selectNewSourceFolderToolStripMenuItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_SelectNewSourceFolder_Click(object sender, EventArgs e)
        {
            TreeNodeMod node = SelectedNode;
            if (node != null)
                node = node.ZipRoot;
            if (node != null)
                SelectNewZipSourceFolder(node);
        }

        /// <summary>
        /// Handels the Click event of the selectNewSourceFolderForAllModsToolStripMenuItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_SelectNewSourceFolderForAllMods_Click(object sender, EventArgs e)
        {
            SelectNewZipSourceFolder(null);
        }

        /// <summary>
        /// Opens the install dir of the mod if it exists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_OpenModFolder_Click(object sender, EventArgs e)
        {
            OpenSelectedModFolder();
        }

        /// <summary>
        /// Opens the copy mod info dialog, and copy the selected mod to the choosen mod from the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_CopyModInfos_Click(object sender, EventArgs e)
        {
            var mods = tvModSelection.SelectedRootNodes;
            if (mods.Count > 0)
                CopyModInfos(((TreeNodeMod)mods[0]).ZipRoot);
        }

        /// <summary>
        /// Opens the ModInfo editor for the selected mod.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_EditModInfos_Click(object sender, EventArgs e)
        {
            var mods = tvModSelection.SelectedRootNodes;
            if (mods.Count > 0)
                EditModInfos(((TreeNodeMod)mods[0]).ZipRoot);
        }

        /// <summary>
        /// Opens the Conflict solving dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_SolveConflicts_Click(object sender, EventArgs e)
        {
            var mods = tvModSelection.SelectedRootNodes;
            if (mods.Count > 0)
            {
                TreeNodeMod mod = (TreeNodeMod) mods[0];
                if (mod != null && mod.HasChildCollision)
                {
                    frmCollisionSolving dlg = new frmCollisionSolving();
                    dlg.CollisionMod = mod;
                    dlg.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Jumps to the ModBrowser tab and opens the download site of the mod (if known).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_VisitSpaceport_Click(object sender, EventArgs e)
        {
            var mods = tvModSelection.SelectedRootNodes;
            if (mods.Count > 0 && !string.IsNullOrEmpty(((TreeNodeMod)mods[0]).ZipRoot.SpaceportURL))
                VisitURL(((TreeNodeMod)mods[0]).ZipRoot.SpaceportURL);
        }

        /// <summary>
        /// Jumps to the ModBrowser tab and opens the Forum site of the mod (if known).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_VistForum_Click(object sender, EventArgs e)
        {
            var mods = tvModSelection.SelectedRootNodes;
            if (mods.Count > 0 && !string.IsNullOrEmpty(((TreeNodeMod)mods[0]).ZipRoot.ForumURL))
                VisitURL(((TreeNodeMod)mods[0]).ZipRoot.ForumURL);
        }

        /// <summary>
        /// Jumps to the ModBrowser tab and opens the CurseForge site of the mod (if known).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_VistCurseForge_Click(object sender, EventArgs e)
        {
            var mods = tvModSelection.SelectedRootNodes;
            if (mods.Count > 0 && !string.IsNullOrEmpty(((TreeNodeMod)mods[0]).ZipRoot.CurseForgeURL))
                VisitURL(((TreeNodeMod)mods[0]).ZipRoot.CurseForgeURL);
        }

        /// <summary>
        /// Checks fot updates for the selected mod.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_CheckForModUpdate_Click(object sender, EventArgs e)
        {
            TreeNodeMod mod = (TreeNodeMod)tvModSelection.FocusedNode;
            if (mod == null) 
                return;

            mod = mod.ZipRoot;
            if (!string.IsNullOrEmpty(mod.SpaceportURL) || !string.IsNullOrEmpty(mod.ForumURL) || !string.IsNullOrEmpty(mod.CurseForgeURL))
                MainForm.Options.Check4ModUpdats(new TreeNodeMod[] { mod }, true);
            else
                MainForm.AddInfo(string.Format("No URL found for mod \"{0}\" - Update check canceled.", mod.Text));
        }

        /// <summary>
        /// Handels the Click event of the collapsNodesToolStripMenuItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_CollapsNodes_Click(object sender, EventArgs e)
        {
            foreach (TreeNodeMod node in tvModSelection.Nodes)
            {
                if (node != null)
                    node.ZipRoot.Collapse();
                else
                    foreach (TreeNodeMod root in tvModSelection.Nodes)
                        root.Collapse();
            }
        }

        /// <summary>
        /// Handels the Click event of the expandAllNodesToolStripMenuItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_ExpandAllNodes_Click(object sender, EventArgs e)
        {
            foreach (TreeNodeMod node in tvModSelection.Nodes)
            {
                if (node != null)
                    node.ZipRoot.ExpandAll();
                else
                    foreach (TreeNodeMod root in tvModSelection.Nodes)
                        root.ExpandAll();
            }
        }

        /// <summary>
        /// Handels the Click event of the selectAllModsToolStripMenuItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_SelectAllMods_Click(object sender, EventArgs e)
        {
            foreach (TreeNodeMod node in tvModSelection.Nodes)
                if (node.HasDestination || node.HasDestinationForChilds) tvModSelection.ChangeCheckedState(node, true);

            tvModSelection.Invalidate();
        }

        /// <summary>
        /// Handels the Click event of the unselectAllModsToolStripMenuItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_UnselectAllMods_Click(object sender, EventArgs e)
        {
            foreach (TreeNodeMod node in tvModSelection.Nodes)
                tvModSelection.ChangeCheckedState(node, false);

            tvModSelection.Invalidate();
        }

        /// <summary>
        /// Handels the Click event of the tsmiMod_search
        /// Opens the search dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_Search_Click(object sender, EventArgs e)
        {
            if (m_SearchDLG != null)
                m_SearchDLG.Close();

            m_SearchDLG = new frmSearchDLG(MainForm);
            int leftDist = (Width - m_SearchDLG.Width) / 2;
            int x = Location.X + leftDist;
            int y = Location.Y + 20;
            m_SearchDLG.Show(this);
            m_SearchDLG.Location = new Point(x, y);
        }

        /// <summary>
        /// Handels the Click event of the tsmiMod_createZipOfInstalledItems.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_CreateZipOfInstalledItems_Click(object sender, EventArgs e)
        {
            if (m_Processing) return;
            m_Processing = true;
            CreateZip();
            m_Processing = false;
        }

        /// <summary>
        /// Handels the Click event of the tsmiMod_chooseName.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_ChooseName_Click(object sender, EventArgs e)
        {
            if (SelectedNode == null || SelectedNode.ZipRoot == null) return;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = MainForm.Options.DownloadPath;
            dlg.CheckFileExists = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int nodeCount = GetFullNodeCount(new TreeNodeMod[] { SelectedNode.ZipRoot });

                // disable controls
                btnAdd.Enabled = false;
                btnRemove.Enabled = false;
                btnClear.Enabled = false;
                btnScanGameData.Enabled = false;
                grpProceed.Enabled = false;
                tvModSelection.ReadOnly = true;
                pBarModProcessing.Visible = true;

                m_RunningTask = new AsyncTask<bool>();
                m_RunningTask.SetCallbackFunctions(
                    delegate()
                    {
                        try
                        {
                            TreeNodeMod root = SelectedNode.ZipRoot;
                            CreateZip(dlg.FileName, root);
                            root.Name = dlg.FileName;
                            MainForm.AddInfo(string.Format("Zip \"{0}\" created. ", root.Name));
                            tvModSelection.Invalidate();
                        }
                        catch (Exception ex)
                        {
                            InvokeIfRequired(() => MessageBox.Show(MainForm, "Ceating of zip failed. " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error));
                            return false;
                        }

                        return true;
                    },
                    delegate(bool b, Exception ex)
                    {
                        btnAdd.Enabled = true;
                        btnRemove.Enabled = true;
                        btnClear.Enabled = true;
                        btnScanGameData.Enabled = true;
                        grpProceed.Enabled = true;
                        tvModSelection.ReadOnly = false;
                        pBarModProcessing.Visible = false;
                    },
                    delegate(int processedNodeCount)
                    {
                        pBarModProcessing.Maximum = nodeCount;
                        if (processedNodeCount != -1 && processedNodeCount <= nodeCount)
                            pBarModProcessing.Value = processedNodeCount;
                    });
                m_RunningTask.Run();
            }
        }

        /// <summary>
        /// Handels the Click event of the tsmiMod_ResetCheckedStates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMod_ResetCheckedStates_Click(object sender, EventArgs e)
        {
            RenewCheckedStateAllNodes();
            tvModSelection.Invalidate();
        }

        ///// <summary>
        ///// Handels the Click event of the tsmiMod_SortByName.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void tsmiMod_SortByName_Click(object sender, EventArgs e)
        //{
        //    //SortModSelection(SortType.ByName);
        //}

        ///// <summary>
        ///// Handels the Click event of the tsmiMod_SortByAddDate.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void tsmiMod_SortByAddDate_Click(object sender, EventArgs e)
        //{
        //    //SortModSelection(SortType.ByAddDate);
        //}

        ///// <summary>
        ///// Handels the Click event of the tsmiMod_SortByState.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void tsmiMod_SortByState_Click(object sender, EventArgs e)
        //{
        //    //SortModSelection(SortType.ByState);
        //}

        ///// <summary>
        ///// Handels the Click event of the tsmiMod_SortByKSPVersion.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void tsmiMod_SortByVersion_Click(object sender, EventArgs e)
        //{
        //    //SortModSelection(SortType.ByVersion);
        //}

        #endregion

        #endregion

        #region Add Mod to TreeView

        /// <summary>
        /// Adds a MOD to the TreeView.
        /// </summary>
        /// <param name="fileNames">Paths to the Zip-Files of the KSP mods.</param>
        private List<TreeNodeMod> AddModToTreeView(string[] fileNames, bool showCollisionDialog = true)
        {
            if (fileNames.Length > 0)
            {
                ModInfo[] modInfos = new ModInfo[fileNames.Length];
                for (int i = 0; i < fileNames.Length; ++i)
                    modInfos[i] = new ModInfo(fileNames[i]);

                return AddModToTreeView(modInfos, showCollisionDialog);
            }
            else
            {
                MainForm.AddError("Add mod(s) failed! Parameter \"filenames\" is empty.");
            }

            return new List<TreeNodeMod>();
        }

        /// <summary>
        /// Adds a MOD to the TreeView.
        /// </summary>
        /// <param name="fileNames">Paths to the Zip-Files of the KSP mods.</param>
        private void AddModToTreeViewAsync(string[] fileNames, bool showCollisionDialog = true)
        {
            if (fileNames.Length > 0)
            {
                ModInfo[] modInfos = new ModInfo[fileNames.Length];
                for (int i = 0; i < fileNames.Length; ++i)
                    modInfos[i] = new ModInfo(fileNames[i]);

                AddModToTreeViewAsync(modInfos, showCollisionDialog);
            }
            else
            {
                MainForm.AddError("Add mod(s) failed! Parameter \"filenames\" is empty.");
            }
        }

        /// <summary>
        /// Adds a MOD to the TreeView.
        /// </summary>
        /// <param name="modInfos">Paths to the Zip-Files of the KSP mods.</param>
        private List<TreeNodeMod> AddModToTreeView(ModInfo[] modInfos, bool showCollisionDialog = true)
        {
            List<TreeNodeMod> addedMods = new List<TreeNodeMod>();
            if (modInfos.Length <= 0)
            {
                MainForm.AddError("Add mod(s) failed! Parameter \"modInfos\" is empty.");
                return addedMods;
            }

            InvokeIfRequired(() =>
            {
                btnAdd.Enabled = false;
                btnRemove.Enabled = false;
                btnClear.Enabled = false;
                grpProceed.Enabled = false;
                tvModSelection.ReadOnly = true;
                pBarModProcessing.Value = m_ProcessedNodesCount = 0;
                pBarModProcessing.Maximum = modInfos.Length;
                pBarModProcessing.Visible = true;
            });

            try
            {
                foreach (ModInfo modInfo in modInfos)
                {
                    // already added?
                    TreeNodeMod newNode = null;
                    TreeNodeMod mod = (string.IsNullOrEmpty(modInfo.ProductID)) ? null : GetModByProductID(modInfo.ProductID, modInfo.VersionControl);
                    if (mod == null && !tvModSelection.Nodes.ContainsKey(modInfo.LocalPath))
                    {
                        newNode = AddNewMod(modInfo);

                        InvokeIfRequired(() =>
                        {
                            if (MainForm.Instance.Options.ShowConflictSolver && showCollisionDialog && newNode != null && newNode.HasChildCollision)
                            {
                                frmCollisionSolving dlg = new frmCollisionSolving {CollisionMod = newNode};
                                dlg.ShowDialog();
                            }
                        });
                    }
                    else if (mod != null && (mod.IsOutdated || modInfo.CreationDateAsDateTime > mod.CreationDateAsDateTime) &&
                                MainForm.Options.ModUpdateBehavior != ModUpdateBehavior.Manualy)
                    {
                        newNode = UpdateMod(modInfo, mod);
                    }
                    else
                    {
                        InvokeIfRequired(() =>
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine(string.Format(Constants.MESSAGE_MOD_ALREADY_ADDED, modInfo.Name));
                            sb.AppendLine();
                            sb.AppendLine("Should the mod be replaced?");
                            if (MessageBox.Show(MainForm, sb.ToString(), "Attantion!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                TreeNodeMod outdatedMod = (TreeNodeMod)tvModSelection.Nodes[modInfo.LocalPath];
                                MainForm.AddInfo(string.Format("Replacing mod \"{0}\"", outdatedMod.Text));

                                newNode = CreateModTreeNodes(modInfo);
                                RemoveOutdatedAndAddNewMod(outdatedMod, newNode);

                                tvModSelection.ChangeCheckedState(newNode, false, true, true);

                                MainForm.AddInfo(string.Format("Mod \"{0}\" replaced.", newNode.Text));

                                if (MainForm.Instance.Options.ShowConflictSolver && showCollisionDialog && newNode != null && newNode.HasChildCollision)
                                {
                                    frmCollisionSolving dlg = new frmCollisionSolving {CollisionMod = newNode};
                                    dlg.ShowDialog();
                                }
                            }
                        });
                    }

                    if (newNode != null)
                        addedMods.Add(newNode);

                    newNode = null;

                    if (m_RunningTask != null)
                        m_RunningTask.PercentFinished = m_ProcessedNodesCount += 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            
            InvokeIfRequired(() =>
            {
                btnAdd.Enabled = true;
                btnRemove.Enabled = true;
                btnClear.Enabled = true;
                grpProceed.Enabled = true;
                tvModSelection.ReadOnly = false;
                m_RunningTask = null;
                pBarModProcessing.Visible = false;

                MainForm.AddInfo("Done!");
            });

            return addedMods;
        }

        /// <summary>
        /// Adds a MOD to the TreeView.
        /// </summary>
        /// <param name="modInfos">Paths to the Zip-Files of the KSP mods.</param>
        private void AddModToTreeViewAsync(ModInfo[] modInfos, bool showCollisionDialog = true)
        {
            #region OLD CODE

            //if (modInfos.Length <= 0)
            //{
            //    MainForm.AddError("Add mod(s) failed! Parameter \"modInfos\" is empty.");
            //    return;
            //}

            //btnAdd.Enabled = false;
            //btnRemove.Enabled = false;
            //btnClear.Enabled = false;
            //grpProceed.Enabled = false;
            //tvModSelection.ReadOnly = true;
            //pBarModProcessing.Value = m_ProcessedNodesCount = 0;
            //pBarModProcessing.Maximum = modInfos.Length;
            //pBarModProcessing.Visible = true;

            #endregion

            m_RunningTask = new AsyncTask<bool>();
            m_RunningTask.SetCallbackFunctions(delegate()
            {
                AddModToTreeView(modInfos, showCollisionDialog);

                #region OLD CODE

                //foreach (ModInfo modInfo in modInfos)
                //{
                //    // already added?
                //    TreeNodeMod newNode = null;
                //    TreeNodeMod mod = (string.IsNullOrEmpty(modInfo.ProductID)) ? null : GetModByProductID(modInfo.ProductID, modInfo.VersionControl);
                //    if (mod == null && !tvModSelection.Nodes.ContainsKey(modInfo.LocalPath))
                //    {
                //        newNode = AddNewMod(modInfo);

                //        InvokeIfRequired(() =>
                //        {
                //            if (newNode != null && newNode.HasChildCollision)
                //            {
                //                frmCollisionSolving dlg = new frmCollisionSolving();
                //                dlg.CollisionModFile = newNode;
                //                dlg.ShowDialog();
                //            }
                //        });
                //    }
                //    else if (mod != null && (mod.IsOutdated || modInfo.CreationDateAsDateTime > mod.CreationDateAsDateTime) &&
                //             MainForm.Options.ModUpdateBehavior != ModUpdateBehavior.Manualy)
                //    {
                //        newNode = UpdateMod(modInfo, mod);
                //    }
                //    else
                //    {
                //        InvokeIfRequired(() =>
                //        {
                //            StringBuilder sb = new StringBuilder();
                //            sb.AppendLine(string.Format(Constants.MESSAGE_MOD_ALREADY_ADDED, modInfo.Name));
                //            sb.AppendLine();
                //            sb.AppendLine("Should the mod be replaced?");
                //            if (MessageBox.Show(MainForm, sb.ToString(), "Attantion!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //            {
                //                TreeNodeMod outdatedMod = (TreeNodeMod)tvModSelection.Nodes[modInfo.LocalPath];
                //                MainForm.AddInfo(string.Format("Replacing mod \"{0}\"", outdatedMod.Text));

                //                TreeNodeMod newMod = CreateModTreeNodes(modInfo);
                //                RemoveOutdatedAndAddNewMod(outdatedMod, newMod);
                                
                //                tvModSelection.ChangeCheckedState(newMod, false, true, true);
                                
                //                MainForm.AddInfo(string.Format("Mod \"{0}\" replaced.", newMod.Text));

                //                if (newNode != null && newNode.HasChildCollision)
                //                {
                //                    frmCollisionSolving dlg = new frmCollisionSolving();
                //                    dlg.CollisionModFile = newNode;
                //                    dlg.ShowDialog();
                //                }
                //            }
                //        });
                //    }

                //    if (m_RunningTask != null)
                //        m_RunningTask.PercentFinished = m_ProcessedNodesCount += 1;
                //}

                #endregion

                return false;
            },
            delegate(bool result, Exception ex)
            {
                if (ex != null)
                    MessageBox.Show(this, ex.Message, "Error");

                #region OLD CODE

                //btnAdd.Enabled = true;
                //btnRemove.Enabled = true;
                //btnClear.Enabled = true;
                //grpProceed.Enabled = true;
                //tvModSelection.ReadOnly = false;
                //m_RunningTask = null;
                //pBarModProcessing.Visible = false;

                //MainForm.AddInfo("Done!");

                #endregion
            },
            delegate(int percentage) { if (percentage <= pBarModProcessing.Maximum) pBarModProcessing.Value = percentage; });

            m_RunningTask.Run();
        }

        /// <summary>
        /// Returns the TreeNodeMod (ZipRoot) with the passed productID or null.
        /// </summary>
        /// <param name="productID">The productID to search for.</param>
        /// <returns>The TreeNodeMod (ZipRoot) with the passed productID or null.</returns>
        private TreeNodeMod GetModByProductID(string productID, VersionControl versionControl)
        {
            return tvModSelection.Nodes.Cast<TreeNodeMod>().FirstOrDefault(node => node.ProductID == productID && node.VersionControl == versionControl);
        }

        /// <summary>
        /// Updates the mod from the modInfo.
        /// Trys to copy the checked state of mod parts, uninstalls the old mod and installs the new one.
        /// </summary>
        /// <param name="modInfo">The ModeInfo of the new mod.</param>
        /// <param name="outdatedMod">The TreeNodeMod of the outdated mod.</param>
        private TreeNodeMod UpdateMod(ModInfo modInfo, TreeNodeMod outdatedMod)
        {
            TreeNodeMod newMod = null;
            try
            {
                MainForm.AddInfo(string.Format("Updating mod {0}", outdatedMod.Text));
                newMod = CreateModTreeNodes(modInfo);
                if (MainForm.Options.ModUpdateBehavior == ModUpdateBehavior.RemoveAndAdd || (!outdatedMod.IsInstalled && !outdatedMod.HasInstalledChilds))
                {
                    InvokeIfRequired(() =>
                                     {
                                         RemoveOutdatedAndAddNewMod(outdatedMod, newMod);
                                         tvModSelection.ChangeCheckedState(newMod, false, true, true);
                                     });
                }
                else
                {
                    // Find matching file nodes and copy destination from old to new mod.
                    if (TryCopyDestToMatchingNodes(outdatedMod, newMod))
                    {
                        newMod.SpaceportURL = outdatedMod.SpaceportURL;
                        newMod.ForumURL = outdatedMod.ForumURL;
                        newMod.CurseForgeURL = outdatedMod.CurseForgeURL;
                        newMod.Note = outdatedMod.Note;
                        InvokeIfRequired(() =>
                                         {
                                             RemoveOutdatedAndAddNewMod(outdatedMod, newMod);
                                             ProcessNodes(new TreeNodeMod[] { newMod }, true);
                                         });
                    }
                    else
                    {
                        // No match found -> user must handle update.
                        InvokeIfRequired(() => MessageBox.Show(MainForm, string.Format("Updating of the mod \"{0}\" failed.{1}Manualy update required.", outdatedMod.Text, Environment.NewLine)));
                    }
                    
                    InvokeIfRequired(() =>
                                     {
                                         if (MainForm.Instance.Options.ShowConflictSolver && newMod != null && newMod.HasChildCollision)
                                         {
                                             frmCollisionSolving dlg = new frmCollisionSolving();
                                             dlg.CollisionMod = newMod;
                                             dlg.ShowDialog();
                                         }
                                     });
                }

                MainForm.AddInfo(string.Format(Constants.MESSAGE_MOD_UPDATED, newMod.Text));
            }
            catch (Exception ex)
            {
                MainForm.AddError(string.Format(Constants.MESSAGE_MOD_ERROR_WHILE_UPDATING, outdatedMod.Text, ex.Message), ex);
            }

            return newMod;
        }

        /// <summary>
        /// Removes the outdated mode from disk and ModSelection and adds the new mod to the ModSelection.
        /// </summary>
        /// <param name="outdatedMod">The mod to remove from ModSelection and disk.</param>
        /// <param name="newMod">The new mod to add to the ModSelection.</param>
        private void RemoveOutdatedAndAddNewMod(TreeNodeMod outdatedMod, TreeNodeMod newMod)
        {
            MainForm.AddInfo(string.Format("Removing outdated mod \"{0}\"", outdatedMod.Text));
            ModRegister.RemoveRegisteredMod(outdatedMod);
            tvModSelection.ChangeCheckedState(outdatedMod, false, true, true);
            ProcessNodes(new TreeNodeMod[] {outdatedMod}, true);
            DeleteNotProcessedDirectorys(true);
            tvModSelection.Nodes.Remove(outdatedMod);

            MainForm.AddInfo(string.Format("Adding updated mod \"{0}\"", newMod.Text));
            tvModSelection.Nodes.Add(newMod);
        }

        /// <summary>
        /// Tries to find notes in the new mod, that matches to the outdated mod.
        /// If a matching node was found the destination and/or the checked state of the node will be copied.
        /// </summary>
        /// <param name="outdatedMod">The outdated mod.</param>
        /// <param name="newMod">The new (updated) mod.</param>
        /// <returns>True if matching files where found, otherwise false.</returns>
        private bool TryCopyDestToMatchingNodes(TreeNodeMod outdatedMod, TreeNodeMod newMod)
        {
            //if (outdatedMod.Name == newMod.Name)
            //    return true;

            bool matchFound = false;
            List<TreeNodeMod> outdatedFileNodes = GetAllFileNodes(outdatedMod);
            if (outdatedFileNodes.Count == 0)
                return matchFound;

            //List<Tuple<TreeNodeMod, Tuple<TreeNodeMod, TreeNodeMod>>> newMatchingFileNodes1 = 
            //    new List<Tuple<TreeNodeMod, Tuple<TreeNodeMod, TreeNodeMod>>>();
            foreach (var node in outdatedFileNodes)
            {
                TreeNodeMod parentOld = (TreeNodeMod)node.Parent;
                if (parentOld == null)
                    continue;

                string path = parentOld.Text + '\\' + node.Text;
                TreeNodeMod matchingNew = SearchNodeByPath(path, newMod, '\\');
                if (matchingNew == null)
                    continue;

                matchFound = true;
                if (MainForm.Options.ModUpdateBehavior == ModUpdateBehavior.CopyDestination)
                {
                    matchingNew.Destination = node.Destination;
                    ((TreeNodeMod)matchingNew.Parent).Destination = ((TreeNodeMod)node.Parent).Destination;
                }
                matchingNew.Checked = node.Checked;
                matchingNew.Parent.Checked = node.Parent.Checked;

                TreeNodeMod parentNew = matchingNew;
                while (parentOld != null)
                {
                    if (parentOld.Parent == null)
                        break;

                    path = parentOld.Parent.Text + '\\' + path;
                    if (SearchNodeByPath(path, newMod, '\\') == null)
                        break;

                    parentNew = (TreeNodeMod)parentNew.Parent;
                    if (parentNew == null)
                        break;

                    if (MainForm.Options.ModUpdateBehavior == ModUpdateBehavior.CopyDestination)
                        parentNew.Destination = parentOld.Destination;
                    parentNew.Checked = parentOld.Checked;
                    parentOld = (TreeNodeMod)parentOld.Parent;
                }

                //newMatchingFileNodes1.Add(new Tuple<TreeNodeMod, Tuple<TreeNodeMod, TreeNodeMod>>(parentOld,
                //    new Tuple<TreeNodeMod, TreeNodeMod>(matchingNew, node)));
            }

            return matchFound;
        }

        /// <summary>
        /// Adds a mod to the ModSelection.
        /// Creates a TreeNodeMod tree node that represents the mod archives content and adds it to the ModSelection.
        /// </summary>
        /// <param name="modInfo">The ModInfo to create a tree of TreeNodeMod from.</param>
        private TreeNodeMod AddNewMod(ModInfo modInfo)
        {
            TreeNodeMod node = null;
            try
            {
                if (modInfo.LocalPath.ToLower().EndsWith(Constants.EXT_CRAFT) && File.Exists(modInfo.LocalPath))
                    modInfo.LocalPath = CreateZipOfCraftFile(modInfo.LocalPath);

                node = CreateModTreeNodes(modInfo);
                if (node != null)
                {
                    InvokeIfRequired(()=> tvModSelection.Nodes.Add(node));
                    MainForm.AddInfo(string.Format(Constants.MESSAGE_MOD_ADDED, node.Text));
                }
            }
            catch (Exception ex)
            {
                MainForm.AddError(string.Format(Constants.MESSAGE_MOD_ERROR_WHILE_READ_ZIP, "", ex.Message), ex);
            }

            return node;
        }

        /// <summary>
        /// Creates a tree of TreeNodeMod nodes that represent the content of a mod archive.
        /// </summary>
        /// <param name="modInfo">The ModInfo of the mod the create a tree for.</param>
        /// <param name="silent">Determines if info messages should be added.</param>
        /// <returns>A tree of TreeNodeMod nodes that represent the content of a mod archive.</returns>
        private TreeNodeMod CreateModTreeNodes(ModInfo modInfo, bool silent = false)
        {
            if (File.Exists(modInfo.LocalPath))
            {
                TreeNodeMod node = new TreeNodeMod(modInfo);
                using (IArchive archive = ArchiveFactory.Open(modInfo.LocalPath))
                {
                    char seperator = '/';
                    string extension = Path.GetExtension(modInfo.LocalPath);
                    if (extension != null && extension.ToLower() == Constants.EXT_RAR)
                        seperator = '\\';

                    // create a TreeNode for every archive entry
                    foreach (IArchiveEntry entry in archive.Entries)
                        CreateTreeNode(entry.FilePath, node, seperator, silent);
                }

                // Find installation root node (first folder that contains (Parts or Plugins or ...)
                if (!FindInstallRoot(node) && !silent)
                    MainForm.AddInfo(string.Format(Constants.MESSAGE_ROOT_NOT_FOUND, node.Text));

                SetToolTips(node);
                CheckNodesWithDestination(node);

                return node;
            }
            else
            {
                if (!silent)
                    MainForm.AddInfo(string.Format(Constants.MESSAGE_MOD_ZIP_NOT_FOUND, modInfo.LocalPath));
            }

            return null;
        }

        /// <summary>
        /// Finds the root folder of the mod that can be installed to the KSP install folder.
        /// </summary>
        /// <param name="node">Node to start the search from.</param>
        /// <returns>The root folder of the mod that can be installed to the KSP install folder.</returns>
        private bool FindInstallRoot(TreeNodeMod node)
        {
            List<TreeNodeMod> kspFolders = new List<TreeNodeMod>();
            List<TreeNodeMod> craftFiles = new List<TreeNodeMod>();
            GetAllKSPFolders(node, ref kspFolders, ref craftFiles);
            if (kspFolders.Count == 1)
            {
                SetDestinationPaths(kspFolders[0], false);
            }
            else if (kspFolders.Count > 1)
            {
                kspFolders.Sort((TreeNodeMod node1, TreeNodeMod node2) =>
                {
                    if (node2.Depth == node1.Depth)
                        return node1.Text.CompareTo(node2.Text);
                    else
                        return node2.Depth - node1.Depth;
                });

                bool lastResult = false;
                foreach (TreeNodeMod kspFolder in kspFolders)
                {
                    lastResult = SetDestinationPaths(kspFolder, lastResult);
                }
            }

            if (craftFiles.Count > 0)
            {
                foreach (TreeNodeMod craftNode in craftFiles)
                {
                    string vab = MainForm.GetPath(KSP_Paths.VAB);
                    string sph = MainForm.GetPath(KSP_Paths.SPH);
                    if (!craftNode.HasDestination || (!craftNode.Destination.StartsWith(vab) && !craftNode.Destination.StartsWith(sph)))
                        SetCraftDestination(craftNode);
                }
            }

            return (kspFolders.Count > 0) || (craftFiles.Count > 0);
        }

        /// <summary>
        /// Checks the node and all childnodes that have a destination.
        /// </summary>
        /// <param name="node">The node to check.</param>
        private void CheckNodesWithDestination(TreeNodeMod node)
        {
            if (node.HasDestination)
                CheckNodeAndParents(node);

            foreach (TreeNodeMod child in node.Nodes)
                CheckNodesWithDestination(child);
        }

        /// <summary>
        /// Checks the node and all parents.
        /// </summary>
        /// <param name="node">The node to check.</param>
        private static void CheckNodeAndParents(TreeNodeMod node)
        {
            // check node and parent nodes.
            node.Checked = true;
            TreeNodeMod parent = (TreeNodeMod)node.Parent;
            while (parent != null)
            {
                parent.Checked = true;
                parent = (TreeNodeMod)parent.Parent;
            }
        }

        /// <summary>
        /// Searches the passed node for ksp folder.
        /// </summary>
        /// <param name="node">Node to start the search from.</param>
        /// <param name="kspFolders">List of found KSP folders.</param>
        /// <param name="craftFiles">List of found craft files.</param>
        /// <returns>A list of ksp folders.</returns>
        private void GetAllKSPFolders(TreeNodeMod node, ref List<TreeNodeMod> kspFolders, ref List<TreeNodeMod> craftFiles)
        {
            if (node.IsKSPFolder) 
                kspFolders.Add(node);

            if (node.Text.EndsWith(Constants.EXT_CRAFT))
                craftFiles.Add(node);

            foreach (TreeNodeMod child in node.Nodes)
                GetAllKSPFolders(child, ref kspFolders, ref craftFiles);
        }

        /// <summary>
        /// Builds and sets the destination path to the passed node and its childs.
        /// </summary>
        /// <param name="node">Node to set the destination path.</param>
        /// <param name="gameDataFound">Falg to inform the function it the GameData folder was already found (for calls from a loop).</param>
        /// <returns>True if the passed node is the GameData folder, otherwise false.</returns>
        private bool SetDestinationPaths(TreeNodeMod node, bool gameDataFound)
        {
            bool result = false;
            string path = string.Empty;
            TreeNodeMod tempNode = node;
            if (node.Text.ToLower() == Constants.GAMEDATA.ToLower())
            {
                tempNode = node;
                path = MainForm.GetPath(KSP_Paths.KSPRoot);
                result = true;
            }
            else if (node.Text.ToLower() == Constants.SHIPS.ToLower())
            {
                tempNode = node;
                path = MainForm.GetPath(KSP_Paths.KSPRoot);
                result = false;
            }
            else if (node.Text.ToLower() == Constants.VAB.ToLower() ||
                     node.Text.ToLower() == Constants.SPH.ToLower())
            {
                tempNode = node;
                path = MainForm.GetPath(KSP_Paths.Ships);
                result = false;
            }
            else if (gameDataFound || node.Parent == null)
            {
                tempNode = node;
                path = MainForm.GetPathByName(node.Name);
                string a = path.ToLower();
                bool b = a.EndsWith(node.Name.ToLower());
                path = (path.ToLower().EndsWith(node.Name.ToLower())) ? path.ToLower().Replace("\\" + node.Name.ToLower(), "") : path;
                result = false;
            }
            else
            {
                tempNode = (TreeNodeMod)node.Parent;
                path = MainForm.GetPath(KSP_Paths.GameData);
                result = false;
            }

            SetDestinationPaths(tempNode, path);

            return result;
        }

        /// <summary>
        /// Returns the destination path of the craft.
        /// </summary>
        /// <param name="craftNode">The craft to get the destination for.</param>
        /// <returns>The destination path of the craft.</returns>
        private void SetCraftDestination(TreeNodeMod craftNode)
        {
            string zipPath = craftNode.ZipRoot.Name;

            using (IArchive archive = ArchiveFactory.Open(zipPath))
            {
                foreach (IArchiveEntry entry in archive.Entries)
                {
                    if (!entry.FilePath.EndsWith(craftNode.Text))
                        continue;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        entry.WriteTo(ms);
                        ms.Position = 0;
                        using (StreamReader sr = new StreamReader(ms))
                        {
                            string fullText = sr.ReadToEnd();
                            int index = fullText.IndexOf("type = ");
                            if (index == -1) 
                                continue;

                            string filename = Path.GetFileName(entry.FilePath);
                            if (string.IsNullOrEmpty(filename))
                                continue;
                            
                            string shipType = fullText.Substring(index + 7, 3);
                            if (shipType.ToLower() == Constants.SPH)
                                craftNode.Destination = Path.Combine(MainForm.GetPath(KSP_Paths.SPH), filename);
                            else
                                craftNode.Destination = Path.Combine(MainForm.GetPath(KSP_Paths.VAB), filename);

                            SetToolTips(craftNode);

                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a TreeNode.
        /// </summary>
        /// <param name="filename">Zip-File path</param>
        /// <param name="parent">The parent node where the created node will be attached attach to.</param>
        /// <param name="pathSeperator">The seperator charater used within the filename.</param>
        /// <param name="silent">Determines if info messages should be added.</param>
        private void CreateTreeNode(string filename, TreeNodeMod parent, char pathSeperator, bool silent = false)
        {
            // ignore directory entries.
            if (!filename.EndsWith(new string(pathSeperator, 1)) &&
                Path.GetFileName(filename).Contains('.'))
                HandleFileEntry(filename, parent, pathSeperator, silent);
        }

        /// <summary>
        /// Handles and creates a file entry.
        /// </summary>
        /// <param name="filename">Zip-File path</param>
        /// <param name="parent">The parent node where the created node will be attached attach to.</param>
        /// <param name="pathSeperator">The seperator charater used within the filename.</param>
        /// <param name="silent">Determines if info messages should be added.</param>
        private void HandleFileEntry(string filename, TreeNodeMod parent, char pathSeperator, bool silent = false)
        {
            // plain filename?
            if (!filename.Contains(pathSeperator))
            {
                CreateFileListEntry(filename, parent, silent);
            }
            else // filename with dir(s) infront
            {
                TreeNodeMod node = CreateNeededDirNodes(filename, parent, pathSeperator);
                CreateFileListEntry(filename, node, silent);
            }
        }

        /// <summary>
        /// Splits the filename at the pathSeperator and creates a dir node for each split part.
        /// </summary>
        /// <param name="filename">Fullpath within the archive.</param>
        /// <param name="parent">The parent TreeNode.</param>
        /// <param name="pathSeperator">The path seperator that is used within the archive.</param>
        /// <returns>The last created node.</returns>
        private TreeNodeMod CreateNeededDirNodes(string filename, TreeNodeMod parent, char pathSeperator)
        {
            TreeNodeMod dirNode = parent;
            string[] dirs = filename.Split(pathSeperator);
            for (int i = 0; i < dirs.Count<string>() - 1; ++i)
            {
                if (dirNode.Nodes.ContainsKey(dirs[i]))
                    dirNode = (TreeNodeMod)dirNode.Nodes[dirs[i]];
                else
                    dirNode = CreateDirListEntry(dirs[i], dirNode);
            }

            return dirNode;
        }

        /// <summary>
        /// Creates a file entry for the TreeView. 
        /// </summary>
        /// <param name="fileName">Zip-File path of the file.</param>
        /// <param name="parent">The parent node where the created node will be attached attach to.</param>
        /// <param name="silent">Determines if info messages should be added.</param>
        private void CreateFileListEntry(string fileName, TreeNodeMod parent, bool silent = false)
        {
            TreeNodeMod node = new TreeNodeMod(fileName, Path.GetFileName(fileName), NodeType.UnknownFile);
            // TODO:!!!
            //node.ToolTipText = "<No path selected>";
            parent.Nodes.Add(node);

            if (!silent)
                MainForm.AddInfo(string.Format(Constants.MESSAGE_FILE_ADDED, fileName));
        }

        /// <summary>
        /// Creates a directory entry for the TreeView. 
        /// </summary>
        /// <param name="dirName">Name of the directory.</param>
        /// <param name="parent">The parent node where the created node will be attached attach to.</param>
        private TreeNodeMod CreateDirListEntry(string dirName, TreeNodeMod parent)
        {
            // dir already created?
            TreeNodeMod dirNode = SearchNodeByPath(parent.Text + "/" + dirName, parent, '/');
            //TreeNodeMod dirNode = SearchNode(dirName, parent);
            if (null == dirNode)
            {
                dirNode = new TreeNodeMod(dirName, dirName);
                // TODO:!!!
                //dirNode.ToolTipText = "<No path selected>";
                dirNode.NodeType = (MainForm.IsKSPDir(dirName)) ? NodeType.KSPFolder : NodeType.UnknownFolder;
                parent.Nodes.Add(dirNode);

                MainForm.AddInfo(string.Format(Constants.MESSAGE_DIR_ADDED, dirName));
            }

            return dirNode;
        }

        /// <summary>
        /// Searches the tree for the first Node where the name matches the search text.
        /// </summary>
        /// <param name="searchText">The search string.</param>
        /// <param name="startNode">Node to start the search from.</param>
        /// <returns>The first Node where the name matches the search text.</returns>
        private TreeNodeMod SearchNode(string searchText, Node startNode)
        {
            TreeNodeMod node = null;
            if (startNode.Text.ToLower() == (searchText.ToLower()))
            {
                node = (TreeNodeMod)startNode;
            }
            else
            {
                foreach (TreeNodeMod child in startNode.Nodes)
                {
                    node = SearchNode(searchText, child);
                    if (node != null)
                        break;
                }
            }

            return node;
        }

        #endregion

        #region Remove MOD from TreeView

        /// <summary>
        /// Callback for key down events of keys that deletes mods from the list.
        /// </summary>
        /// <param name="actionKeyInfo">Infos of the action key.</param>
        /// <returns>True if the key was handled, oitherwise false.</returns>
        private bool HandleDeleteKey(ActionKeyInfo actionKeyInfo)
        {
            RemoveMod(SelectedRootNodes);
            return true;
        }

        /// <summary>
        /// Removes a Mod from the tree and KSP.
        /// </summary>
        /// <param name="nodesToRemove">The Zip-File node to remove.</param>
        /// <param name="silent">Determines if info messages should be added.</param>
        private void RemoveMod(List<TreeNodeMod> nodesToRemove, bool silent = false)
        {
            // TODO: Do it async!!

            if (nodesToRemove.Count > 0 &&
                (silent || DialogResult.Yes == MessageBox.Show(this, Constants.MESSAGE_MOD_DELETE_QUESTION, "", MessageBoxButtons.YesNo)))
            {
                // deinstall mod
                TreeNodeMod[] nodes = new TreeNodeMod[nodesToRemove.Count];
                for (int i = 0; i < nodesToRemove.Count; ++i)
                {
                    nodes[i] = (TreeNodeMod)nodesToRemove[i];
                    tvModSelection.ChangeCheckedState(nodesToRemove[i], false, true, true);
                }
                ProcessNodes(nodes, silent);

                // remove node from TreeView
                foreach (TreeNodeMod node in nodesToRemove)
                {
                    tvModSelection.Nodes.Remove(node);
                    ModRegister.RemoveRegisteredMod(node);
                }

                if (tvModSelection.Nodes.Count == 0)
                    ClearNodes();

                MainForm.SaveKSPConfig();
            }
        }

        #endregion

        #region Processing

        /// <summary>
        /// Processes all passed nodes. (Adds/Removes the MOD to/from the KSP install folders).
        /// </summary>
        /// <param name="nodeCollection">The NodeCollection to process.</param>
        /// <param name="silent">Determines if info messages should be added.</param>
        private void ProcessSelectionAsync(TreeListViewNodes nodeCollection, bool silent = false)
        {
            TreeNodeMod[] nodes = new TreeNodeMod[nodeCollection.Count];
            for (int i = 0; i < nodeCollection.Count; ++i)
                nodes[i] = (TreeNodeMod)nodeCollection[i];

            ProcessSelectionAsync(nodes, silent);
        }

        /// <summary>
        /// Processes all passed nodes. (Adds/Removes the MOD to/from the KSP install folders).
        /// </summary>
        /// <param name="nodes">The array of node to process.</param>
        /// <param name="silent">Determines if info messages should be added.</param>
        private void ProcessSelectionAsync(TreeNodeMod[] nodes, bool silent = false)
        {
            if (nodes == null || nodes.Length == 0) return;

            btnAdd.Enabled = false;
            btnRemove.Enabled = false;
            btnClear.Enabled = false;
            btnScanGameData.Enabled = false;
            grpProceed.Enabled = false;
            tvModSelection.ReadOnly = true;
            pBarModProcessing.Value = m_ProcessedNodesCount = 0;
            pBarModProcessing.Maximum = GetFullNodeCount(nodes);
            pBarModProcessing.Visible = true;
            m_RunningTask = new AsyncTask<bool>();
            m_RunningTask.SetCallbackFunctions(delegate() { ProcessNodes(nodes, silent); return true; },
                                               delegate(bool result, Exception ex)
                                               {
                                                   if (ex != null)
                                                       MessageBox.Show(this, ex.Message, "Error");

                                                   btnAdd.Enabled = true;
                                                   btnRemove.Enabled = true;
                                                   btnClear.Enabled = true;
                                                   btnScanGameData.Enabled = true;
                                                   grpProceed.Enabled = true;
                                                   tvModSelection.ReadOnly = false;
                                                   m_RunningTask = null;
                                                   pBarModProcessing.Visible = false;

                                                   MainForm.AddInfo("Done!");
                                               },
                                               delegate(int percentage) { if (pBarModProcessing.Maximum >= percentage) pBarModProcessing.Value = percentage; });
            m_RunningTask.Run();
        }

        /// <summary>
        /// Returns the count of all nodes and subnode and subsub...
        /// </summary>
        /// <param name="nodeList">The list to count the nodes from.</param>
        /// <returns>The count of nodes.</returns>
        private int GetFullNodeCount(List<TreeNodeMod> nodeList)
        {
            if (nodeList == null || nodeList.Count == 0) return 0;

            TreeNodeMod[] nodes = new TreeNodeMod[nodeList.Count];
            for (int i = 0; i < nodeList.Count; ++i)
                nodes[i] = nodeList[i];

            return GetFullNodeCount(nodes);
        }

        /// <summary>
        /// Returns the count of all nodes and subnode and subsub...
        /// </summary>
        /// <param name="nodeArray">The array to count the nodes from.</param>
        /// <returns>The count of nodes.</returns>
        private int GetFullNodeCount(TreeNodeMod[] nodeArray)
        {
            if (nodeArray == null || nodeArray.Length == 0) return 0;

            int count = 0;
            foreach (TreeNodeMod node in nodeArray)
            {
                ++count;

                TreeNodeMod[] nodes = new TreeNodeMod[node.Nodes.Count];
                for (int i = 0; i < node.Nodes.Count; ++i)
                    nodes[i] = (TreeNodeMod)node.Nodes[i];

                count += GetFullNodeCount(nodes);
            }

            return count;
        }

        /// <summary>
        /// Processes all passed nodes. (Adds/Removes the MOD to/from the KSP install folders).
        /// </summary>
        /// <param name="nodeArray">The NodeArray to process.</param>
        /// <param name="silent">Determines if info messages should be added displayed.</param>
        public void ProcessNodes(TreeNodeMod[] nodeArray, bool silent = false)
        {
            foreach (TreeNodeMod node in nodeArray)
            {
                if (node.Checked && !File.Exists(node.ZipRoot.Name))
                {
                    if (!silent)
                        MainForm.AddInfo(string.Format(Constants.MESSAGE_MOD_ZIP_NOT_FOUND, node.Text));
                    break;
                }

                if (node.HasDestination)
                {
                    if (!silent)
                        MainForm.AddInfo(string.Format(Constants.MESSAGE_ROOT_IDENTIFIED, node.Text));

                    ProcessNode(node, silent);

                    DeleteNotProcessedDirectorys(silent);

                    InvokeIfRequired(()=> m_NotDeletedDirs.Clear());
                }
                else if (node.HasDestinationForChilds)
                {
                    TreeNodeMod[] nodes = new TreeNodeMod[node.Nodes.Count];
                    for (int i = 0; i < node.Nodes.Count; ++i)
                        nodes[i] = (TreeNodeMod)node.Nodes[i];

                    ProcessNodes(nodes, silent);
                }
                else if (!silent)
                {
                    MainForm.AddInfo(string.Format(Constants.MESSAGE_ROOT_NOT_FOUND, node.Text));
                }
            }
        }

        /// <summary>
        /// Processes the passed node. (Adds/Removes the MOD to/from the KSP install folders).
        /// </summary>
        /// <param name="node">The node to process.</param>
        /// <param name="silent">Determines if info messages should be added displayed.</param>
        private void ProcessNode(TreeNodeMod node, bool silent = false)
        {
            if (node.Checked)
            {
                // Extranct and copy file.
                if (!node.IsFile)
                {
                    if (!Directory.Exists(node.Destination))
                    {
                        try
                        {
                            Directory.CreateDirectory(node.Destination);
                            if (!silent)
                                MainForm.AddInfo(string.Format(Constants.MESSAGE_DIR_CREATED, node.Destination));
                        }
                        catch
                        {
                            MainForm.AddInfo(string.Format(Constants.MESSAGE_DIR_CREATED_ERROR, node.Destination));
                        }
                    }

                    InvokeIfRequired(() => node.NodeType = (node.IsKSPFolder) ? NodeType.KSPFolderInstalled : NodeType.UnknownFolderInstalled);
                }
                else
                {
                    ExtractFile(node, node.Destination, silent);
                }
            }
            else
            {
                // Try remove dir/file.
                if (!node.IsFile)
                {
                    if (!node.IsKSPFolder)
                    {
                        if (Directory.Exists(node.Destination))
                        {
                            if (!Directory.GetDirectories(node.Destination).Any() && !Directory.GetFiles(node.Destination).Any())
                            {
                                try
                                {
                                    Directory.Delete(node.Destination, true); 
                                    if (!silent)
                                        MainForm.AddInfo(string.Format(Constants.MESSAGE_DIR_DELETED, node.Destination));
                                }
                                catch
                                {
                                    m_NotDeletedDirs.Add(node);
                                }
                            }
                            else
                            {
                                // add dir for later try to delete
                                InvokeIfRequired(() => m_NotDeletedDirs.Add(node));
                            }
                        }
                    }
                    else
                        m_NotDeletedDirs.Add(node);
                    
                    InvokeIfRequired(() => node.NodeType = (node.IsKSPFolder) ? NodeType.KSPFolder : NodeType.UnknownFolder);
                }
                else
                {
                    bool installedByOtherMod = ModRegister.GetCollisionModFiles(node).Any(n => n.IsInstalled);
                    if (File.Exists(node.Destination) && !installedByOtherMod)
                    {
                        try
                        {
                            File.Delete(node.Destination);
                            if (!silent)
                                MainForm.AddInfo(string.Format(Constants.MESSAGE_FILE_DELETED, node.Destination));
                        }
                        catch (Exception ex)
                        {
                            MainForm.AddError(string.Format(Constants.MESSAGE_FILE_DELETED_ERROR, node.Destination), ex);
                        }
                    }
                    
                    InvokeIfRequired(() => node.NodeType = NodeType.UnknownFile);
                }
            }

            foreach (TreeNodeMod child in node.Nodes)
                ProcessNode(child, silent);

            //if (m_RunningTask != null)
            //    m_RunningTask.PercentFinished = m_ProcessedNodesCount += 1;
        }

        /// <summary>
        /// Extracts the file from the archiv with the passen key.
        /// </summary>
        /// <param name="node">The node to install the file from.</param>
        /// <param name="path">The path to install the file to.</param>
        /// <param name="silent">Determines if info messages should be added displayed.</param>
        private void ExtractFile(TreeNodeMod node, string path, bool silent = false)
        {
            if (node == null) return;

            using (IArchive archive = ArchiveFactory.Open(node.ZipRoot.Name))
            {
                IArchiveEntry entry = archive.Entries.FirstOrDefault(e => e.FilePath == node.Name);
                if (entry == null) 
                    return;

                if (!File.Exists(path))
                {
                    try
                    {
                        // create new file.
                        entry.WriteToFile(path);

                        if (!silent)
                            MainForm.AddInfo(string.Format(Constants.MESSAGE_FILE_EXTRACTED, path));
                    }
                    catch (Exception ex)
                    {
                        MainForm.AddError(string.Format(Constants.MESSAGE_FILE_EXTRACTED_ERROR, path), ex);
                    }
                }
                else if (Override)
                {
                    try
                    {
                        // delete old file
                        File.Delete(path);

                        // create new file.
                        entry.WriteToFile(path);

                        if (!silent)
                            MainForm.AddInfo(string.Format(Constants.MESSAGE_FILE_EXTRACTED, path));
                    }
                    catch (Exception ex)
                    {
                        MainForm.AddError(string.Format(Constants.MESSAGE_FILE_EXTRACTED_ERROR, path), ex);
                    }
                }
                
                InvokeIfRequired(() => node.NodeType = NodeType.UnknownFileInstalled);
            }
        }

        /// <summary>
        /// Try to delete all not processed directories.
        /// </summary>
        /// <param name="silent">Determines if info messages should be added displayed.</param>
        private void DeleteNotProcessedDirectorys(bool silent = false)
        {
            m_NotDeletedDirs.Sort((x, y) => y.Depth - x.Depth);
            foreach (TreeNodeMod node in m_NotDeletedDirs)
                DeleteDirectory(node.Destination, silent);
        }

        /// <summary>
        /// Try to delete all not processed directories.
        /// </summary>
        /// <param name="dir">The dir to delete.</param>
        /// <param name="silent">Determines if info messages should be added displayed.</param>
        private void DeleteDirectory(string dir, bool silent = false)
        {
            try
            {
                if (Directory.Exists(dir) && !Directory.GetDirectories(dir).Any() &&
                    !Directory.GetFiles(dir).Any() && !MainForm.IsKSPDir(dir))
                {
                    Directory.Delete(dir, true);
                    if (!silent)
                        MainForm.AddInfo(string.Format("Directory deleted \"{0}\" ", dir));
                }
            }
            catch (Exception ex)
            {
                MainForm.AddError(string.Format(Constants.MESSAGE_DIR_DELETED_ERROR, dir), ex);
            }
        }

        #endregion

        #region Select destination

        /// <summary>
        /// Displays a dialg to select a source and destination folder.
        /// </summary>
        /// <param name="node">The root node of the archive file.</param>
        /// <returns>True if dialog was quit with DialogResult.OK</returns>
        private bool SelectDestFolder(TreeNodeMod node)
        {
            if (node == null) return false;

            string a = MainForm.GetPath(KSP_Paths.KSPRoot);
            string dest = node.Destination.Replace(MainForm.GetPath(KSP_Paths.KSPRoot).ToLower(), "");
            if (dest.StartsWith("\\"))
                dest = dest.Substring(1);
            int index = dest.IndexOf("\\");
            if (index > -1)
                dest = dest.Substring(0, index);

            frmDestFolderSelection dlg = new frmDestFolderSelection(MainForm);
            dlg.DestFolders = GetDefaultDestPaths();
            dlg.DestFolder = dest;
            dlg.SrcFolders = new TreeNodeMod[] { node };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string src = dlg.SrcFolder;
                if (dlg.SrcFolder.Contains("/") || dlg.SrcFolder.Contains("\\"))
                    src = Path.GetFileName(dlg.SrcFolder);

                TreeNodeMod srcNode = SearchNode(src, node);
                if (srcNode == null)
                    srcNode = SearchNode(Path.GetFileNameWithoutExtension(src), node);

                if (dlg.SrcFolder == node.Name)
                    srcNode = node;

                if (srcNode != null)
                {
                    SetDestinationPaths(srcNode, dlg.DestFolder, dlg.CopyContent);
                    tvModSelection.Invalidate();

                    if (MainForm.Instance.Options.ShowConflictSolver && node.HasChildCollision)
                    {
                        frmCollisionSolving csDlg = new frmCollisionSolving {CollisionMod = node};
                        if (csDlg.ShowDialog() == DialogResult.OK && csDlg.SelectedMod != node.ZipRoot)
                            return false;
                    }

                    return true;
                }
                else
                    MainForm.AddInfo(Constants.MESSAGE_SOURCE_NODE_NOT_FOUND);
            }

            return false;
        }

        /// <summary>
        /// Returns a string array of possible destination paths.
        /// </summary>
        /// <returns>A string array of possible destination paths.</returns>
        private string[] GetDefaultDestPaths()
        {
            List<string> destFolders = new List<string>();
            destFolders.AddRange(Constants.KSPFolders);
            destFolders.Add(Constants.VAB);
            destFolders.Add(Constants.SPH);

            for (int i = 0; i < destFolders.Count<string>(); ++i)
                destFolders[i] = MainForm.GetPathByName(destFolders[i]);

            return destFolders.ToArray();
        }

        #endregion

        #region Scan GameData

        /// <summary>
        /// Scanns the KSP GameData directory for installed mods and adds them to the ModSelection.
        /// </summary>
        private void ScanGameData()
        {
            List<ScanInfo> entries = new List<ScanInfo>();
            try
            {
                string scanDir = MainForm.GetPath(KSP_Paths.GameData);
                string[] dirs = Directory.GetDirectories(scanDir);
                foreach (string dir in dirs)
                {
                    string dirname = dir.Substring(dir.LastIndexOf("\\") + 1);
                    if (dirname.ToLower() != "squad" && dirname.ToLower() != "myflags" && dirname.ToLower() != "nasamission")
                    {
                        ScanInfo scanInfo = new ScanInfo(dirname, dir, false);
                        entries.Add(scanInfo);
                        ScanDir(scanInfo);
                    }
                }

                List<ScanInfo> unknowns = GetUnknowenNodes(entries);
                if (unknowns.Count > 0)
                {
                    foreach (ScanInfo unknown in unknowns)
                    {
                        TreeNodeMod node = ScanInfoToKSPMA_TreeNode(unknown);
                        tvModSelection.Nodes.Add(node);
                    }
                }
                else
                    MainForm.AddInfo("No new Mods found.");

                RenewCheckedStateAllNodes();
            }
            catch (Exception ex)
            {
                MainForm.AddError("Error during Gamedata folder scan.", ex);
            }
        }

        /// <summary>
        /// Scanns the passed dir for files and directories and creates a tree of ScannInfos from it.
        /// </summary>
        /// <param name="scanDir">The ScanInfo of the start directory.</param>
        private void ScanDir(ScanInfo scanDir)
        {
            List<ScanInfo> entries = new List<ScanInfo>();
            foreach (string file in Directory.GetFiles(scanDir.Path))
            {
                string filename = Path.GetFileName(file);
                ScanInfo scanInfo = new ScanInfo(filename, file, true);
                scanInfo.Parent = scanDir;
            }

            string[] dirs = Directory.GetDirectories(scanDir.Path);
            foreach (string dir in dirs)
            {
                string dirname = dir.Substring(dir.LastIndexOf("\\") + 1);
                ScanInfo scanInfo = new ScanInfo(dirname, dir, false, scanDir);
                ScanDir(scanInfo);
            }
        }

        /// <summary>
        /// Searches the list of ScanInfo trees for unknowen nodes.
        /// Searches the complete ModSelection for a matching node.
        /// </summary>
        /// <param name="scanInfos">A list of ScanInfos trees to search.</param>
        /// <returns>A list of scanInfo trees with unknown nodes.</returns>
        private List<ScanInfo> GetUnknowenNodes(List<ScanInfo> scanInfos)
        {
            List<ScanInfo> entries = new List<ScanInfo>();
            foreach (ScanInfo entry in scanInfos)
            {
                bool found = false;
                foreach (TreeNodeMod node in tvModSelection.Nodes)
                {
                    if (CompareNodes(entry, node))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    entries.Add(entry);
            }
            return entries;
        }

        /// <summary>
        /// Compares the ScanInfo to all known nodes (from parent).
        /// </summary>
        /// <param name="scanInfo">The ScanInfo to compare.</param>
        /// <param name="parent">The start node of the comparision.</param>
        /// <returns>True if a match was found, otherwise false.</returns>
        private bool CompareNodes(ScanInfo scanInfo, TreeNodeMod parent)
        {
            if (scanInfo.Name == parent.Text)
                return true;

            foreach (TreeNodeMod child in parent.Nodes)
            {
                if (child.Text == scanInfo.Name)
                    return true;

                if (CompareNodes(scanInfo, child))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Creates a TreeNodeMod from the passed ScanInfo.
        /// </summary>
        /// <param name="unknown">The ScanInfo of the unknown node.</param>
        /// <returns>The new created TeeeNodeMod.</returns>
        private TreeNodeMod ScanInfoToKSPMA_TreeNode(ScanInfo unknown)
        {
            TreeNodeMod node = new TreeNodeMod();
            node.Name = unknown.Name;
            node.Text = unknown.Name;
            node.AddDate = DateTime.Now.ToString();
            node.Destination = unknown.Path;
            node.Checked = true;

            if (unknown.IsFile)
                node.NodeType = NodeType.UnknownFileInstalled;
            else
                node.NodeType = NodeType.UnknownFolderInstalled;

            foreach (ScanInfo si in unknown.Childs)
            {
                TreeNodeMod child = ScanInfoToKSPMA_TreeNode(si);
                node.Nodes.Add(child);
            }

            return node;
        }

        #endregion

        #region Zip creation

        /// <summary>
        /// Creates a Zip for each root node in the passed node list.
        /// </summary>
        /// <param name="nodes">List of root nodes to create zips for.</param>
        private void CreateZip(List<TreeNodeMod> nodes = null)
        { 
            // get path for the zip
            MainForm.Options.GetValidDownloadPath();
            if (!Directory.Exists(MainForm.Options.DownloadPath))
            {
                MainForm.AddInfo("Zip creation aborted!");
                return;
            }

            if (nodes == null)
                nodes = SelectedNodes;

            int nodeCount = GetFullNodeCount(nodes);

            // disable controls
            btnAdd.Enabled = false;
            btnRemove.Enabled = false;
            btnClear.Enabled = false;
            btnScanGameData.Enabled = false;
            grpProceed.Enabled = false;
            tvModSelection.ReadOnly = true;
            pBarModProcessing.Visible = true;

            m_RunningTask = new AsyncTask<bool>();
            m_RunningTask.SetCallbackFunctions(
                delegate()
                {
                    try
                    {
                        // create the zip
                        List<string> done = new List<string>();
                        foreach (TreeNodeMod node in nodes)
                        {
                            TreeNodeMod root = node.ZipRoot;
                            if (root != null && !File.Exists(root.Name) && !done.Contains(root.Name))
                            {
                                string zipPath = Path.Combine(MainForm.Options.DownloadPath, Path.GetFileNameWithoutExtension(root.Name) + Constants.EXT_ZIP);
                                CreateZip(zipPath, root);
                                MainForm.AddInfo(string.Format("Zip \"{0}\" created. ", root.Name));
                                done.Add(root.Name);
                                root.Name = zipPath;
                                tvModSelection.Invalidate();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        InvokeIfRequired(() => MessageBox.Show(MainForm, "Ceating of zip failed. " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error));
                        return false;
                    }

                    return true;
                },
                delegate(bool b, Exception ex)
                {
                    btnAdd.Enabled = true;
                    btnRemove.Enabled = true;
                    btnClear.Enabled = true;
                    btnScanGameData.Enabled = true;
                    grpProceed.Enabled = true;
                    tvModSelection.ReadOnly = false;
                    pBarModProcessing.Visible = false;
                },
                delegate(int processedNodeCount)
                {
                    pBarModProcessing.Maximum = nodeCount;
                    if (processedNodeCount != -1 && processedNodeCount <= nodeCount)
                        pBarModProcessing.Value = processedNodeCount;
                });
            m_RunningTask.Run();
        }

        private void CreateZip(string zipPath, TreeNodeMod root)
        {
            using (var zip = ZipArchive.Create())
            {
                int nodecount = 0;
                foreach (TreeNodeMod child in root.Nodes)
                    nodecount = CreateZipEntry(zip, child, nodecount);
                
                InvokeIfRequired(() => pBarModProcessing.Value = 0);

                zip.SaveTo(zipPath, CompressionType.None);
            }
        }

        /// <summary>
        /// Creates a ZipEntry of the passed node and its childs to the passed zip file.
        /// </summary>
        /// <param name="zip">The zip file to add the new zip entry to.</param>
        /// <param name="node">The node to create a zip entry for.</param>
        /// <param name="processedNodeCount">Count of the processed nodes (for recursive calls only).</param>
        /// <returns>Count of the processed nodes.</returns>
        private int CreateZipEntry(ZipArchive zip, TreeNodeMod node, int processedNodeCount = 0)
        {
            string path = node.Destination.ToLower().Replace(MainForm.GetPath(KSP_Paths.GameData) + "\\", "");
            if (node.IsFile)
                zip.AddEntry(path, node.Destination);

            if (m_RunningTask != null)
                m_RunningTask.PercentFinished = ++processedNodeCount;

            foreach (TreeNodeMod child in node.Nodes)
                processedNodeCount = CreateZipEntry(zip, child, processedNodeCount);

            return processedNodeCount;
        }

        #endregion

        /// <summary>
        /// Tries to open the file if the passed node represents a file.
        /// </summary>
        /// <param name="node">The node to open the file from.</param>
        private bool TryOpenFile(TreeNodeMod node)
        {
            if (node == null || !node.IsFile) return false;

            while (node.Parent != null)
                node = (TreeNodeMod)node.Parent;

            string fullpath = node.Name;
            try
            {
                if (File.Exists(fullpath))
                {
                    using (IArchive archiv = ArchiveFactory.Open(fullpath))
                    {
                        string fullPath = SelectedNode.FullPath;
                        foreach (IArchiveEntry entry in archiv.Entries)
                        {
                            string filename = Path.GetFileName(entry.FilePath);
                            if (fullPath.Contains(entry.FilePath.Replace('/', '\\')) &&
                                filename == SelectedNode.Text)
                            {
                                using (MemoryStream memStream = new MemoryStream())
                                {
                                    entry.WriteTo(memStream);
                                    memStream.Position = 0;
                                    StreamReader reader = new StreamReader(memStream);

                                    TextDisplayer frm = new TextDisplayer();
                                    frm.TextBox.Text = reader.ReadToEnd();
                                    frm.Text = filename;
                                    frm.ShowDialog(this);
                                    
                                    return true;
                                }
                            }
                        }
                    }
                }
                else
                    MainForm.AddInfo("File not found \"" + fullpath + "\".");
            }
            catch (Exception ex)
            {
                MainForm.AddError("Error while reading \"" + fullpath + "\".", ex);
            }

            return false;
        }

        /// <summary>
        /// Opens a FolderBrowserDialog and sets the archive source path to the selected archive node or all archive nodes (if passed node is null).
        /// </summary>
        /// <param name="node">The archive node to set selected source folder to or null.</param>
        private void SelectNewZipSourceFolder(TreeNodeMod node)
        {
            FolderSelectDialog dlg = new FolderSelectDialog();

            if (node != null)
                dlg.Title = "Select new source of the selected Mod Zip-File \"" + Path.GetFileName(node.Name) + "\".";
            else
                dlg.Title = "Select new source of all Mod Zip-Files.";

            if (dlg.ShowDialog(this.Handle))
            {
                try
                {
                    if (node != null)
                    {
                        string newFullPath = Path.Combine(dlg.FileName, Path.GetFileName(node.Name));
                        if (File.Exists(newFullPath))
                        {
                            node.Name = newFullPath;
                            tvModSelection.Invalidate();
                            MainForm.SaveKSPConfig();
                        }
                        else
                            MessageBox.Show(this, "The selected folder did not contain the Mod Zip-File \"" + Path.GetFileName(node.Name) + "\".");
                    }
                    else
                    {
                        foreach (TreeNodeMod root in tvModSelection.Nodes)
                        {
                            string newFullPath = Path.Combine(dlg.FileName, Path.GetFileName(root.Name));
                            if (File.Exists(newFullPath))
                            {
                                root.Name = newFullPath;
                                tvModSelection.Invalidate();
                                MainForm.SaveKSPConfig();
                            }
                            else
                                MainForm.AddInfo("Mod Zip-File \"" + Path.GetFileName(root.Name) + "\" not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MainForm.AddError(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// Traversing the tree and renews the checked state of the nodes.
        /// </summary>
        /// <param name="node">The beginning node.</param>
        private void RenewCheckedState(TreeNodeMod node)
        {
            m_Processing = true;

            if (node.HasDestination)
            {
                if (node.IsFile)
                {
                    bool exists = false;
                    try
                    {
                        exists = File.Exists(node.Destination);
                    }
                    catch (Exception ex)
                    {
                        string NoWarningPls = ex.Message;
                    }

                    if (exists)
                    {
                        node.NodeType = NodeType.UnknownFileInstalled;
                        tvModSelection.ChangeCheckedState(node, true, false, true);
                    }
                    else
                    {
                        node.NodeType = NodeType.UnknownFile;
                        tvModSelection.ChangeCheckedState(node, false, false, true);
                    }
                }
                else
                {
                    bool exists = false;
                    try
                    {
                        exists = Directory.Exists(node.Destination);
                    }
                    catch (Exception ex)
                    {
                        string NoWarningPls = ex.Message;
                    }

                    // Don't check GameData folder if there are no installed childs
                    if ((node.Text.ToLower() == Constants.GAMEDATA.ToLower() && !node.HasInstalledChilds) ||
                        (node.Text.ToLower() == Constants.SHIPS.ToLower() && !node.HasInstalledChilds) ||
                        (node.Text.ToLower() == Constants.VAB.ToLower() && !node.HasInstalledChilds) ||
                        (node.Text.ToLower() == Constants.SPH.ToLower() && !node.HasInstalledChilds))
                        exists = false;

                    if (exists)
                    {
                        node.NodeType = NodeType.UnknownFolderInstalled;
                        tvModSelection.ChangeCheckedState(node, true, false, true);
                    }
                    else
                    {
                        node.NodeType = NodeType.UnknownFolder;
                        tvModSelection.ChangeCheckedState(node, false, false, true);
                    }
                }
            }
            else if (node.HasInstalledChilds)
                tvModSelection.ChangeCheckedState(node, true, false, true);
            else
                tvModSelection.ChangeCheckedState(node, false, false, true);

            foreach (TreeNodeMod child in node.Nodes)
                RenewCheckedState(child);

            m_Processing = false;
        }

        /// <summary>
        /// Sorts the list by the Text property of the nodes.
        /// </summary>
        /// <param name="nodes">A list of node that should be sorted.</param>
        /// <param name="desc">True if sort in descending order.</param>
        private void SortByName(ref List<TreeNodeMod> nodes, bool desc)
        {
            nodes.Sort(delegate(TreeNodeMod node1, TreeNodeMod node2)
                       {
                           if (desc)
                               return node1.Text.CompareTo(node2.Text);
                           else
                               return node2.Text.CompareTo(node1.Text);
                       });
        }

        /// <summary>
        /// Sorts the list by the date the mod was added.
        /// </summary>
        /// <param name="nodes">A list of node that should be sorted.</param>
        /// <param name="desc">True if sort in descending order.</param>
        private void SortByAddDate(ref List<TreeNodeMod> nodes, bool desc)
        {
            nodes.Sort(delegate(TreeNodeMod node1, TreeNodeMod node2)
            {
                DateTime date1 = DateTime.Now;
                if (!string.IsNullOrEmpty(node1.AddDate))
                    date1 = DateTime.Parse(node1.AddDate);

                DateTime date2 = DateTime.Now;
                if (!string.IsNullOrEmpty(node2.AddDate))
                    date2 = DateTime.Parse(node2.AddDate);

                if (desc)
                    return date1.CompareTo(date2);
                else
                    return date2.CompareTo(date1);
            });
        }

        /// <summary>
        /// Sorts the list by the current node state (red, yellow, grey, black, green.
        /// </summary>
        /// <param name="nodes">A list of node that should be sorted.</param>
        /// <param name="desc">True if sort in descending order.</param>
        private void SortByState(ref List<TreeNodeMod> nodes, bool desc)
        {
            // TODO: 
            nodes.Sort(delegate(TreeNodeMod node1, TreeNodeMod node2)
            {
                if (desc)
                    return node1.Text.CompareTo(node2.Text);
                else
                    return node2.Text.CompareTo(node1.Text);
            });
        }

        /// <summary>
        /// Sorts the list by the KSP version of the mod.
        /// </summary>
        /// <param name="nodes">A list of node that should be sorted.</param>
        /// <param name="desc">True if sort in descending order.</param>
        private void SortByVersion(ref List<TreeNodeMod> nodes, bool desc)
        {
            // TODO: SORT BY VERSION
            nodes.Sort(delegate(TreeNodeMod node1, TreeNodeMod node2)
            {
                if (desc)
                    return node1.Version.CompareTo(node2.Version);
                else
                    return node2.Version.CompareTo(node1.Version);
            });
        }

        /// <summary>
        /// Opens the install dir of the mod if it exists.
        /// </summary>
        private void OpenSelectedModFolder()
        {
            TreeNodeMod modNode = null;
            if (tvModSelection.NodesSelection.Count > 0)
                modNode = (TreeNodeMod)tvModSelection.NodesSelection[0];

            if (!modNode.IsInstalled && !modNode.HasInstalledChilds)
            {
                if (tvModSelection.SelectedRootNodes.Count <= 0) return;

                modNode = (TreeNodeMod)tvModSelection.SelectedRootNodes[0];

                if (!modNode.IsInstalled && !modNode.HasInstalledChilds) return;
            }

            try
            {
                string fullpath = GetFirstValidDestination(modNode);
                if (!string.IsNullOrEmpty(fullpath) && Directory.Exists(fullpath))
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fullpath;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                MainForm.AddError("Open mod folder faild.", ex);
            }
        }

        /// <summary>
        /// Returns the first destination that is installed.
        /// </summary>
        /// <param name="modNode">TreeNode to search from.</param>
        /// <returns>The destination path (directory).</returns>
        private string GetFirstValidDestination(TreeNodeMod modNode)
        {
            if (modNode.HasDestination && !modNode.Destination.ToLower().EndsWith("gamedata"))
            {
                if (Path.GetFileName(modNode.Destination).Contains("."))
                {
                    if (File.Exists(modNode.Destination))
                        return Path.GetDirectoryName(modNode.Destination);
                }
                else
                {
                    if (Directory.Exists(modNode.Destination))
                        return modNode.Destination;
                }
            }
            else
            {
                foreach (TreeNodeMod child in modNode.Nodes)
                {
                    string fullpath = GetFirstValidDestination(child);
                    if (!string.IsNullOrEmpty(fullpath))
                        return fullpath;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Jumps to the ModBrowser tab and navigates to the URL.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        private void VisitURL(string url)
        {
            MainForm.tabControl1.SelectedTab = MainForm.tabPageModBrowser;
            MainForm.ModBrowser.Navigate(url);
        }

        /// <summary>
        /// Opens the copy mod info dialog, and copy the selected mod to the choosen mod from the dialog.
        /// </summary>
        /// <param name="zipRoot">The root node of the mod to copy.</param>
        private void CopyModInfos(TreeNodeMod zipRoot)
        {
            frmCopyModInfo dlg = new frmCopyModInfo();
            dlg.SourceMod = zipRoot;
            dlg.Mods = tvModSelection.Nodes;
            dlg.ShowDialog(MainForm);
        }

        /// <summary>
        /// Opens the ModInfo editor for the selected mod.
        /// </summary>
        /// <param name="zipRoot">The root node of the mod to edit.</param>
        private void EditModInfos(TreeNodeMod zipRoot)
        {
            frmEditModInfo dlg = new frmEditModInfo();
            dlg.ModZipRoot = zipRoot;
            if (dlg.ShowDialog(MainForm) == DialogResult.OK)
            {
                if (!zipRoot.IsInstalled)
                    zipRoot.Text = dlg.ModName;

                zipRoot.AddDate = dlg.DownloadDate;
                zipRoot.Author = dlg.Author;
                zipRoot.CreationDate = dlg.CreationDate;
                zipRoot.Downloads = dlg.Downloads;
                zipRoot.Note = dlg.Note;
                zipRoot.ProductID = dlg.ProductID;
                zipRoot.SpaceportURL = dlg.SpaceportURL;
                zipRoot.ForumURL = dlg.ForumURL;
                zipRoot.CurseForgeURL = dlg.CurseForgeURL;
                zipRoot.Version = dlg.Version;
                zipRoot.VersionControl = dlg.VersionControl;
            }
        }

        #endregion

        #region Internal classes

        /// <summary>
        /// Wrapper class for information of the gamedata folder scan.
        /// </summary>
        class ScanInfo
        {
            /// <summary>
            /// Parent scan info of this instance.
            /// </summary>
            public ScanInfo Parent = null;

            /// <summary>
            /// Child scan infos of this instance.
            /// </summary>
            public List<ScanInfo> Childs = new List<ScanInfo>();

            /// <summary>
            /// File or directory name.
            /// </summary>
            public string Name = string.Empty;

            /// <summary>
            /// Full path of the file/directory.
            /// </summary>
            public string Path = string.Empty;

            /// <summary>
            /// Flag that indicates wether this scan info is for a file or not.
            /// </summary>
            public bool IsFile = false;


            /// <summary>
            /// Cretes a new instance of the class ScanInfo.
            /// </summary>
            /// <param name="name">File or directory name.</param>
            /// <param name="path">Full path of the file/directory.</param>
            /// <param name="isFile">Flag that indicates wether this scan info is for a file or not.</param>
            /// <param name="parent">Parent of this instance.</param>
            public ScanInfo(string name, string path, bool isFile, ScanInfo parent = null)
            {
                Name = name;
                Path = path;
                IsFile = isFile;
                Parent = parent;

                if (Parent != null)
                    Parent.Childs.Add(this);
            }
        }

        #endregion
    }
}
