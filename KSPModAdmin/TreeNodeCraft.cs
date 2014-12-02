using System.Windows.Forms;
using KSPModAdmin.Utils.CommonTools;
using System.Collections.Generic;

namespace KSPModAdmin.Utils
{
    /// <summary>
    /// A TreeNode representation of a Zip entry from a KSP Mod Zip-File.
    /// </summary>
    public class TreeNodeCraft : CommonTools.Node
    {
        protected List<string> mModList = new List<string>();

        #region Properties

        public string CraftName { get; set; }
        public string Type
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
            }
        }
        public string Folder
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
        public string Version
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
        public string Mods
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
        //public string Note
        //{
        //    get
        //    {
        //        if (Data != null && Data.Length > 5)
        //            return (string)Data[5];
        //        else
        //            return string.Empty;
        //    }
        //    set
        //    {
        //        if (Data != null && Data.Length > 5)
        //            Data[5] = value;
        //    }
        //}

        public string FilePath { get; set; }

        public bool IsPartInfo { get { return Type == string.Empty; } }

        public bool ValidPart { get { return (RelatedPart != null); } }

        public bool IsInvalidOrHasInvalidChilds
        {
            get
            {
                if (Parent == null)
                {
                    foreach (TreeNodeCraft child in Nodes)
                    {
                        if (child.IsInvalidOrHasInvalidChilds)
                            return true;
                    }

                    return false;
                }

                if (!ValidPart)
                    return true;

                else
                {
                    foreach (TreeNodeCraft child in Nodes)
                    {
                        if (child.IsInvalidOrHasInvalidChilds)
                            return true;
                    }
                }

                return false;
            }
        }

        public TreeNodePart1 RelatedPart { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        public TreeNodeCraft()
            : base()
        {
            CraftName = string.Empty;
            SetData(new string[] { "", "", "", "", "" });
            FilePath = string.Empty;
            RelatedPart = null;
        }

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        /// <param name="text">The display text of the node.</param>
        public TreeNodeCraft(string text)
            : base()
        {
            CraftName = text;
            SetData(new string[] { "", "", "", "", "" });
            FilePath = string.Empty;
            RelatedPart = null;
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        /// <param name="key">The identification key of the Node.</param>
        /// <param name="text">The display text of the node.</param>
        public TreeNodeCraft(string key, string text)
            : base()
        {
            CraftName = text;
            SetData(new string[] { "", "", "", "", "" });
            FilePath = string.Empty;
            RelatedPart = null;
            Name = key;
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        /// <param name="key">The identification key of the Node.</param>
        /// <param name="text">The display text of the node.</param>
        /// <param name="text">The note of the node.</param>
        public TreeNodeCraft(string key, string text, string craftType, string folder, string version, string mods)
            : base()
        {
            CraftName = text;
            SetData(new string[] { "", "", "", "", "" });
            FilePath = string.Empty;
            RelatedPart = null;
            Name = key;
            Text = text;
            Type = craftType;
            Folder = folder;
            Version = version;
            Mods = mods;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a clone of itself and all its childs.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            TreeNodeCraft clone = new TreeNodeCraft(Name, CraftName);
            clone.Id = Id;
            clone.ImageId = ImageId;
            clone.Tag = Tag;
            clone.Checked = Checked;
            clone.Expanded = Expanded;
            clone.HasChildren = (Nodes.Count > 0);

            foreach (TreeNodeCraft child in Nodes)
                clone.Nodes.Add((TreeNodeCraft)child.Clone());

            //clone.Name = Name;
            clone.Text = Text;
            clone.Type = Type;
            clone.Folder = Folder;
            clone.Version = Version;
            clone.Mods = Mods;

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

        public void AddMod(string modName)
        {
            if (!mModList.Contains(modName))
                mModList.Add(modName);

            UpdateMods();
        }

        public void RemoveMod(string modName)
        {
            if (mModList.Contains(modName))
                mModList.Remove(modName);

            UpdateMods();
        }

        /// <summary>
        /// Removes the relation part from the appropriate craftpart.
        /// </summary>
        /// <param name="partNode"></param>
        public void RemovePartRelation(TreeNodePart1 partNode)
        {
            foreach (TreeNodeCraft part in Nodes)
            {
                if (part.RelatedPart != null && part.RelatedPart.PartTitle == partNode.PartTitle)
                {
                    part.Text = part.CraftName; // craft name is here the name of the part.
                    part.RelatedPart = null;
                }
            }
        }

        /// <summary>
        /// Sorts its part nodes by the node text value.
        /// </summary>
        public void SortPartsByDisplayText()
        {
            Nodes.Sort(delegate(Node n1, Node n2)
                       {
                           string s1 = n1.Text.Substring(0, n1.Text.IndexOf(" ("));
                           string s2 = n2.Text.Substring(0, n2.Text.IndexOf(" ("));
                           return s1.CompareTo(s2);
                       });
        }

        #endregion

        #region Private

        private void UpdateMods()
        {
            Mods = string.Empty;
            foreach (string mod in mModList)
                Mods += mod + ", ";

            if (Mods.Length > 2)
                Mods = Mods.Substring(0, Mods.Length - 2);
        }

        /// <summary>
        /// Searches the node with the passed key.
        /// </summary>
        /// <param name="key">The identification key of the Node.</param>
        /// <param name="startNode">The startnode to start the search from.</param>
        /// <returns></returns>
        private TreeNodeCraft SearchNode(string key, TreeNodeCraft startNode)
        {
            if (key == string.Empty || startNode == null)
                return null;

            if (Name == key)
                return this;

            foreach (TreeNodeCraft child in Nodes)
            {
                TreeNodeCraft node = SearchNode(key, (TreeNodeCraft)child);
                if (node != null)
                    return node;
            }

            return null;
        }

        #endregion
    }
}