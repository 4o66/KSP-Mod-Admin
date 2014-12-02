using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Utils;

namespace KSPModAdmin.Views
{
    /// <summary>
    /// The Crafts view.
    /// </summary>
    public partial class ucCrafts : ucBase
    {
        #region Member

        /// <summary>
        /// List of all crafts.
        /// </summary>
        private List<TreeNodeCraft> mCraftNodes = new List<TreeNodeCraft>();

        #endregion

        #region Constructors

        /// <summary>
        /// Creaes a new instance of the ucCrafts class.
        /// </summary>
        public ucCrafts()
        {
            InitializeComponent();
        }

        #endregion

        #region Public

        /// <summary>
        /// Refreshes the crafts list (scans KSP install folder for crafts).
        /// </summary>
        public void RefreshCrafts()
        {
            ScanDir();
        }

        /// <summary>
        /// Clears the crafts TreeView.
        /// </summary>
        public void ClearTreeView()
        {
            mCraftNodes.Clear();
            tvCrafts.Nodes.Clear();
        }

        #endregion

        #region Private

        #region Event handling

        /// <summary>
        /// Handels the Load event of the ucCrafts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCrafts_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            cbBuildingFilter.SelectedIndex = 0;

            tvCrafts.Nodes.Clear();

            tvCrafts.AddActionKey(VirtualKey.VK_RETURN, tvCrafts_EnterPressed);
            tvCrafts.AddActionKey(VirtualKey.VK_DELETE, tvCrafts_DeletePressed);
            tvCrafts.AddActionKey(VirtualKey.VK_BACK, tvCrafts_DeletePressed);

            tvCrafts.AddColumn("craft", "Craft/Part", 180, 180);
            tvCrafts.AddColumn("type", "Type", 40, 40, true, true);
            tvCrafts.AddColumn("folder", "Folder", 60, 60, true);
            tvCrafts.AddColumn("version", "Version", 45, 45, true);
            tvCrafts.AddColumn("mods", "Mods", 120, 120);

            MainForm.Parts.ScanComplete += new ScanCompleteHandler(Parts_ScanComplete);
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
            if (tvCrafts.FocusedNode == null) return;

            List<KSPModAdmin.Utils.CommonTools.Node> nodes = tvCrafts.SelectedRootNodes;
            foreach (TreeNodeCraft node in nodes)
                RemoveCraft((TreeNodeCraft)node);
        }

        /// <summary>
        /// Handels the Click event of the btnRename.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRename_Click(object sender, EventArgs e)
        {
            if (tvCrafts.FocusedNode == null) return;

            RenameCraft((TreeNodeCraft)tvCrafts.FocusedNode);
        }

        /// <summary>
        /// Handels the Click event of the btnSwapBuilding.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwapBuilding_Click(object sender, EventArgs e)
        {
            if (tvCrafts.FocusedNode == null) return;

            SwapCraftBuilding((TreeNodeCraft)tvCrafts.FocusedNode);
        }

        /// <summary>
        /// Handels the Click event of the btnValidate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValidate_Click(object sender, EventArgs e)
        {
            ValidateCrafts();
        }

        /// <summary>
        /// Handles the DoubleClick event of the tvCrafts TreeView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvCrafts_DoubleClick(object sender, EventArgs e)
        {
            TreeNodeCraft parent = (TreeNodeCraft)tvCrafts.FocusedNode;

            if (parent == null) return;

            string fullpath = string.Empty;
            string title = string.Empty;
            try
            {
                if (parent.IsPartInfo)
                {
                    if (((TreeNodeCraft)parent).RelatedPart != null)
                    {
                        fullpath = ((TreeNodeCraft)parent).RelatedPart.FilePath.Replace("KSP Install Folder", MainForm.GetPath(KSP_Paths.KSPRoot));
                        title = ((TreeNodeCraft)parent).Text;
                    }
                }
                else
                {
                    while (parent.Parent != null)
                        parent = (TreeNodeCraft)parent.Parent;

                    fullpath = ((TreeNodeCraft)parent).FilePath.Replace("KSP Install Folder", MainForm.GetPath(KSP_Paths.KSPRoot));
                    title = ((TreeNodeCraft)parent).Text;
                }

                if (File.Exists(fullpath))
                {
                    TextDisplayer frm = new TextDisplayer();
                    frm.TextBox.Text = File.ReadAllText(fullpath);
                    frm.Text = title;
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
        private bool tvCrafts_EnterPressed(ActionKeyInfo keyState)
        {
            tvCrafts_DoubleClick(tvCrafts, null);
            return true;
        }

        /// <summary>
        /// Handels the DeletePressed event of the tvCrafts.
        /// </summary>
        /// <param name="keyState">Key state information.</param>
        /// <returns>True if key was handled otherwise false.</returns>
        private bool tvCrafts_DeletePressed(ActionKeyInfo keyState)
        {
            btnRemove_Click(tvCrafts, null);
            return true;
        }

        /// <summary>
        /// Handels the DrawNode event of the tvCrafts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvCrafts_CustomDrawCell(object o, Utils.CommonTools.CustomDrawCellEventArgs e)
        {
            TreeNodeCraft node = (TreeNodeCraft)e.Node;

            Color textColor = SystemColors.WindowText;
            if (node.IsInvalidOrHasInvalidChilds && e.Column.Index == 0)
                textColor = Color.FromArgb(255, 0, 0);
            
            TextRenderer.DrawText(e.Graphics,
                                  e.Node.Data[e.Column.Index].ToString(),
                                  tvCrafts.Font,
                                  e.Bounds,
                                  textColor,
                                  Color.Empty,
                                  TextFormatFlags.VerticalCenter);
        }

        /// <summary>
        /// Handels the SelectedNodeChanged event of the tvParts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvCrafts_FocusedNodeChanged(object o, EventArgs e)
        {
            btnSwapBuilding.Enabled = (tvCrafts.NodesSelection.Count == 1);
            btnRename.Enabled = (tvCrafts.NodesSelection.Count == 1);
            btnRemove.Enabled = (tvCrafts.FocusedNode != null);
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
        private void cmsCrafts_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool enable = (tvCrafts.FocusedNode != null);
            renameToolStripMenuItem.Enabled = enable;
            swapToolStripMenuItem.Enabled = enable;
            removeToolStripMenuItem.Enabled = enable;
        }
        
        #endregion 

        /// <summary>
        /// Scans the KSP install dir and sub dirs for *.craft files.
        /// </summary>
        private void ScanDir()
        {
            picLoading.Visible = true;
            btnRefresh.Enabled = false;
            btnRename.Enabled = false;
            btnSwapBuilding.Enabled = false;
            btnValidate.Enabled = false;
            btnRemove.Enabled = false;
            cbBuildingFilter.Enabled = false;
            tvCrafts.ReadOnly = true;

            mCraftNodes.Clear();

            AsyncTask<bool>.DoWork(delegate()
                                   {
                                       string[] files = Directory.GetFiles(MainForm.GetPath(KSP_Paths.KSPRoot), "*.craft", SearchOption.AllDirectories);
                                       foreach (string file in files)
                                           AddCraftEntry(file);

                                       return true;
                                   },
                                   delegate(bool result, Exception ex)
                                   {
                                       if (ex != null)
                                           MessageBox.Show("Error during craft reading! \"" + ex.Message + "\"");
                                       else
                                           ValidateCrafts();
                                   });
        }

        /// <summary>
        /// Fills the TreView depanden on the filter settings.
        /// </summary>
        private void FillTreeView()
        {
            int count = 0;
            InvokeIfRequired(() => tvCrafts.Nodes.Clear());

            mCraftNodes.Sort((c1, c2) => c1.Text.CompareTo(c2.Text));
            foreach (TreeNodeCraft node in mCraftNodes)
                if (cbBuildingFilter.SelectedIndex == 0 || node.Type.ToLower().Contains(cbBuildingFilter.SelectedItem.ToString().ToLower()))
                {
                    InvokeIfRequired(() => tvCrafts.Nodes.Add(node));
                    ++count;
                }

            lblCount.Text = string.Format("{0} ({1}) Crafts", count, mCraftNodes.Count);
        }

        /// <summary>
        /// Adds the passe craft to the internal craft list.
        /// </summary>
        /// <param name="file">fullpath to the craft file.</param>
        private void AddCraftEntry(string file)
        {
            string adjustedPath = file.Replace(MainForm.GetPath(KSP_Paths.KSPRoot), "KSP Install Folder");

            TreeNodeCraft craftNode = new TreeNodeCraft();
            craftNode.Name = file;
            craftNode.FilePath = file;
            craftNode.Folder = GetCraftFolder(adjustedPath);
            mCraftNodes.Add(craftNode);

            bool partInfo = false;
            int bracetCount = 0;
            string[] lines = File.ReadLines(file).ToArray<string>();
            foreach (string line in lines)
            {
                string tempLine = line.Trim();
                if (!partInfo)
                {
                    if (tempLine.ToLower().StartsWith("ship =") || tempLine.ToLower().StartsWith("ship="))
                    {
                        string name = tempLine.Split('=')[1];
                        craftNode.Text = name.Trim();
                        craftNode.CraftName = name.Trim();
                    }

                    else if (tempLine.ToLower().StartsWith("type =") || tempLine.ToLower().StartsWith("type="))
                    {
                        string type = tempLine.Split('=')[1];
                        craftNode.Type = type.Trim();
                    }

                    else if (tempLine.ToLower().StartsWith("version =") || tempLine.ToLower().StartsWith("version="))
                    {
                        string version = tempLine.Split('=')[1];
                        craftNode.Version = version.Trim();
                    }

                    else if (tempLine.ToLower().StartsWith("part"))
                    {
                        partInfo = true;
                    }
                }
                else
                {
                    if (tempLine.StartsWith("{"))
                        ++bracetCount;

                    else if (tempLine.StartsWith("}"))
                    {
                        --bracetCount;
                        if (bracetCount < 1)
                            partInfo = false;
                    }

                    else if (tempLine.ToLower().StartsWith("part =") || tempLine.ToLower().StartsWith("part="))
                    {
                        string partName = tempLine.Split('=')[1].Trim();
                        partName = partName.Substring(0, partName.LastIndexOf("_"));
                        if (!craftNode.Nodes.ContainsKey(partName))
                            craftNode.Nodes.Add(new TreeNodeCraft(partName, partName + " (1)"));
                        else
                        {
                            TreeNodeCraft part = (TreeNodeCraft)craftNode.Nodes[partName];
                            int length = part.Text.Length - part.Text.IndexOf(')');
                            int index = part.Text.IndexOf(')');

                            while (index >= 0 && part.Text[index] != '(')
                                --index;

                            string str = part.Text.Substring(index + 1, length);
                            int i = int.Parse(str) + 1;
                            part.Text = partName + " (" + i.ToString() + ")";
                        }
                    }
                }
            }
            craftNode.SortPartsByDisplayText();
        }

        /// <summary>
        /// Extracts the first folder name of the path.
        /// </summary>
        /// <param name="adjustedPath">The path to get the first foldernam  of.</param>
        /// <returns>The first folder name of the path.</returns>
        private static string GetCraftFolder(string path)
        {
            string folder = string.Empty;
            folder = path.Substring(path.IndexOf('\\') + 1);
            folder = folder.Substring(0, folder.IndexOf('\\'));
            return folder;
        }

        /// <summary>
        /// Removes the craft from KSP and unchecks it in the mod selection.
        /// </summary>
        /// <param name="treeNode">The craft node to remove.</param>
        private void RemoveCraft(TreeNodeCraft craftNode)
        {
            string craftPath = craftNode.FilePath.Replace("KSP Install Folder", MainForm.GetPath(KSP_Paths.KSPRoot));
            TreeNodeMod node = MainForm.ModSelection.SearchNode(Path.GetFileName(craftNode.FilePath));

            DialogResult dlgResult = DialogResult.Cancel;
            if (node == null)
                dlgResult = MessageBox.Show(this, "The craft you are trying to delete is not from a mod.\n\rDo you want to delete the craft permanetly?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (node != null || dlgResult == DialogResult.Yes)
            {
                if (File.Exists(craftPath))
                {
                    File.Delete(craftPath);
                    if (node != null)
                    {
                        node.Checked = false;
                        node.NodeType = NodeType.UnknownFile;
                    }

                    tvCrafts.Nodes.Remove(craftNode);

                    foreach (TreeNodeCraft pNode in craftNode.Nodes)
                    {
                        if (pNode.RelatedPart != null)
                            pNode.RelatedPart.RemoveCraft(craftNode);
                    }
                }
            }
        }

        /// <summary>
        /// Asks the user for a new name for the craft and renames it. 
        /// </summary>
        /// <param name="craftNode">The craft node to rename.</param>
        private void RenameCraft(TreeNodeCraft craftNode)
        {
            throw new NotImplementedException();

            //Point pos = Point.Empty;
            //tvCrafts.OpenEditorAtLocation(pos);

            //frmNameSelection dlg = new frmNameSelection();
            //dlg.NewName = craftNode.Text;
            //dlg.KnownNames = GetListOfCraftNames();
            //if (dlg.ShowDialog(MainForm) == DialogResult.OK)
            //{
            //    string fullPath = craftNode.FilePath.Replace("KSP Install Folder", MainForm.GetPath(KSP_Paths.KSP_Root));
            //    if (File.Exists(fullPath))
            //    {
            //        string allText = File.ReadAllText(fullPath);
            //        string newText = allText.Replace("ship = " + craftNode.Text, "ship = " + dlg.NewName);
            //        File.WriteAllText(fullPath, newText);
            //        craftNode.Text = dlg.NewName;
            //    }
            //}
        }

        /// <summary>
        /// Swaps the related building of the craft.
        /// </summary>
        /// <param name="treeNode">The craft node to swap the building from.</param>
        private void SwapCraftBuilding(TreeNodeCraft craftNode)
        {
            string fullPath = craftNode.FilePath.Replace("KSP Install Folder", MainForm.GetPath(KSP_Paths.KSPRoot));
            if (File.Exists(fullPath))
            {
                string newType = GetOtherBuilding(craftNode.Type);
                string allText = File.ReadAllText(fullPath);
                string newText = allText.Replace("type = " + craftNode.Type, "type = " + newType);
                string newPath = GetNewPath(craftNode, newType);
                File.WriteAllText(fullPath, newText);
                File.Move(fullPath, newPath);
                craftNode.Type = newType;
                craftNode.FilePath = newPath;

                tvCrafts.Invalidate();
            }
        }

        /// <summary>
        /// Checks if all parts of the craft are installed.
        /// </summary>
        /// <param name="treeNode">The craft node to validate.</param>
        private void ValidateCrafts()
        {
            btnRefresh.Enabled = false;
            btnRemove.Enabled = false;
            btnRename.Enabled = false;
            btnSwapBuilding.Enabled = false;
            btnValidate.Enabled = false;
            cbBuildingFilter.Enabled = false;
            picLoading.Visible = true;
            tvCrafts.ReadOnly = true;

            AsyncTask<bool>.DoWork(delegate()
                                   {
                                       MainForm.Parts.RefreshParts();
                                       return true;
                                   },
                                   delegate(bool result, Exception ex)
                                   {
                                       Parts_ScanComplete(MainForm.Parts.GetListOfAllParts());
                                   });
        }

        /// <summary>
        /// Checks if all parts of the craft are installed.
        /// </summary>
        /// <param name="partList">The list of installed parts.</param>
        private void Parts_ScanComplete(List<TreeNodePart1> partList)
        {
            AsyncTask<bool>.DoWork(delegate()
            {
                InvokeIfRequired(() => tvCrafts.Nodes.Clear());

                foreach (TreeNodeCraft craft in mCraftNodes)
                {
                    Dictionary<string, TreeNodeCraft> alreadyCheckedParts = new Dictionary<string, TreeNodeCraft>();
                    foreach (TreeNodeCraft part in craft.Nodes)
                    {
                        if (!part.IsPartInfo) continue;

                        string partName = part.Text.Substring(0, part.Text.IndexOf(" ("));
                        string count = part.Text.Substring(partName.Length);
                        if (alreadyCheckedParts.ContainsKey(part.Text))
                        {
                            if ((TreeNodeCraft)alreadyCheckedParts[part.Text] != null)
                            {
                                part.RelatedPart = ((TreeNodeCraft)alreadyCheckedParts[part.Text]).RelatedPart;
                                part.Text = part.RelatedPart.PartTitle + count;
                                craft.AddMod(part.RelatedPart.Mod);
                            }
                            continue;
                        }
                        else
                        {
                            bool found = false;
                            foreach (TreeNodePart1 instPart in partList)
                            {
                                if (instPart.PartName.Replace("_", ".") == partName)
                                {
                                    if (!alreadyCheckedParts.ContainsKey(instPart.PartName))
                                        alreadyCheckedParts.Add(instPart.PartName, part);
                                    part.Text = instPart.PartTitle + count;
                                    part.RelatedPart = instPart;
                                    part.AddMod(instPart.Mod);
                                    InvokeIfRequired(() => part.RelatedPart.AddRelatedCraft(craft));
                                    craft.AddMod(instPart.Mod);
                                    found = true;
                                    break;
                                }
                            }

                            if (!found)
                                alreadyCheckedParts.Add(part.Text, null);
                        }
                    }

                    craft.SortPartsByDisplayText();
                }

                return true;
            },
            delegate(bool result, Exception ex)
            {
                InvokeIfRequired(() => FinishScanComplete(ex));
            });
        }

        /// <summary>
        /// Called to finish the Parts_ScanComplete callback.
        /// </summary>
        /// <param name="ex"></param>
        private void FinishScanComplete(Exception ex)
        {
            if (ex != null)
                MessageBox.Show(ParentForm, string.Format("Error during craft validating. \"{0}\"", ex.Message));
            else
            {
                FillTreeView();

                btnRefresh.Enabled = true;
                btnValidate.Enabled = true;
                cbBuildingFilter.Enabled = true;
                picLoading.Visible = false;
                tvCrafts.ReadOnly = false;

                tvCrafts.FocusedNode = null;

                //tvCrafts.Invalidate();
            }
        }

        /// <summary>
        /// Returns a list of all craft names.
        /// </summary>
        /// <returns>A list of all craft names.</returns>
        private List<string> GetListOfCraftNames()
        {
            List<string> list = new List<string>();

            foreach (TreeNodeCraft craft in tvCrafts.Nodes)
                list.Add(craft.Text);

            return list;
        }

        /// <summary>
        /// Returns the other vehicle building.
        /// </summary>
        /// <param name="building">Current vehicle building.</param>
        /// <returns>The other vehicle building.</returns>
        private string GetOtherBuilding(string building)
        {
            if (building.ToLower() == Constants.VAB)
                return Constants.SPH.ToUpper();
            else
                return Constants.VAB.ToUpper();
        }

        /// <summary>
        /// Returns the new path for the craft.
        /// </summary>
        /// <param name="craftNode">The CraftNode to get a new path for.</param>
        /// <param name="fullPath">The fullpath of the craft file.</param>
        /// <param name="newType">the new type of the craft.</param>
        /// <returns>The new path for the craft.</returns>
        private string GetNewPath(TreeNodeCraft craftNode, string newType)
        {
            string fullPath = craftNode.FilePath.Replace("KSP Install Folder", MainForm.GetPath(KSP_Paths.KSPRoot));
            int index = fullPath.ToLower().IndexOf("\\" + craftNode.Type.ToLower() + "\\");

            if (index > -1)
            {
                string start = fullPath.Substring(0, index + 1);
                string end = fullPath.Substring(index + 5);

                return Path.Combine(Path.Combine(start, newType.ToUpper()), end);
            }
            else
                return fullPath;
        }

        /// <summary>
        /// Returns the craft folder where the part.cfg lies.
        /// </summary>
        /// <param name="partNode">The craft to get the folder from.</param>
        /// <returns>The craft folder where the part.cfg lies.</returns>
        private string GetCraftFolder(TreeNodePart1 craftNode)
        {
            string path = craftNode.FilePath.Substring(0, craftNode.FilePath.LastIndexOf("\\"));
            path = path.Substring(path.LastIndexOf("\\") + 1);

            return path;
        }

        #endregion
    }
}
