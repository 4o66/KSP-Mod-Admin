using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace KSPModAdmin.Views
{
    public partial class ucFlags : ucBase
    {
        #region Members

        /// <summary>
        /// List of all flags by group.
        /// </summary>
        List<KeyValuePair<string, ListViewItem>> m_Flags = new List<KeyValuePair<string, ListViewItem>>();

        /// <summary>
        /// Flag to determine if a filter index change schould be ingored.
        /// </summary>
        bool m_IgnoreIndexChange = false;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the count of listed flags.
        /// </summary>
        public int FlagCount { get { return lvFlags.Items.Count; } }

        #endregion

        #region Constructor

        public ucFlags()
        {
            InitializeComponent();
        }

        #endregion

        #region Public

        /// <summary>
        /// Refreshes the Flags tab. 
        /// Searches the KSP install dir for flags and adds them to the ListView.
        /// </summary>
        public void RefreshFlagTab()
        {
            MainForm.AddInfo("Refreshing flag tab ...");

            lvFlags.Items.Clear();
            lvFlags.Groups.Clear();

            ilFlags.Images.Clear();

            m_IgnoreIndexChange = true;

            string lastFilter = (string)cbFlagFilter.SelectedItem;
            cbFlagFilter.Items.Clear();
            cbFlagFilter.Items.Add("All");
            cbFlagFilter.Items.Add("MyFlags");

            m_Flags.Clear();
            lvFlags.Items.Clear();

            if (MainForm.KSPPath.Contains("->")) return;

            btnFlagDelete.Enabled = false;
            btnFlagRefresh.Enabled = false;
            btnFlagImport.Enabled = false;
            cbFlagFilter.Enabled = false;
            pbFlagsLoad.Visible = true;
            AsyncTask<bool>.DoWork(delegate()
            {
                SearchDir4FlagsDirs(MainForm.GetPath(KSP_Paths.KSPRoot));
                return true;
            },
            delegate(bool result, Exception ex)
            {
                if (ex != null)
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (lastFilter != null && (lastFilter == "All" || lastFilter == "MyFlags" || GetGroup(lvFlags, lastFilter) != null))
                    cbFlagFilter.SelectedItem = lastFilter;
                else
                    cbFlagFilter.SelectedIndex = 0;

                btnFlagDelete.Enabled = true;
                btnFlagRefresh.Enabled = true;
                btnFlagImport.Enabled = true;
                cbFlagFilter.Enabled = true;
                pbFlagsLoad.Visible = false;
                m_IgnoreIndexChange = false;

                FillListView();
            });
        }

        /// <summary>
        /// Clears the flags list view.
        /// </summary>
        public void ClearFlags()
        {
            lvFlags.Items.Clear();
        }

        #endregion

        #region Private

        #region Event handling

        /// <summary>
        /// Handels the Load event of the ucFlags.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucFlags_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // preselect Filter for Flags tab (all)
            cbFlagFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// Handels the SelectedIndexChanged event of the cbFlagFilter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbFlagFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_IgnoreIndexChange) return;
            FillListView();
        }

        /// <summary>
        /// Handels the Click event of the btnFlagRefresh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFlagRefresh_Click(object sender, EventArgs e)
        {
            RefreshFlagTab();
        }

        /// <summary>
        /// Handels the Click event of the btnFlagImport.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFlagImport_Click(object sender, EventArgs e)
        {
            ImportFlag();
        }

        /// <summary>
        /// Handels the Click event of the btnFlagDelete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFlagDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedFlag();
        }

        #endregion

        #region Refresh

        /// <summary>
        /// Searches the dir and all subdirs for flags.
        /// </summary>
        /// <param name="dir">The directory to sreach.</param>
        private void SearchDir4FlagsDirs(string dir)
        {
            if (dir == string.Empty) return;

            try
            {
                if (dir.ToLower().EndsWith("flags"))
                    SearchFlags(dir);

                string[] subdirs = Directory.GetDirectories(dir);
                foreach (string subdir in subdirs)
                    SearchDir4FlagsDirs(subdir);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Searches the directory for *.png.
        /// </summary>
        /// <param name="dir">The directory to sreach.</param>
        private void SearchFlags(string dir)
        {
            foreach (string file in Directory.GetFiles(dir))
            {
                if (Path.GetExtension(file).ToLower() == ".png")
                    AddFlagToList(file);
            }
        }

        /// <summary>
        /// Adds a Flag to the internal list of flags.
        /// </summary>
        /// <param name="file">Fullpath to the Flag file.</param>
        private void AddFlagToList(string file)
        {
            if (File.Exists(file))
            {
                Image image = Image.FromFile(file);
                ilFlags.Images.Add(image);

                ListViewItem item = new ListViewItem();
                item.ImageIndex = ilFlags.Images.Count - 1;
                item.Text = Path.GetFileNameWithoutExtension(file);
                item.Tag = file;
                m_Flags.Add(new KeyValuePair<string, ListViewItem>(GetGroupName(file), item));
            }
        }

        /// <summary>
        /// Gets the name of the group from filename.
        /// (The Name of the Parent.Parent directory of the file)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string GetGroupName(string file)
        {
            string result = Path.GetDirectoryName(file);
            result = result.Substring(0, result.ToLower().Replace("\\flags", "").Length);
            result = result.Substring(result.LastIndexOf("\\") + 1);
            return result;
        }

        /// <summary>
        /// Gets a existing ListViewGroup or creates a new one.
        /// </summary>
        /// <param name="lvFlags">The ListView.</param>
        /// <param name="groupName">The name of the group.</param>
        /// <returns>A existing ListViewGroup or new created one.</returns>
        private ListViewGroup GetGroup(ListView lvFlags, string groupName)
        {
            foreach (ListViewGroup group in lvFlags.Groups)
                if (group.Name.ToLower() == groupName.ToLower())
                    return group;

            ListViewGroup newGroup = new ListViewGroup();
            newGroup.Header = groupName;
            newGroup.Name = groupName;

            bool found = false;
            foreach (string group in cbFlagFilter.Items)
            {
                if (group.ToLower() == groupName.ToLower())
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                cbFlagFilter.Items.Add(groupName);

            return newGroup;
        }

        /// <summary>
        /// Fills the Listview depanded on filter settings.
        /// </summary>
        private void FillListView()
        {
            lvFlags.Items.Clear();
            foreach (KeyValuePair<string, ListViewItem> pair in m_Flags)
                if (cbFlagFilter.SelectedItem != null &&
                    (((string)cbFlagFilter.SelectedItem) == "All" ||
                     ((string)cbFlagFilter.SelectedItem).ToLower() == pair.Key.ToLower()))
                {
                    ListViewGroup group = GetGroup(lvFlags, pair.Key);
                    lvFlags.Groups.Add(group);
                    group.Items.Add(pair.Value);
                    lvFlags.Items.Add(pair.Value);
                }
        }

        #endregion

        #region ImportFlag

        /// <summary>
        /// Starts the Import of a new flag.
        /// </summary>
        private void ImportFlag()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Constants.IMAGE_FILTER;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                string filename = dlg.FileName;

                Image image = null;
                try
                {
                    string path = MainForm.GetPathByName(Constants.GAMEDATA);
                    if (path == string.Empty)
                    {
                        MainForm.AddInfo("Invalid KSP path.");
                        return;
                    }

                    // Create .../GameData is not exist.
                    if (!Directory.Exists(path))
                    {
                        MainForm.AddInfo("Creating directory \".../GameData\".");
                        Directory.CreateDirectory(path);
                    }

                    // Create .../MyFlgas/Flags is not exist.
                    path = MainForm.GetPathByName(Constants.MYFLAGS);
                    if (!Directory.Exists(path))
                    {
                        MainForm.AddInfo("Creating directory \".../MyFlgas/Flags\".");
                        path = MainForm.GetPathByName(Constants.MYFLAGS).Replace("\\" + Constants.FLAGS, "");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        // Forlder must named like "Flags" case sensitive!!!!
                        path += "\\" + Constants.FLAGS[0].ToString().ToUpper();
                        path += Constants.FLAGS.Substring(1);
                        Directory.CreateDirectory(path);
                    }

                    // delete file with same name.
                    string savePath = Path.Combine(MainForm.GetPathByName(Constants.MYFLAGS), Path.GetFileNameWithoutExtension(filename) + ".png");
                    if (File.Exists(savePath))
                    {
                        MainForm.AddInfo(string.Format("Deleting existing flag \"{0}\".", savePath));
                        File.Delete(savePath);
                    }

                    // save image with max flag size to gamedata/myflags/flags/.
                    image = Image.FromFile(filename);
                    if (image.Size.Width != Constants.FLAG_WIDTH || image.Size.Height != Constants.FLAG_HEIGHT)
                    {
                        MainForm.AddInfo("Adjusting flag size ...");
                        Bitmap newImage = new Bitmap(Constants.FLAG_WIDTH, Constants.FLAG_HEIGHT);
                        using (Graphics graphicsHandle = Graphics.FromImage(newImage))
                        {
                            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphicsHandle.DrawImage(image, 0, 0, Constants.FLAG_WIDTH, Constants.FLAG_HEIGHT);
                        }
                        MainForm.AddInfo("Saving flag ...");
                        newImage.Save(savePath, ImageFormat.Png);
                        image = newImage; // Image.FromFile(savePath);
                    }
                    else
                    {
                        MainForm.AddInfo("Saving flag ...");
                        image.Save(savePath, ImageFormat.Png);
                    }
                }
                catch (Exception ex)
                {
                    image = null;
                    MessageBox.Show(this, "Error during flag creation. \"" + ex.Message + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (image != null)
                {
                    string flagname = Path.GetFileNameWithoutExtension(filename);
                    m_Flags.Add(new KeyValuePair<string, ListViewItem>("MyFlags", CreateNewFlagItem(flagname, image, filename)));
                }

                RefreshFlagTab();
            }
        }

        /// <summary>
        /// Creates a new ListViewItem for a flag.
        /// </summary>
        /// <param name="flagname">Name of the Flag.</param>
        /// <param name="image">The image of the flag.</param>
        /// <param name="filename">Fullpath of the flag file.</param>
        /// <returns></returns>
        private ListViewItem CreateNewFlagItem(string flagname, Image image, string filename)
        {
            ilFlags.Images.Add(image);

            ListViewItem lvItem = new ListViewItem();
            lvItem.Text = flagname;
            lvItem.ImageIndex = ilFlags.Images.Count - 1;
            lvItem.Group = GetGroup(lvFlags, Constants.MYFLAGS_GROUP);
            lvItem.Tag = filename;

            return lvItem;
        }

        #endregion

        #region DeleteFlag

        /// <summary>
        /// Deletes selected flags (Myflags only).
        /// </summary>
        private void DeleteSelectedFlag()
        {
            if (lvFlags.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in lvFlags.SelectedItems)
                {
                    string filename = (string)item.Tag;
                    try
                    {
                        if (File.Exists(filename))
                        {
                            // delete file
                            File.Delete(filename);

                            // remove pic from ListView
                            KeyValuePair<string, ListViewItem> pair2Del = new KeyValuePair<string, ListViewItem>("", null);
                            foreach (KeyValuePair<string, ListViewItem> pair in m_Flags)
                            {
                                if (((string)pair.Value.Tag) == filename)
                                {
                                    pair2Del = pair;

                                    // will be removed with next refresh.
                                    //ilFlags.Images.RemoveAt(pair.Value.ImageIndex);
                                    break;
                                }
                            }

                            if (pair2Del.Value != null)
                                m_Flags.Remove(pair2Del);

                            // remove from mod selection
                            MainForm.ModSelection.RemoveNodeByNodeText(Path.GetFileName(filename));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Error while deleting file. " + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                FillListView();
            }
        }

        #endregion

        #endregion
    }
}
