using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Utils;
using System.Text;
using KSPModAdmin.Utils.CommonTools;

namespace KSPModAdmin.Views
{
    /// <summary>
    /// Delegate for the ScanComplete event
    /// </summary>
    /// <param name="partList">The list of found parts.</param>
    public delegate void ScanCompleteHandler(List<TreeNodePart1> partList);

    /// <summary>
    /// The Parts view.
    /// </summary>
    public partial class ucParts : ucBase
    {
        /// <summary>
        /// Event ScanComplete occurs when the scan of parts is complete.
        /// </summary>
        public event ScanCompleteHandler ScanComplete;

        #region Member

        /// <summary>
        /// List of all parts.
        /// </summary>
        private List<TreeNodePart1> mPartNodes = new List<TreeNodePart1>();

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the ucParts class.
        /// </summary>
        public ucParts()
        {
            InitializeComponent();

            cbCategoryFilter.SelectedIndex = 0;
            cbModFilter.SelectedIndex = 0;
            tvParts.Nodes.Clear();
        }

        #endregion

        #region Public

        /// <summary>
        /// Refreshes the Parts list (scans the KSP install folder for parts).
        /// </summary>
        public void RefreshParts()
        {
            ScanDir();
        }

        /// <summary>
        /// Returns the count of parts.
        /// </summary>
        /// <returns>The count of parts.</returns>
        public int GetPartCount()
        {
            return mPartNodes.Count;
        }

        /// <summary>
        /// Returns a list of all parts.
        /// </summary>
        /// <returns>A list of all parts.</returns>
        public List<TreeNodePart1> GetListOfAllParts()
        {
            return mPartNodes;
        }

        /// <summary>
        /// Clears the parts TreeView.
        /// </summary>
        public void ClearTreeView()
        {
            mPartNodes.Clear();
            tvParts.Nodes.Clear();
        }

        #endregion

        #region Private

        #region Event handling

        /// <summary>
        /// Handels the Load event of the ucParts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucParts_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            tvParts.AddActionKey(VirtualKey.VK_RETURN, tvParts_EnterPressed);
            tvParts.AddActionKey(VirtualKey.VK_DELETE, tvParts_DeletePressed);
            tvParts.AddActionKey(VirtualKey.VK_BACK, tvParts_DeletePressed);

            tvParts.AddColumn("title", "Part/Craft", 150, 150);
            tvParts.AddColumn("category", "Category", 60, 60, true);
            tvParts.AddColumn("mod", "Mod", 100, 100);
            tvParts.AddColumn("name", "Name", 100, 100);
        }

        /// <summary>
        /// Handels the Click event of the btnRefresh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ScanDir();
        }

        /// <summary>
        /// Handels the Click event of the btnRemove.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (tvParts.FocusedNode == null) return;

            List<Node> nodes = tvParts.SelectedRootNodes;
            foreach (Node node in nodes) 
                RemovePart((TreeNodePart1)node);
        }

        /// <summary>
        /// Handels the Click event of the btnRename.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRename_Click(object sender, EventArgs e)
        {
            if (tvParts.FocusedNode == null) return;

            RenameCraft((TreeNodePart1)tvParts.FocusedNode);
        }

        /// <summary>
        /// Handels the Click event of the btnSwapBuilding.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeCategory_Click(object sender, EventArgs e)
        {
            if (tvParts.FocusedNode == null) return;

            ChangeCategory((TreeNodePart1)tvParts.FocusedNode);
        }

        /// <summary>
        /// Handels the Click event of the change category sub menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_Category_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            ChangeCategory((TreeNodePart1)tvParts.FocusedNode, item.Text);
        }

        /// <summary>
        /// Handels the DoubleClick event of the tvParts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvParts_DoubleClick(object sender, EventArgs e)
        {
            Node parent = tvParts.FocusedNode;

            if (parent == null) return;

            while (parent.Parent != null)
                parent = parent.Parent;

            TreeNodePart1 partNode = (TreeNodePart1)parent;
            string fullpath = partNode.FilePath.Replace("KSP Install Folder", MainForm.GetPath(KSP_Paths.KSPRoot));

            try
            {
                if (File.Exists(fullpath))
                {
                    TextDisplayer frm = new TextDisplayer();
                    frm.TextBox.Text = File.ReadAllText(fullpath);
                    frm.Text = partNode.PartName;
                    frm.ShowDialog(this);
                }
                else
                    MainForm.AddInfo("File not found \"" + fullpath + "\".");
            }
            catch (Exception ex)
            {
                MainForm.AddError("Error while reading \"" + fullpath + "\".", ex); // + Environment.NewLine + "Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Handels the EnterPressed event of the tvCrafts.
        /// </summary>
        /// <param name="keyState">Key state information.</param>
        /// <returns>True if key was handled otherwise false.</returns>
        private bool tvParts_EnterPressed(ActionKeyInfo keyState)
        {
            tvParts_DoubleClick(tvParts, null);
            return true;
        }

        /// <summary>
        /// Handels the DeletePressed event of the tvCrafts.
        /// </summary>
        /// <param name="keyState">Key state information.</param>
        /// <returns>True if key was handled otherwise false.</returns>
        private bool tvParts_DeletePressed(ActionKeyInfo keyState)
        {
            btnRemove_Click(tvParts, null);
            return true;
        }

        /// <summary>
        /// Handels the SelectedNodeChanged event of the tvParts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvParts_FocusedNodeChanged(object o, EventArgs e)
        {
            btnChangeCategory.Enabled = (tvParts.NodesSelection.Count == 1);
            btnRename.Enabled = (tvParts.NodesSelection.Count == 1);
            btnRemove.Enabled = (tvParts.FocusedNode != null);
        }

        /// <summary>
        /// Handels the SelectedIndexChanged event of the cbBuildingFilter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBuildingFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTreeView();
        }

        /// <summary>
        /// Handels the Opening event of the contextMenuStrip1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsParts_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool enable = (tvParts.FocusedNode != null);
            renameToolStripMenuItem.Enabled = enable;
            removeToolStripMenuItem.Enabled = enable;
            changeCategoryToolStripMenuItem.Enabled = enable;
        }
        
        #endregion

        /// <summary>
        /// Scans the KSP install dir and sub dirs for *.craft files.
        /// </summary>
        private void ScanDir()
        {
            InvokeIfRequired(() => InitScanDir());

            AsyncTask<bool>.DoWork(delegate()
                                   {
                                       string[] files = Directory.GetFiles(MainForm.GetPath(KSP_Paths.KSPRoot), "*.cfg", SearchOption.AllDirectories);
                                       if (files != null && files.Length > 0)
                                           foreach (string file in files)
                                               AddPartEntry(file);
                                       else
                                           MainForm.AddInfo(string.Format("No part.cfg files found \"{0}\".", MainForm.GetPath(KSP_Paths.KSPRoot)));

                                       return true;
                                   },
                                   delegate(bool result, Exception ex)
                                   {
                                       InvokeIfRequired(() => EndScanDir(ex));
                                   });
        }

        /// <summary>
        /// Initializes the directory scan.
        /// </summary>
        private void InitScanDir()
        {
            picLoading.Visible = true;
            btnRefresh.Enabled = false;
            btnRename.Enabled = false;
            btnChangeCategory.Enabled = false;
            btnRemove.Enabled = false;
            cbCategoryFilter.Enabled = false;
            cbModFilter.Enabled = false;
            tvParts.ReadOnly = true;

            mPartNodes.Clear();
            cbModFilter.Items.Clear();
            cbModFilter.Items.Add("All");
            cbModFilter.Items.Add("Squad");
            cbModFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// Ends the dorectpry scan.
        /// </summary>
        /// <param name="ex"></param>
        private void EndScanDir(Exception ex)
        {
            picLoading.Visible = false;
            btnRefresh.Enabled = true;
            cbCategoryFilter.Enabled = true;
            cbModFilter.Enabled = true;
            tvParts.ReadOnly = false;

            if (ex != null)
                MessageBox.Show("Error during part reading! \"" + ex.Message + "\"");
            else
                FillTreeView();

            tvParts.FocusedNode = null;

            if (ScanComplete != null)
                ScanComplete(GetListOfAllParts());
        }

        /// <summary>
        /// Fills the TreView depanden on the filter settings.
        /// </summary>
        private void FillTreeView()
        {
            InvokeIfRequired(() => tvParts.Nodes.Clear());

            //Sort by mod and name
            mPartNodes.Sort((p1, p2) => p1.PartTitle.CompareTo(p2.PartTitle));

            int count = 0;
            foreach (TreeNodePart1 node in mPartNodes)
                if ((cbCategoryFilter.SelectedIndex == 0 || node.Category.ToLower() == cbCategoryFilter.SelectedItem.ToString().ToLower()) &&
                    (cbModFilter.SelectedIndex == 0 || node.Mod.ToLower() == cbModFilter.SelectedItem.ToString().ToLower()))
                {
                    InvokeIfRequired(() => tvParts.Nodes.Add(node));
                    ++count;
                }
            
            lblCount.Text = string.Format("{0} ({1}) Parts", count, mPartNodes.Count);
        }

        /// <summary>
        /// Adds the passe part to the internal part list.
        /// </summary>
        /// <param name="file">fullpath to the part file.</param>
        private void AddPartEntry(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                MainForm.AddInfo(string.Format("Error during part reading.{0}Path is empty.", Environment.NewLine));
                return;
            }

            string[] lines = File.ReadLines(file).ToArray();
            if (lines == null || lines.Length == 0)
            {
                MainForm.AddInfo(string.Format("Error during part reading \"{0}\"{1}File content empty.", file, Environment.NewLine));
                return;
            }

            int braceCount = 0;
            bool isPartFile = false;
            bool isWithinPartDev = false;
            TreeNodePart1 partNode = null;
            foreach (string line in lines)
            {
                if (line == null)
                {
                    MainForm.AddError(string.Format("Error during part reading \"{0}\"{1}Enexpected 'null' line.", file, Environment.NewLine));
                    continue;
                }

                if (line.ToLower().Trim().StartsWith("part {") || line.ToLower().Trim().StartsWith("part{"))
                {
                    isPartFile = true;
                    isWithinPartDev = true;
                    braceCount += 1;
                    AddPartNode(partNode);
                    partNode = GetNewPartNode(file);
                }

                else if (line.ToLower().Trim().StartsWith("part"))
                    isPartFile = true;

                else if (line.Trim().StartsWith("{") || (!line.Contains("//") && line.Contains("{")))
                {
                    braceCount += 1;
                    if (braceCount == 1 && isPartFile)
                    {
                        isWithinPartDev = true;
                        AddPartNode(partNode);
                        partNode = GetNewPartNode(file);
                    }
                    else
                        isWithinPartDev = false;
                }

                else if (line.Trim().StartsWith("}"))
                {
                    braceCount -= 1;
                    if (braceCount == 1 && isPartFile)
                        isWithinPartDev = true;
                    else if (braceCount > 1)
                        isWithinPartDev = false;
                    else if (braceCount == 0)
                    {
                        isWithinPartDev = false;
                        AddPartNode(partNode);
                    }
                }

                if (isPartFile && isWithinPartDev)
                    ParsePartLine(file, line, ref partNode);
            }

            AddPartNode(partNode);
        }

        /// <summary>
        /// Parses the line for part informations.
        /// </summary>
        /// <param name="file">Full path to the part cfg file.</param>
        /// <param name="line">The line to parse.</param>
        /// <param name="partNode">The node to write the informations to.</param>
        private void ParsePartLine(string file, string line, ref TreeNodePart1 partNode)
        {
            string tempLine = line.Trim();

            // TODO: change name to title!
            if (tempLine.ToLower().StartsWith("name =") || tempLine.ToLower().StartsWith("name="))
            {
                string[] nameValuePair = tempLine.Split('=');
                if (nameValuePair.Length != 2)
                    MainForm.AddError(string.Format("Error during part reading \"{0}\"{1}Name / title parameter missmatch.", file, Environment.NewLine));

                else
                {
                    string name = nameValuePair[1].Trim();
                    partNode.PartTitle = name;
                    partNode.PartName = name;
                }
            }

            else if (tempLine.ToLower().StartsWith("title =") || tempLine.ToLower().StartsWith("title="))
            {
                string[] nameValuePair = tempLine.Split('=');
                if (nameValuePair.Length != 2)
                    MainForm.AddError(string.Format("Error during part reading \"{0}\"{1}Name / title parameter missmatch.", file, Environment.NewLine));

                else
                {
                    string name = nameValuePair[1].Trim();
                    partNode.PartTitle = name;
                }
            }

            else if (tempLine.ToLower().StartsWith("category =") || tempLine.ToLower().StartsWith("category="))
            {
                string[] nameValuePair = tempLine.Split('=');
                if (nameValuePair.Length != 2)
                    MainForm.AddError(string.Format("Error during part reading \"{0}\"{1}Name / title parameter missmatch.", file, Environment.NewLine));

                else
                {
                    int categoryIndex = -1;
                    string category = nameValuePair[1].Trim();
                    if (int.TryParse(category, out categoryIndex))
                        category = TranslateCategoryIndex(categoryIndex);
                    //partNode.Text = string.Format("{0} - {1}", partNode.PartName, partNode.Category);
                    partNode.Category = category;
                }
            }
        }

        /// <summary>
        /// Creates a new default TreeNodePart.
        /// </summary>
        /// <param name="file">The full path of the part cfg file.</param>
        /// <returns></returns>
        private TreeNodePart1 GetNewPartNode(string file)
        {
            TreeNodePart1 partNode = new TreeNodePart1();
            partNode.FilePath = file.Replace(MainForm.GetPath(KSP_Paths.KSPRoot), "KSP Install Folder");
            if (file.Contains("GameData"))
            {
                string mod = file.Substring(file.IndexOf("GameData") + 9);
                mod = mod.Substring(0, mod.IndexOf("\\"));
                partNode.Mod = mod;

                InvokeIfRequired(() => { if (!cbModFilter.Items.Contains(mod)) cbModFilter.Items.Add(mod); });
            }

            return partNode;
        }

        private void AddPartNode(TreeNodePart1 partNode)
        {
            if (partNode != null && !string.IsNullOrEmpty(partNode.PartName) && !mPartNodes.Contains(partNode))
                mPartNodes.Add(partNode);
        }

        /// <summary>
        /// Translate the category number to a category string.
        /// </summary>
        /// <param name="categoryIndex"></param>
        /// <returns></returns>
        private string TranslateCategoryIndex(int categoryIndex)
        {
            switch (categoryIndex)
            {
                case 0:
                    return "Propulsion";
                case 1:
                    return "Control";
                case 2:
                    return "Structural";
                case 3:
                    return "Aero";
                case 4:
                    return "Utility";
                case 5:
                    return "Science";
                case 6:
                    return "Pods";
            }

            return string.Empty;
        }

        /// <summary>
        /// Removes the part from KSP and unchecks it in the mod selection.
        /// </summary>
        /// <param name="partNode">The part node to remove.</param>
        private void RemovePart(TreeNodePart1 partNode)
        {
            string partFolder = GetPartFolder(partNode);
            string partPath = Path.GetDirectoryName(partNode.FilePath).Replace("KSP Install Folder", MainForm.GetPath(KSP_Paths.KSPRoot));
            TreeNodeMod node = MainForm.ModSelection.SearchNode(partFolder);

            DialogResult dlgResult = DialogResult.Cancel;
            if (node == null)
                dlgResult = MessageBox.Show(this, "The part you are trying to delete is not from a mod.\n\rDo you want to delete the part permanetly?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (partNode.Nodes != null && partNode.Nodes.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("The part you are trying to delete is used by the following craft(s):");
                foreach (Node tempNode in partNode.Nodes)
                    sb.AppendFormat("- {0}{1}", tempNode.Text, Environment.NewLine);
                sb.AppendLine();
                sb.AppendLine("Delete it anyway?");
                dlgResult = MessageBox.Show(this, sb.ToString(), "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            if ((node != null || dlgResult == DialogResult.Yes) && Directory.Exists(partPath))
            {
                Directory.Delete(partPath, true);

                if (node != null)
                {
                    if (partNode.Nodes != null)
                    {
                        foreach (Node n in partNode.Nodes)
                            ((TreeNodeCraft)n.Tag).RemovePartRelation(partNode);
                    }

                    node.Checked = false;
                    node.NodeType = NodeType.UnknownFolder;
                    foreach (TreeNodeMod child in node.Nodes)
                    {
                        child.Checked = false;
                        child.NodeType = child.IsFile ? NodeType.UnknownFile : NodeType.UnknownFolder;
                    }
                }

                tvParts.Nodes.Remove(partNode);
            }
        }

        /// <summary>
        /// Asks the user for a new name for the part and renames it. 
        /// </summary>
        /// <param name="partNode">The part node to rename.</param>
        private void RenameCraft(TreeNodePart1 partNode)
        {
            frmNameSelection dlg = new frmNameSelection();
            dlg.Description = "Please choose a new name (ATTANTION: This may corrupting crafts!).";
            dlg.NewName = partNode.PartName;
            dlg.KnownNames = GetListOfPartNames();
            if (dlg.ShowDialog(MainForm) == DialogResult.OK)
            {
                string fullPath = partNode.FilePath.Replace("KSP Install Folder", MainForm.GetPath(KSP_Paths.KSPRoot));
                if (File.Exists(fullPath))
                {
                    string allText = File.ReadAllText(fullPath);
                    string newText = allText.Replace("name = " + partNode.PartName, "name = " + dlg.NewName);
                    File.WriteAllText(fullPath, newText);
                    partNode.PartName = dlg.NewName;
                    partNode.Text = partNode.ToString();
                }
            }
        }

        /// <summary>
        /// Changes the category of the part.
        /// </summary>
        /// <param name="partNode">The node of the part to change the category from.</param>
        /// <param name="newCategory">The new category to set the partNode to.</param>
        private void ChangeCategory(TreeNodePart1 partNode, string newCategory = "")
        {
            frmPartCategorySelection dlg = new frmPartCategorySelection();
            dlg.Category = partNode.Category;
            if (newCategory != string.Empty || dlg.ShowDialog(MainForm) == DialogResult.OK)
            {
                string category = newCategory;
                if (newCategory == string.Empty)
                    category = dlg.Category;
                string fullPath = partNode.FilePath.Replace("KSP Install Folder", MainForm.GetPath(KSP_Paths.KSPRoot));
                if (File.Exists(fullPath))
                {
                    string allText = File.ReadAllText(fullPath);
                    string newText = allText.Replace("category = " + partNode.Category, "category = " + category);
                    File.WriteAllText(fullPath, newText);
                    partNode.Category = category;

                    foreach (Utils.CommonTools.Node node in partNode.Nodes)
                    {
                        if (node.Text.StartsWith("Category = "))
                        {
                            node.Text = "Category = " + partNode.Category;
                            break;
                        }
                    }
                }
            }
            tvParts.Invalidate();
        }

        /// <summary>
        /// Returns a list of all part names.
        /// </summary>
        /// <returns>A list of all part names.</returns>
        private List<string> GetListOfPartNames()
        {
            return (from TreeNodePart1 part in tvParts.Nodes select part.PartName).ToList();
        }

        /// <summary>
        /// Returns the part folder where the part.cfg lies.
        /// </summary>
        /// <param name="partNode">The part to get the folder from.</param>
        /// <returns>The part folder where the part.cfg lies.</returns>
        private string GetPartFolder(TreeNodePart1 partNode)
        {
            string path = partNode.FilePath.Substring(0, partNode.FilePath.LastIndexOf("\\"));
            path = path.Substring(path.LastIndexOf("\\") + 1);

            return path;
        }

        #endregion
    }
}
