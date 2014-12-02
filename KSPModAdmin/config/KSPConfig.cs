using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using KSPModAdmin.Utils;

namespace KSPModAdmin.Config
{
    /// <summary>
    /// The config of all installed MODs for the specified KSP installation.
    /// </summary>
    public class KSPConfig
    {
        #region Members

        /// <summary>
        /// Versioin of the save file.
        /// </summary>
        protected const string mVersion = "v1.0";

        /// <summary>
        /// The backup path.
        /// </summary>
        protected string mBackupPath = Constants.MESSAGE_BACKUP_SELECT_FOLDER_TXTBOX;

        /// <summary>
        /// The download path.
        /// </summary>
        protected string mDownloadPath = string.Empty;

        /// <summary>
        /// Flag to aktivate the override mode during processing (overrides existing files in ksp  folders):
        /// </summary>
        protected bool mOverride = false;

        /// <summary>
        /// Flag for backup on KSP Mod Admin startup.
        /// </summary>
        protected bool mBackupOnStartup = false;

        /// <summary>
        /// Flag for backup on KSP Launch.
        /// </summary>
        protected bool mBackupOnKSPLaunch = false;

        /// <summary>
        /// Flag for backup on a certain interval.
        /// </summary>
        protected bool mBackupOnInterval = false;

        /// <summary>
        /// Backup interval time in minutes.
        /// </summary>
        protected int mBackupInterval = 15;

        /// <summary>
        /// Maximum backup files.
        /// </summary>
        protected int mMaxBackupFiles = 3;

        /// <summary>
        /// Dictionary of backup notes mBackupNotes[filename, note]
        /// </summary>
        protected Dictionary<string, string> mBackupNotes = new Dictionary<string, string>();

        /// <summary>
        /// Flag to determine if KSP should be started with a borderless window.
        /// </summary>
        protected bool mStartWithBorderlessWindow = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the backup path.
        /// </summary>
        public string BackupPath { get { return mBackupPath; } set { mBackupPath = value; } }

        /// <summary>
        /// Gets or sets the download path.
        /// </summary>
        public string DownloadPath { get { return mDownloadPath; } set { mDownloadPath = value; } }

        /// <summary>
        /// Flag to aktivate the override mode during processing (overrides existing files in ksp  folders):
        /// </summary>
        public bool Override { get { return mOverride; } set { mOverride = value; } }

        /// <summary>
        /// Gets or sets the flag for backup on KSP Mod Admin startup.
        /// </summary>
        public bool BackupOnStartup { get { return mBackupOnStartup; } set { mBackupOnStartup = value; } }

        /// <summary>
        /// Gets or sets the flag for backup on KSP Launch.
        /// </summary>
        public bool BackupOnKSPLaunch { get { return mBackupOnKSPLaunch; } set { mBackupOnKSPLaunch = value; } }

        /// <summary>
        /// Gets or sets the flag for backup on a certain interval.
        /// </summary>
        public bool BackupOnInterval { get { return mBackupOnInterval; } set { mBackupOnInterval = value; } }

        /// <summary>
        /// Backup interval time in minutes.
        /// </summary>
        public int BackupInterval { get { return mBackupInterval; } set { mBackupInterval = value; } }

        /// <summary>
        /// Maximum backup files.
        /// </summary>
        public int MaxBackupFiles { get { return mMaxBackupFiles; } set { mMaxBackupFiles = value; } }

        /// <summary>
        /// Dictionary of backup notes mBackupNotes[filename, note]
        /// </summary>
        public Dictionary<string, string> BackupNotes { get { return mBackupNotes; } set { mBackupNotes = value; } }

        /// <summary>
        /// Flag to determine if KSP should be started with a borderless window.
        /// </summary>
        public bool StartWithBorderlessWindow { get { return mStartWithBorderlessWindow; } set { mStartWithBorderlessWindow = value; } }

        #endregion

        /// <summary>
        /// Loads the config.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="modNodes"></param>
        /// <returns></returns>
        public void Load(string path, ref List<TreeNodeMod> modNodes)
        {
            TreeNodeMod root = new TreeNodeMod(Constants.ROOT);
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNodeList moVersion = doc.GetElementsByTagName(Constants.VERSION);
                if (moVersion.Count > 0)
                {
                    switch (moVersion[0].InnerText.ToLower())
                    {
                        case "v1.0":
                            root = LoadV1_0(doc);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MainForm.Instance.AddError("Error: " + ex.Message, ex);
            }

            modNodes.AddRange(root.Nodes.Cast<TreeNodeMod>());
        }

        /// <summary>
        /// Saves the config.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="nodeArray"></param>
        /// <param name="backupNotes"></param>
        /// <returns></returns>
        public bool Save(string path, TreeNodeMod[] nodeArray, Dictionary<string, string> backupNotes)
        {
            mBackupNotes = backupNotes;

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode root = doc.CreateElement(Constants.ROOTNODE);
            doc.AppendChild(root);

            XmlNode versionNode = doc.CreateElement(Constants.VERSION);
            versionNode.InnerText = mVersion;
            root.AppendChild(versionNode);

            XmlNode generalNode = doc.CreateElement(Constants.GENERAL);
            root.AppendChild(generalNode);
            
            XmlNode node = doc.CreateElement(Constants.KSPSTARTUPOPTIONS);
            XmlAttribute nodeAttribute = doc.CreateAttribute(Constants.BORDERLESSWINDOW);
            nodeAttribute.Value = mStartWithBorderlessWindow.ToString(); ;
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            node = doc.CreateElement(Constants.BACKUP_PATH);
            nodeAttribute = doc.CreateAttribute(Constants.NAME);
            nodeAttribute.Value = mBackupPath;
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            XmlNode backupNotesNode = doc.CreateElement(Constants.BACKUPNOTES);
            foreach (KeyValuePair<string, string> note in BackupNotes)
            {
                node = doc.CreateElement(Constants.BACKUPNOTE);
                nodeAttribute = doc.CreateAttribute(Constants.FILENAME);
                nodeAttribute.Value = note.Key;
                node.Attributes.Append(nodeAttribute);
                nodeAttribute = doc.CreateAttribute(Constants.NOTE);
                nodeAttribute.Value = note.Value;
                node.Attributes.Append(nodeAttribute);
                backupNotesNode.AppendChild(node);
            }
            generalNode.AppendChild(backupNotesNode);

            node = doc.CreateElement(Constants.DOWNLOAD_PATH);
            nodeAttribute = doc.CreateAttribute(Constants.NAME);
            nodeAttribute.Value = mDownloadPath;
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            node = doc.CreateElement(Constants.OVERRRIDE);
            nodeAttribute = doc.CreateAttribute(Constants.VALUE);
            nodeAttribute.Value = mOverride.ToString();
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            node = doc.CreateElement(Constants.AUTOBACKUPSETTING);
            nodeAttribute = doc.CreateAttribute(Constants.BACKUPONSTARTUP);
            nodeAttribute.Value = mBackupOnStartup.ToString();
            node.Attributes.Append(nodeAttribute);
            nodeAttribute = doc.CreateAttribute(Constants.BACKUPONKSPLAUNCH);
            nodeAttribute.Value = mBackupOnKSPLaunch.ToString();
            node.Attributes.Append(nodeAttribute);
            nodeAttribute = doc.CreateAttribute(Constants.BACKUPONINTERVAL);
            nodeAttribute.Value = mBackupOnInterval.ToString();
            node.Attributes.Append(nodeAttribute);
            nodeAttribute = doc.CreateAttribute(Constants.BACKUPINTERVAL);
            nodeAttribute.Value = mBackupInterval.ToString();
            node.Attributes.Append(nodeAttribute);
            nodeAttribute = doc.CreateAttribute(Constants.MAXBACKUPFILES);
            nodeAttribute.Value = mMaxBackupFiles.ToString();
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            XmlNode modsNode = doc.CreateElement(Constants.MODS);
            root.AppendChild(modsNode);

            foreach (TreeNodeMod mod in nodeArray)
                modsNode.AppendChild(CreateXmlNode(Constants.MOD, mod, root));

            doc.Save(path);

            return true;
        }

        /// <summary>
        /// Saves the node and all its child nodes.
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="child">The node to create the XmlNode from.</param>
        /// <param name="parent">The parent XmlNode of the new created XmlNode.</param>
        private static XmlNode CreateXmlNode(string nodeName, TreeNodeMod child, XmlNode parent)
        {
            XmlDocument doc = parent.OwnerDocument;
            XmlNode modNode = doc.CreateElement(nodeName);
            XmlAttribute pathNodeAttribute = doc.CreateAttribute(Constants.KEY);
            pathNodeAttribute.Value = child.Text;
            modNode.Attributes.Append(pathNodeAttribute);
            pathNodeAttribute = doc.CreateAttribute(Constants.NAME);
            pathNodeAttribute.Value = child.Name;
            modNode.Attributes.Append(pathNodeAttribute);
            pathNodeAttribute = doc.CreateAttribute(Constants.NODETYPE);
            pathNodeAttribute.Value = ((int)child.NodeType).ToString();
            modNode.Attributes.Append(pathNodeAttribute);
            pathNodeAttribute = doc.CreateAttribute(Constants.CHECKED);
            pathNodeAttribute.Value = child.Checked.ToString();
            modNode.Attributes.Append(pathNodeAttribute);
            if (child.AddDate != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.ADDDATE);
                pathNodeAttribute.Value = child.AddDate;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.Version != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.VERSION);
                pathNodeAttribute.Value = child.Version;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.Note != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.NOTE);
                pathNodeAttribute.Value = child.Note;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.ProductID != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.PRODUCTID);
                pathNodeAttribute.Value = child.ProductID;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.CreationDate != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.CREATIONDATE);
                pathNodeAttribute.Value = child.CreationDate;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.Author != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.AUTHOR);
                pathNodeAttribute.Value = child.Author;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.Rating != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.RATING);
                pathNodeAttribute.Value = child.Rating;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.Downloads != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.DOWNLOADS);
                pathNodeAttribute.Value = child.Downloads;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.SpaceportURL != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.MODURL);
                pathNodeAttribute.Value = child.SpaceportURL;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.ForumURL != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.FORUMURL);
                pathNodeAttribute.Value = child.ForumURL;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.CurseForgeURL != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.CURSEFORGEURL);
                pathNodeAttribute.Value = child.CurseForgeURL;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.Parent == null)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.VERSIONCONTROL);
                pathNodeAttribute.Value = ((int)child.VersionControl).ToString();
                modNode.Attributes.Append(pathNodeAttribute);
            }
            if (child.Destination != string.Empty)
            {
                pathNodeAttribute = doc.CreateAttribute(Constants.DESTINATION);
                pathNodeAttribute.Value = child.Destination;
                modNode.Attributes.Append(pathNodeAttribute);
            }
            parent.AppendChild(modNode);

            foreach (KSPModAdmin.Utils.CommonTools.Node childchild in child.Nodes)
                modNode.AppendChild(CreateXmlNode(Constants.MOD_ENTRY, ((TreeNodeMod)childchild), modNode));

            return modNode;
        }

        #region Load

        /// <summary>
        /// v1.0 load function.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private TreeNodeMod LoadV1_0(XmlDocument doc)
        {
            TreeNodeMod result = new TreeNodeMod(Constants.ROOT);
            XmlNodeList nodeList = doc.GetElementsByTagName(Constants.BACKUP_PATH);
            if (nodeList.Count >= 1)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == Constants.NAME)
                        mBackupPath = att.Value;
                }
            }

            nodeList = doc.GetElementsByTagName(Constants.DOWNLOAD_PATH);
            if (nodeList.Count >= 1)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == Constants.NAME)
                        mDownloadPath = att.Value;
                }
            }

            nodeList = doc.GetElementsByTagName(Constants.OVERRRIDE);
            if (nodeList.Count >= 1)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == Constants.VALUE)
                        mOverride = false; // mOverride = (att.Value.ToLower() == "true");
                }
            }

            nodeList = doc.GetElementsByTagName(Constants.KSPSTARTUPOPTIONS);
            if (nodeList.Count >= 1)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == Constants.BORDERLESSWINDOW)
                        mStartWithBorderlessWindow = (att.Value.ToLower() == "true");
                }
            }

            nodeList = doc.GetElementsByTagName(Constants.AUTOBACKUPSETTING);
            if (nodeList.Count >= 1)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == Constants.BACKUPONSTARTUP)
                        mBackupOnStartup = (att.Value.ToLower() == "true");
                    else if (att.Name == Constants.BACKUPONKSPLAUNCH)
                        mBackupOnKSPLaunch = (att.Value.ToLower() == "true");
                    else if (att.Name == Constants.BACKUPONINTERVAL)
                        mBackupOnInterval = false; // mBackupOnInterval = (att.Value.ToLower() == "true");
                    else if (att.Name == Constants.BACKUPINTERVAL)
                        mBackupInterval = int.Parse(att.Value);
                    else if (att.Name == Constants.MAXBACKUPFILES)
                        mMaxBackupFiles = int.Parse(att.Value);
                }
            }

            mBackupNotes.Clear();
            string noteKey = string.Empty;
            string noteValue = string.Empty;
            XmlNodeList notes = doc.GetElementsByTagName(Constants.BACKUPNOTE);
            foreach (XmlNode note in notes)
            {
                noteKey = string.Empty;
                noteValue = string.Empty;
                foreach (XmlAttribute att in note.Attributes)
                {
                    if (att.Name == Constants.FILENAME)
                        noteKey = att.Value;
                    else if (att.Name == Constants.NOTE)
                        noteValue = att.Value;
                }
                mBackupNotes.Add(noteKey, noteValue);
            }

            result.Nodes.Clear();
            XmlNodeList mods = doc.GetElementsByTagName(Constants.MOD);
            foreach (XmlNode mod in mods)
            {
                TreeNodeMod newNode = new TreeNodeMod();
                result.Nodes.Add(newNode);
                FillModTreeNode(mod, ref newNode);
                //result.Nodes.Add(CreateMODTreeNode(mod));
            }

            return result;
        }

        /// <summary>
        /// Creates a TreeNode for the XmlNode information.
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private TreeNodeMod FillModTreeNode(XmlNode mod, ref TreeNodeMod node)
        {
            node.AddDate = DateTime.Now.ToString();
            foreach (XmlAttribute att in mod.Attributes)
            {
                if (att.Name == Constants.NAME)
                    node.Name = att.Value;
                else if (att.Name == Constants.KEY)
                    node.Text = att.Value;
                else if (att.Name == Constants.ADDDATE)
                    node.AddDate = att.Value;
                else if (att.Name == Constants.VERSION)
                    node.Version = att.Value;
                else if (att.Name == Constants.NOTE)
                    node.Note = att.Value;
                else if (att.Name == Constants.PRODUCTID)
                    node.ProductID = att.Value;
                else if (att.Name == Constants.CREATIONDATE)
                    node.CreationDate = att.Value;
                else if (att.Name == Constants.AUTHOR)
                    node.Author = att.Value;
                else if (att.Name == Constants.RATING)
                    node.Rating = att.Value;
                else if (att.Name == Constants.DOWNLOADS)
                    node.Downloads = att.Value;
                else if (att.Name == Constants.MODURL)
                    node.SpaceportURL = att.Value;
                else if (att.Name == Constants.FORUMURL)
                    node.ForumURL = att.Value;
                else if (att.Name == Constants.CURSEFORGEURL)
                    node.CurseForgeURL = att.Value;
                else if (att.Name == Constants.VERSIONCONTROL)
                    node.VersionControl = (VersionControl)int.Parse(att.Value);
                else if (att.Name == Constants.CHECKED)
                    node.Checked = (att.Value.ToLower() == Constants.TRUE);
                else if (att.Name == Constants.NODETYPE)
                    node.NodeType = (NodeType)int.Parse(att.Value);
                else if (att.Name == Constants.DESTINATION)
                    node.Destination = att.Value;
                //else if (att.Name == Constants.INSTALLROOTKEY)
                //    node.InstallRootKey = att.Value;
            }

            foreach (XmlNode modEntry in mod.ChildNodes)
            { 
                TreeNodeMod newNode = new TreeNodeMod();
                node.Nodes.Add(newNode);
                FillModTreeNode(modEntry, ref newNode);
                //node.Nodes.Add(CreateMODTreeNode(modEntry));
            }

            return node;
        }


        ///// <summary>
        ///// Creates a TreeNode for the XmlNode information.
        ///// </summary>
        ///// <param name="mod"></param>
        ///// <returns></returns>
        //private TreeNodeMod CreateMODTreeNode(XmlNode mod)
        //{
        //    TreeNodeMod node = new TreeNodeMod();
        //    node.AddDate = DateTime.Now.ToString();
        //    foreach (XmlAttribute att in mod.Attributes)
        //    {
        //        if (att.Name == Constants.NAME)
        //            node.Name = att.Value;
        //        else if (att.Name == Constants.KEY)
        //            node.Text = att.Value;
        //        else if (att.Name == Constants.ADDDATE)
        //            node.AddDate = att.Value;
        //        else if (att.Name == Constants.VERSION)
        //            node.Version = att.Value;
        //        else if (att.Name == Constants.NOTE)
        //            node.Note = att.Value;
        //        else if (att.Name == Constants.PRODUCTID)
        //            node.ProductID = att.Value;
        //        else if (att.Name == Constants.CREATIONDATE)
        //            node.CreationDate = att.Value;
        //        else if (att.Name == Constants.AUTHOR)
        //            node.Author = att.Value;
        //        else if (att.Name == Constants.RATING)
        //            node.Rating = att.Value;
        //        else if (att.Name == Constants.DOWNLOADS)
        //            node.Downloads = att.Value;
        //        else if (att.Name == Constants.MODURL)
        //            node.SpaceportURL = att.Value;
        //        else if (att.Name == Constants.FORUMURL)
        //            node.ForumURL = att.Value;
        //        else if (att.Name == Constants.CHECKED)
        //            node.Checked = (att.Value.ToLower() == Constants.TRUE);
        //        else if (att.Name == Constants.NODETYPE)
        //            node.NodeType = (NodeType)int.Parse(att.Value);
        //        else if (att.Name == Constants.DESTINATION)
        //            node.Destination = att.Value;
        //        //else if (att.Name == Constants.INSTALLROOTKEY)
        //        //    node.InstallRootKey = att.Value;
        //    }

        //    foreach (XmlNode modEntry in mod.ChildNodes)
        //        node.Nodes.Add(CreateMODTreeNode(modEntry));

        //    return node;
        //}

        #endregion
    }
}
