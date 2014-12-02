using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using KSPModAdmin.Utils.CommonTools;

namespace KSPModAdmin.Utils
{
    public class TreeNodePart1 : CommonTools.Node
    {
        #region Properties

        /// <summary>
        /// The full path to the part file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The title of the part.
        /// </summary>
        public string PartTitle
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        /// <summary>
        /// The type the part file.
        /// </summary>
        public string Category
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

        /// <summary>
        /// The mod the part file.
        /// </summary>
        public string Mod
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

        /// <summary>
        /// The name of the part.
        /// </summary>
        public string PartName
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CraftNode class.
        /// </summary>
        public TreeNodePart1()
            : base()
        {
            SetData(new string[] { "", "", "", "" });
            FilePath = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the CraftNode class.
        /// </summary>
        /// <param name="text">The display text of the node.</param>
        public TreeNodePart1(string text)
            : base()
        {
            SetData(new string[] { "", "", "", "" });
            FilePath = string.Empty;
            PartTitle = text;
        }

        /// <summary>
        /// Initializes a new instance of the CraftNode class.
        /// </summary>
        /// <param name="key">The identification key of the Node.</param>
        /// <param name="text">The display text of the node.</param>
        public TreeNodePart1(string key, string text)
            : base()
        {
            Name = key;
            SetData(new string[] { "", "", "", "" });
            FilePath = string.Empty;
            PartTitle = text;
        }

        #endregion

        public void AddRelatedCraft(TreeNodeCraft craft)
        {
            if (!Nodes.ContainsKey(craft.Text))
            {
                TreeNodePart1 cNode = new TreeNodePart1(craft.FilePath, craft.CraftName);
                cNode.Tag = craft;
                Nodes.Add(cNode);
            }
        }

        public void RemoveCraft(TreeNodeCraft craftNode)
        {
            TreeNodePart1 temp = null;
            foreach (TreeNodePart1 craft in Nodes)
            {
                if (craft.Tag != null && ((TreeNodeCraft)craft.Tag).FilePath == craftNode.FilePath)
                {
                    temp = craft;
                    break;
                }
            }

            if (temp != null)
                Nodes.Remove(temp);
        }
    }
}
