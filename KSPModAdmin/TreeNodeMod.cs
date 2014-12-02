using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Views;

namespace KSPModAdmin.Utils
{
    /// <summary>
    /// A TreeNode representation of a Zip entry from a KSP Mod Zip-File.
    /// </summary>
    public class TreeNodeMod : CommonTools.Node
    {
        #region Member

        /// <summary>
        /// The destionation path of this node.
        /// </summary>
        private string mDestination = string.Empty;

        /// <summary>
        /// The KSPMA_TreeNode of the Zip-File (master parent).
        /// </summary>
        private TreeNodeMod mZipRoot = null;

        /// <summary>
        /// Flag to determine whether this mod is outdated or not.
        /// </summary>
        private bool mOutdated = false;

        /// <summary>
        /// Gets or sets the flag to determine if the destination of the node collides with another node destination.
        /// </summary>
        private bool mHasCollision = false;

        #endregion

        #region Properties

        public ModInfo ModInfo
        {
            get
            {
                ModInfo modInfo = new ModInfo();
                modInfo.Author = Author;
                modInfo.CreationDate = CreationDate;
                modInfo.DownloadDate = AddDate;
                modInfo.Downloads = Downloads;
                modInfo.LocalPath = Name;
                modInfo.Name = Text;
                modInfo.ProductID = ProductID;
                modInfo.Rating = Rating;
                modInfo.SpaceportURL = SpaceportURL;
                modInfo.ForumURL = ForumURL;
                modInfo.CurseForgeURL = CurseForgeURL;
                modInfo.VersionControl = VersionControl;
                return modInfo;
            }
            set
            {
                if (value != null)
                {
                    //Name = value.LocalPath;
                    if (!IsInstalled)
                        Text = value.Name;

                    Author = value.Author;
                    CreationDate = value.CreationDate;
                    AddDate = value.DownloadDate;
                    Downloads = value.Downloads;
                    ProductID = value.ProductID;
                    Rating = value.Rating;
                    SpaceportURL = value.SpaceportURL;
                    ForumURL = value.ForumURL;
                    CurseForgeURL = value.CurseForgeURL;
                    VersionControl = value.VersionControl;
                }
                else
                {
                    Author = string.Empty;
                    CreationDate = string.Empty;
                    AddDate = string.Empty;
                    Downloads = "0";
                    ProductID = string.Empty;
                    Rating = "0 (0)";
                    SpaceportURL = string.Empty;
                    ForumURL = string.Empty;
                    CurseForgeURL = string.Empty;
                    VersionControl = VersionControl.Spaceport;
                }
            }
        }

        #region Node data

        public string AddDate
        {
            get
            {
                if (Data != null && Data.Length > 1)
                    return (string)Data[1];
                else
                    return string.Empty;
            }
            set
            {
                if (Data != null && Data.Length > 1)
                    Data[1] = value;

                bool isOutdated = false;
                if (!string.IsNullOrEmpty(AddDate) && !string.IsNullOrEmpty(CreationDate))
                {
                    try { isOutdated = (DateTime.Parse(AddDate) < DateTime.Parse(CreationDate)); }
                    catch { }
                }
                IsOutdated = isOutdated;
            }
        }

        public string Version
        {
            get
            {
                if (Data != null && Data.Length > 2)
                    return (string)Data[2];
                else
                    return string.Empty;
            }
            set
            {
                if (Data != null && Data.Length > 2)
                    Data[2] = value;
            }
        }

        public string Note
        {
            get
            {
                if (Data != null && Data.Length > 3)
                    return (string)Data[3];
                else
                    return string.Empty;
            }
            set
            {
                if (Data != null && Data.Length > 3)
                    Data[3] = value;
            }
        }

        public string ProductID
        {
            get
            {
                if (Data != null && Data.Length > 4)
                    return (string)Data[4];
                else
                    return string.Empty;
            }
            set
            {
                if (Data != null && Data.Length > 4)
                    Data[4] = value;
            }
        }

        public string CreationDate
        {
            get
            {
                if (Data != null && Data.Length > 5)
                    return (string)Data[5];
                else
                    return string.Empty;
            }
            set
            {
                if (Data != null && Data.Length > 5)
                    Data[5] = value;
            }
        }

        public DateTime CreationDateAsDateTime
        {
            get
            {
                if (string.IsNullOrEmpty(CreationDate))
                    return DateTime.MinValue;
                else
                {
                    DateTime value = DateTime.MinValue;
                    return DateTime.TryParse(CreationDate, out value) ? value : DateTime.MinValue;
                }
            }
            set
            {
                CreationDate = (value == DateTime.MinValue ? string.Empty : value.ToString());
            }
        }

        public string Author
        {
            get
            {
                if (Data != null && Data.Length > 6)
                    return (string)Data[6];
                else
                    return string.Empty;
            }
            set
            {
                if (Data != null && Data.Length > 6)
                    Data[6] = value;
            }
        }

        public string Rating
        {
            get
            {
                if (Data != null && Data.Length > 7)
                    return (string)Data[7];
                else
                    return string.Empty;
            }
            set
            {
                if (Data != null && Data.Length > 7)
                    Data[7] = value;
            }
        }

        public string Downloads
        {
            get
            {
                if (Data != null && Data.Length > 8)
                    return (string)Data[8];
                else
                    return string.Empty;
            }
            set
            {
                if (Data != null && Data.Length > 8)
                    Data[8] = value;
            }
        }

        public string SpaceportURL { get; set; }

        public string ForumURL
        {
            get
            {
                if (Data != null && Data.Length > 10)
                    return (string)Data[10];
                else
                    return string.Empty;
            }
            set
            {
                if (Data != null && Data.Length > 10)
                    Data[10] = value;
            }
        }

        public string CurseForgeURL
        {
            get
            {
                if (Data != null && Data.Length > 9)
                    return (string)Data[9];
                else
                    return string.Empty;
            }
            set
            {
                if (Data != null && Data.Length > 9)
                    Data[9] = value;
            }
        }

        public VersionControl VersionControl { get; set; }

        #endregion

        /// <summary>
        /// Gets the KSPMA_TreeNode of the Zip-File (master parent).
        /// </summary>
        public TreeNodeMod ZipRoot
        {
            get
            {
                if (mZipRoot != null)
                    return mZipRoot;
                else
                {
                    if (Parent != null)
                        return ((TreeNodeMod)Parent).ZipRoot;
                    else
                        return this;
                }
            }
        }

        /// <summary>
        /// The NodeType pf this node.
        /// </summary>
        private NodeType mNodeType = NodeType.UnknownFolder;

        /// <summary>
        /// Gets or sets the NodeType pf this node.
        /// </summary>
        public NodeType NodeType
        {
            get { return mNodeType; }
            set
            {
                mNodeType = value;
                mZipRoot = null;
                switch (mNodeType)
                {
                    case NodeType.ZipRoot:
                        ImageId = 0;
                        mZipRoot = this;
                        break;
                    case NodeType.KSPFolder:
                        ImageId = 0;
                        break;
                    case NodeType.KSPFolderInstalled:
                        ImageId = 1;
                        break;
                    case NodeType.UnknownFolder:
                        ImageId = 0;
                        break;
                    case NodeType.UnknownFolderInstalled:
                        ImageId = 1;
                        break;
                    case NodeType.UnknownFile:
                        ImageId = 2;
                        break;
                    case NodeType.UnknownFileInstalled:
                        ImageId = 3;
                        break;
                    default:
                        MessageBox.Show("ImageIndex vergessen!");
                        ImageId = 0;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the destionation path of this node.
        /// </summary>
        public string Destination 
        {
            get
            {
                return mDestination; 
            } 
            set 
            { 
                if (mDestination != value)
                {
                    ModRegister.RemoveRegisteredModFile(this);
                    mDestination = value;

                    if (!string.IsNullOrEmpty(value) && Text != Constants.GAMEDATA)
                        ModRegister.RegisterModFile(this);
                }
                else
                {
                    mDestination = value;
                }
            } 
        }

        /// <summary>
        /// Gets or sets the AddDate as/from a DateTime.
        /// </summary>
        public DateTime AddDateAsDateTime
        {
            get
            {
                if (string.IsNullOrEmpty(AddDate))
                    return DateTime.MinValue;
                else
                {
                    DateTime value = DateTime.MinValue;
                    if (DateTime.TryParse(AddDate, out value))
                        return value;
                    else
                        return DateTime.MinValue;
                }
            }
            set
            {
                if (value == DateTime.MinValue)
                    AddDate = string.Empty;
                else
                    AddDate = value.ToString();
            }
        }


        /// <summary>
        /// Flag that indicates if the ZipArchive still exists at the last known path.
        /// </summary>
        public bool ZipExists
        {
            get
            {
                try
                {
                    var root = ZipRoot;
                    if (root != null)
                        return File.Exists(root.Name);

                }
                catch (Exception) { }

                return false;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates wether this node is a directory or not.
        /// </summary>
        public bool IsFile
        {
            get
            {
                return (NodeType == NodeType.UnknownFile ||
                        NodeType == NodeType.UnknownFileInstalled);
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates wether this node is a KSP folder or not.
        /// </summary>
        public bool IsKSPFolder
        {
            get
            {
                return (this.NodeType == NodeType.KSPFolder ||
                        this.NodeType == NodeType.KSPFolderInstalled);
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates wether this node was installed or not.
        /// </summary>
        public bool IsInstalled
        {
            get
            {
                return (NodeType == NodeType.KSPFolderInstalled ||
                        NodeType == NodeType.UnknownFileInstalled ||
                        NodeType == NodeType.UnknownFolderInstalled);
            }
        }

        /// <summary>
        /// Gets a value that indicates wether this mod is outdated or not.
        /// </summary>
        public bool IsOutdated
        {
            get
            {
                if (!string.IsNullOrEmpty(AddDate) && !string.IsNullOrEmpty(CreationDate))
                {
                    try { return (DateTime.Parse(AddDate) < DateTime.Parse(CreationDate) || mOutdated); }
                    catch { }
                }

                return false;
            }
            set { mOutdated = value; }
        }

        /// <summary>
        /// Gets or sets a value that indicates wether this node was installed or not.
        /// </summary>
        public bool HasInstalledChilds
        {
            get
            {
                foreach (TreeNodeMod child in Nodes)
                    if (child.IsInstalled || child.HasInstalledChilds)
                    {
                        if (child.Text == Constants.GAMEDATA ||
                            child.Text == Constants.SHIPS ||
                            child.Text == Constants.VAB ||
                            child.Text == Constants.SPH)
                        {
                            if (child.HasInstalledChilds)
                                return true;
                        }
                        else
                            return true;
                    }

                return false;
            }
        }

        /// <summary>
        /// Gets a value that indicates wether this node structure has a DestFolder or not.
        /// </summary>
        public bool HasDestination { get { return (mDestination != string.Empty); } }

        /// <summary>
        /// Flag that indicates if a child node has a destination.
        /// </summary>
        public bool HasDestinationForChilds
        {
            get
            {
                if (HasDestination) return true;

                foreach (TreeNodeMod child in this.Nodes)
                    if (child.HasDestinationForChilds) return true;

                return false;
            }
        }

        /// <summary>
        /// Gets or sets a flag to determine if the destination of the node collides with another node destination.
        /// </summary>
        public bool HasCollision
        {
            get
            {
                return mHasCollision && IsFile || (mHasCollision && !IsFile && MainForm.Instance.Options.FolderConflictDetection);
            }
            set { mHasCollision = value; }
        }

        /// <summary>
        /// Gets the flag that determines if a child node has a colliding node or not.
        /// </summary>
        public bool HasChildCollision
        {
            get
            {
                if (HasCollision)// && Text.ToLower() != Constants.GAMEDATA)
                    return true;

                foreach (TreeNodeMod child in Nodes)
                {
                    if (child.HasChildCollision)
                        return true;
                }

                return false;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        public TreeNodeMod()
            : base()
        {
            SetData(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
            HasCollision = false;
            VersionControl = VersionControl.None;
        }

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        /// <param name="text">The display text of the node.</param>
        public TreeNodeMod(string text)
            : base()
        {
            SetData(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
            Text = text;
            HasCollision = false;
            VersionControl = VersionControl.None;
        }

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        /// <param name="key">The identification key of the Node.</param>
        /// <param name="text">The display text of the node.</param>
        public TreeNodeMod(string key, string text)
            : base()
        {
            SetData(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
            Name = key;
            Text = text;
            HasCollision = false;
            VersionControl = VersionControl.None;
        }

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        /// <param name="key">The identification key of the Node.</param>
        /// <param name="text">The display text of the node.</param>
        /// <param name="nodeType">The display text of the node.</param>
        public TreeNodeMod(string key, string text, NodeType nodeType)
            : base()
        {
            SetData(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
            Name = key;
            Text = text;
            NodeType = nodeType;
            HasCollision = false;
            VersionControl = VersionControl.None;
        }

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        /// <param name="modInfo">The modInfos.</param>
        public TreeNodeMod(ModInfo modInfo)
            : base()
        {
            SetData(new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
            NodeType = KSPModAdmin.NodeType.ZipRoot;
            Name = modInfo.LocalPath;
            Text = modInfo.Name;
            AddDate = modInfo.DownloadDate;
            CreationDate = modInfo.CreationDate;
            Rating = modInfo.Rating;
            Downloads = modInfo.Downloads;
            SpaceportURL = modInfo.SpaceportURL;
            ForumURL = modInfo.ForumURL;
            CurseForgeURL = modInfo.CurseForgeURL;
            Author = modInfo.Author;
            ProductID = modInfo.ProductID;
            HasCollision = false;
            VersionControl = modInfo.VersionControl;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = string.Empty;
            //if (Depth > 0)
            //    result += "|";
            for (int i = 0; i < Depth; ++i)
                result += " ";
            //if (Depth > 0)
            //    result += "> ";
            return result + Text;
        }

        /// <summary>
        /// Returns a clone of itself and all its childs.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            TreeNodeMod clone = new TreeNodeMod(Name, Text, NodeType);
            clone.Id = Id;
            clone.ImageId = ImageId;
            clone.Tag = Tag;
            clone.Checked = Checked;
            clone.Expanded = Expanded;

            foreach (TreeNodeMod child in Nodes)
                clone.Nodes.Add((TreeNodeMod)child.Clone());

            clone.HasChildren = (Nodes.Count > 0);

            //clone.Name = Name;
            //clone.Text = Text;
            clone.AddDate = AddDate;
            clone.Note = Note;
            clone.mDestination = mDestination;

            if (Data.Length > 0)
            {
                clone.Data = new string[Data.Length];
                for (int i = 0; i < Data.Length; ++i)
                    clone.Data[i] = Data[i];
            }

            return clone;
        }

        #endregion

        #region Public

        #endregion

        #region Private

        /// <summary>
        /// Searches the node with the passed key.
        /// </summary>
        /// <param name="key">The identification key of the Node.</param>
        /// <param name="startNode">The startnode to start the search from.</param>
        /// <returns></returns>
        private TreeNodeMod SearchNode(string key, TreeNodeMod startNode)
        {
            if (key == string.Empty || startNode == null)
                return null;

            if (Name == key)
                return this;

            foreach (TreeNodeMod child in Nodes)
            {
                TreeNodeMod node = SearchNode(key, (TreeNodeMod)child);
                if (node != null)
                    return node;
            }

            return null;
        }

        #endregion
    }
}
