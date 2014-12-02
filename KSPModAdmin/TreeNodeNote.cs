using System.Windows.Forms;

namespace KSPModAdmin.Utils
{
    /// <summary>
    /// A TreeNode representation of a Zip entry from a KSP Mod Zip-File.
    /// </summary>
    public class TreeNodeNote : CommonTools.Node
    {
        #region Properties

        public string Note
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        public TreeNodeNote()
            : base()
        {
            SetData(new string[] { "", "" });
        }

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        /// <param name="text">The display text of the node.</param>
        public TreeNodeNote(string text)
            : base()
        {
            SetData(new string[] { "", "" });
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        /// <param name="key">The identification key of the Node.</param>
        /// <param name="text">The display text of the node.</param>
        public TreeNodeNote(string key, string text)
            : base()
        {
            SetData(new string[] { "", "" });
            Name = key;
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the KSPMA_TreeNode class.
        /// </summary>
        /// <param name="key">The identification key of the Node.</param>
        /// <param name="text">The display text of the node.</param>
        /// <param name="text">The note of the node.</param>
        public TreeNodeNote(string key, string text, string note)
            : base()
        {
            SetData(new string[] { "", "" });
            Name = key;
            Text = text;
            Note = note;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a clone of itself and all its childs.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            TreeNodeNote clone = new TreeNodeNote(Name, Text);
            clone.Id = Id;
            clone.ImageId = ImageId;
            clone.Tag = Tag;
            clone.Checked = Checked;
            clone.Expanded = Expanded;
            clone.HasChildren = (Nodes.Count > 0);

            foreach (TreeNodeNote child in Nodes)
                clone.Nodes.Add((TreeNodeNote)child.Clone());

            //clone.Name = Name;
            //clone.Text = Text;
            clone.Note = Note;

            if (Data.Length > 0)
            {
                clone.Data = new string[Data.Length];
                for (int i = 0; i < Data.Length; ++i)
                    clone.Data[i] = Data[i];
            }

            return clone;
        }

        #endregion

        #region Private

        /// <summary>
        /// Searches the node with the passed key.
        /// </summary>
        /// <param name="key">The identification key of the Node.</param>
        /// <param name="startNode">The startnode to start the search from.</param>
        /// <returns></returns>
        private TreeNodeNote SearchNode(string key, TreeNodeNote startNode)
        {
            if (key == string.Empty || startNode == null)
                return null;

            if (Name == key)
                return this;

            foreach (TreeNodeNote child in Nodes)
            {
                TreeNodeNote node = SearchNode(key, (TreeNodeNote)child);
                if (node != null)
                    return node;
            }

            return null;
        }

        #endregion
    }
}