using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Drawing;
using KSPModAdmin.Utils;
using System.ComponentModel;

namespace KSPModAdmin.Utils
{
    /// <summary>
    /// EventHandler for the CheckedEvaluation event.
    /// </summary>
    public delegate bool CheckedEvaluationHandler(TreeNode nodeToCheck);

    /// <summary>
    /// EventHandler for the SelectedNodeChanged event.
    /// </summary>
    public delegate void SelectedNodeChangedHandler(TreeNode selectedNode);

    /// <summary>
    /// Simple extension of the TreeView.
    /// Adds the following features:
    /// - Multi node selection
    /// - Backwards checkbox checking (if a cihld is checkt the parents will be checked too) see CheckedStateBackwarding.
    /// - Forwards checkbox checking (if a parent is checked all its childs will be checked too) see CheckedStateForwarding.
    /// - Branch selection if a node is selected the complete branch (the root node and all its childs) will be selected.
    /// - ReadOnly mode.
    /// </summary>
    public class TreeViewEx : TreeView
    {
        /// <summary>
        /// Event CheckedEvaluation occurs when a node should be checked.
        /// Return value determines cancelation of the check process.
        /// </summary>
        public event CheckedEvaluationHandler CheckedStateChanging = null;

        /// <summary>
        /// Event for selected tree node changed.
        /// Occurs when the selected node changes.
        /// </summary>
        public event SelectedNodeChangedHandler SelectedNodeChanged = null;

        #region Members

        /// <summary>
        /// The currently focused TreeNode.
        /// </summary>
        private TreeNode m_FocusedNode = null;

        /// <summary>
        /// The currently selected TreeNode.
        /// </summary>
        private TreeNode m_SelectedNode = null;

        /// <summary>
        /// The currently selected TreeNodes.
        /// </summary>
        private List<TreeNode> m_SelectedNodes = new List<TreeNode>();

        /// <summary>
        /// Flag that indicates if a upade of chacked state is already running.
        /// </summary>
        private bool m_UpdatingNodeCheckedState = false;

        /// <summary>
        /// Flag to determine if this control is in read only mode or not.
        /// </summary>
        private bool m_ReadOnly = false;

        /// <summary>
        /// The back color in read only mode.
        /// </summary>
        private Color m_ReadOnlyBackColor = SystemColors.ControlLight;

        /// <summary>
        /// The last back color of the TreeView.
        /// </summary>
        private Color m_LastBackColor = Color.Empty; //SystemColors.ControlLightLight;

        /// <summary>
        /// The manager of action keys that handels our key strokes.
        /// </summary>
        private ActionKeyManager m_ActionKeyManager = new ActionKeyManager();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value that indicates wether the user can MultiSeletion nodes or not.
        /// </summary>
        [DefaultValue(false)]
        public bool AllowMultiSelect { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates wether the TreeView is readonly or not.
        /// </summary>
        [DefaultValue(false)]
        public bool ReadOnly 
        {
            get
            {
                return m_ReadOnly;
            }
            set
            {
                m_ReadOnly = value;
                if (!m_ReadOnly)
                    BackColor = m_LastBackColor;
                else
                {
                    if (m_LastBackColor != Color.Empty)
                        m_LastBackColor = BackColor;
                    BackColor = ReadOnlyBGColor;
                }
            }
        }

        /// <summary>
        /// The currently selected TreeNodes.
        /// </summary>
        /// <Remarks>We use the new keyword to Hide the native treeview's SelectedNode property.</Remarks> 
        [Browsable(false)]
        public new TreeNode SelectedNode
        {
            get { return m_SelectedNode; }
            set
            {
                ClearSelectedNodes();
                if (value != null)
                    SelectNode(value, true);
            }
        }

        /// <summary>
        /// The currently selected TreeNode.
        /// </summary>
        [Browsable(false)]
        public List<TreeNode> SelectedNodes
        {
            get
            {
                return m_SelectedNodes;
            }
            set
            {
                ClearSelectedNodes();
                if (value != null)
                {
                    foreach (TreeNode node in value)
                        SelectNode(node, true);
                }
            }
        }

        /// <summary>
        /// The currently selected root TreeNode.
        /// </summary>
        [Browsable(false)]
        public List<TreeNode> SelectedRootNodes
        {
            get
            {   
                List<TreeNode> result = new List<TreeNode>();
                foreach (TreeNode node in m_SelectedNodes)
                {
                    TreeNode tempNode = node;
                    while (tempNode.Parent != null)
                        tempNode = tempNode.Parent;

                    if (!result.Contains(tempNode))
                        result.Add(tempNode);
                }

                return result;
            }
        }

        /// <summary>
        /// Get or sets a value that indicating wether the complete branch shoud be selected if a child was selected (or deselected).
        /// </summary>
        [DefaultValue(false)]
        public bool SelectCompleteBranch { get; set; }

        /// <summary>
        /// Get or sets a value that indicating wether the childs of the checked node should be checked too.
        /// </summary>
        [DefaultValue(false)]
        public bool CheckedStateForwarding { get; set; }

        /// <summary>
        /// Get or sets a value that indicating wether the parents of the checked node should be checked too.
        /// </summary>
        [DefaultValue(false)]
        public bool CheckedStateBackwarding { get; set; }

        /// <summary>
        /// Get or sets the BackColor in read only mode.
        /// </summary>
        public Color ReadOnlyBGColor { get { return m_ReadOnlyBackColor; } set { m_ReadOnlyBackColor = value; } }

        #endregion
        
        #region Constructors

        /// <summary>
        /// Creates a instance of the TreeViewEx class.
        /// </summary>
        public TreeViewEx()
		{
			base.SelectedNode = null;
		}

        #endregion

        #region Static

        /// <summary>
        /// Returns the selection state of the passed node.
        /// </summary>
        /// <param name="node">The node to check the selection state.</param>
        /// <returns>The selection state of the passed node.</returns>
        public static bool IsSelected(TreeNode node) { return node.ForeColor == SystemColors.HighlightText; }

        #endregion

        #region Overrides

        /// <summary>
        /// Handles the KeyDown event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e); 
            
            if (e.KeyCode == Keys.A && e.Control)
            {
                HandleControlA();
            }

            if (m_FocusedNode == null) return;

            if (e.KeyCode == Keys.Space)
            {
                HandleSpace();
            }
            else if (e.KeyCode == Keys.Up)
            {
                HandleArrowUp();
            }
            else if (e.KeyCode == Keys.Down)
            {
                HandleArrowDown();
            }
            else if (e.KeyCode == Keys.Left)
            {
                HandleArrowLeft();
            }
            else if (e.KeyCode == Keys.Right)
            {
                HandleArrowRight();
            }
            else if (char.IsLetterOrDigit((char)e.KeyValue) && !e.Control && !e.Alt)
            {
                HandleLetterOrDigit((char)e.KeyValue);
            }

            this.EndUpdate();
        }

        /// <summary>
        /// Handles the OnMouseDown event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            base.SelectedNode = null;

            TreeNode node = this.GetNodeAt(e.Location);
            if (node != null)
            {
                if (node.Bounds.X <= e.Location.X)
                    SelectNode(node);
            }
            else
                ClearSelectedNodes();
        }

        /// <summary>
        /// Handles the OnBeforeSelect event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            // don't allow base.SelectedNode to be set
            e.Cancel = true;
            base.OnBeforeSelect(e);
            base.SelectedNode = null;
        }

        /// <summary>
        /// Handles the OnAfterSelect event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            // don't allow base.SelectedNode to be set
            base.OnAfterSelect(e);
            base.SelectedNode = null;
        }

        /// <summary>
        /// Handles the BeforeCheck event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeCheck(TreeViewCancelEventArgs e)
        {
            if (ReadOnly)
                e.Cancel = true;
            else if (!m_UpdatingNodeCheckedState)
                base.OnBeforeCheck(e);
        }

        /// <summary>
        /// Handles the AfterCheck event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            if (!m_UpdatingNodeCheckedState)
                base.OnAfterCheck(e);

            ChangeCheckedRecursive(e.Node, e.Node.Checked);
        }

        /// <summary>
        /// Handles the OnDrawNode event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            Rectangle bounds = new Rectangle(e.Node.Bounds.X + 1, e.Node.Bounds.Y, e.Node.Bounds.Width, e.Node.Bounds.Height);

            if (e.Node.ForeColor == SystemColors.HighlightText)
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), bounds);

            TextRenderer.DrawText(e.Graphics,
                                  e.Node.Text,
                                  e.Node.NodeFont,
                                  bounds,
                                  e.Node.ForeColor,
                                  Color.Empty,
                                  TextFormatFlags.VerticalCenter);

            if (e.Node == m_FocusedNode && m_SelectedNode != null)
            {
                bounds.Height -= 1;
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), bounds);
            }

            bounds.Height += 1;
            base.OnDrawNode(new DrawTreeNodeEventArgs(e.Graphics, e.Node, bounds, e.State));
        }

        /// <summary>
        /// Handles the OnMouseUp event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (SelectedNodeChanged != null)
                SelectedNodeChanged(SelectedNode);
        }
        
        #region DoubleClick handling

        /// <summary>
        /// Constant of the WindowMessage (left mouse button double click)
        /// </summary>
        /// <remarks></remarks>
        public const int WM_LBUTTONDBLCLK = 0x203;

        /// <summary>
        /// Listens to the Windows message loop for the WM_LBUTTONDBLCLK message.
        /// </summary>
        /// <param name="msg">The windows message.</param>
        /// <remarks></remarks>
        protected override void WndProc(ref System.Windows.Forms.Message msg)
        {
            switch (msg.Msg)
            {
                case WM_LBUTTONDBLCLK:
                    OnDoubleClick(new EventArgs());
                    return;
            }

            if (m_ActionKeyManager.HandleKeyMessage(ref msg))
                return;

            base.WndProc(ref msg);
        }

        /// <summary>
        /// Handels the Doubleclick event
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        protected override void OnDoubleClick(EventArgs e)
        {
            // get mouse position in client relation.
            Point location = System.Windows.Forms.Cursor.Position;
            location = this.PointToClient(location);

            // get node on mouse position
            TreeNode node = this.GetNodeAt(location);
            if (node != null) // && node.Bounds.X <= location.X)
            {
                if (!node.IsExpanded)
                    node.Expand();
                else
                    node.Collapse();
            }
                
            base.OnDoubleClick(e);
        }

        #endregion

        #endregion

        #region Public 

        /// <summary>
        /// Adds a key with a sertain action to the TreeView.
        /// If the key gets pressed the callback function will be called.
        /// </summary>
        /// <param name="key">The key to watch.</param>
        /// <param name="callback">The Action that should be performed when key gets pressed.</param>
        /// <param name="modifierKeys">A list of modifier keys that must be pressed to fire callback.</param>
        /// <param name="once">Fires only once for keydown, the key has to be released and pressed again to fire the callback again.</param>
        public void AddActionKey(VirtualKey key, ActionKeyHandler callback, ModifierKey[] modifierKeys = null, bool once = false)
        {
            m_ActionKeyManager.AddActionKey(key, callback, modifierKeys, once);
        }

        /// <summary>
        /// Removes a key and its actions from the TreeView.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        public void RemoveActionKey(VirtualKey key)
        {
            m_ActionKeyManager.RemoveActionKey(key);
        }

        /// <summary>
        /// Removes all keys and there actions.
        /// </summary>
        public void RemoveAllActionKeys()
        {
            m_ActionKeyManager.RemoveAllActionKeys();
        }

        #endregion

        #region Private

        #region Key handling

        /// <summary>
        /// Handles the space key.
        /// Check or uncheck the focused node.
        /// </summary>
        private void HandleSpace()
        {
            // Check or uncheck the node
            m_FocusedNode.Checked = !m_FocusedNode.Checked;
        }

        /// <summary>
        /// Handles the key arrow up.
        /// Selectes the previous node.
        /// </summary>
        private void HandleArrowUp()
        {
            // Select the previous node
            TreeNode node = m_FocusedNode.PrevVisibleNode;
            if (m_FocusedNode.PrevVisibleNode != null)
            {
                SelectNode(m_FocusedNode.PrevVisibleNode);

                if (SelectCompleteBranch && m_SelectedNodes.Count > 1)
                {
                    TreeNode root = node;
                    // get branch parent 
                    while (root.Parent != null)
                        root = root.Parent;

                    m_FocusedNode = root;
                }
            }
        }

        /// <summary>
        /// Handles the key arrow down.
        /// Selectes the next node.
        /// </summary>
        private void HandleArrowDown()
        {
            // Select the next node
            if (m_FocusedNode.NextVisibleNode != null)
                SelectNode(m_FocusedNode.NextVisibleNode);
        }

        /// <summary>
        /// Handles the key arrow left.
        /// Collapses the focused node or selects the parent if already collapsed.
        /// </summary>
        private void HandleArrowLeft()
        {
            if (m_FocusedNode.IsExpanded && m_FocusedNode.Nodes.Count > 0)
            {
                // Collapse node when it has children
                m_FocusedNode.Collapse();
            }
            else if (m_FocusedNode.Parent != null)
            {
                // Node already collapsed -> select its parent.
                SelectNode(m_FocusedNode.Parent);
            }
        }

        /// <summary>
        /// Handles the key arrow right.
        /// Expands the focused node or selects the first child if already expanded.
        /// </summary>
        private void HandleArrowRight()
        {
            if (!m_FocusedNode.IsExpanded)
            {
                // Expand a collpased node's children
                m_FocusedNode.Expand();
            }
            else if (m_FocusedNode.Nodes.Count > 0)
            {
                // Node already expanded select -> its first child
                SelectNode(m_FocusedNode.Nodes[0]);
            }
        }

        /// <summary>
        /// Handles the keys control + a.
        /// Selects all noeds.
        /// </summary>
        private void HandleControlA()
        {
            foreach (TreeNode node in Nodes)
                SelectNodeAndChildsRecursive(node, true);
        }

        /// <summary>
        /// Handles letter and digit keys.
        /// Selects the first node with the first char is equal to the passed char.
        /// </summary>
        private void HandleLetterOrDigit(char character)
        {
            // select first node with this character
            string search = character.ToString().ToLower();
            TreeNode node = m_FocusedNode.NextVisibleNode;
            while (node != null)
            {
                if (node.Text.ToLower().StartsWith(search))
                {
                    SelectNode(node);
                    break;
                }

                node = node.NextVisibleNode;
            }
        }

        #endregion

        #region Select node

        /// <summary>
        /// Clears the list of selected nodes (and resets the node colors).
        /// </summary>
        private void ClearSelectedNodes()
        {
            foreach (TreeNode node in m_SelectedNodes)
            {
                node.BackColor = this.BackColor;
                node.ForeColor = this.ForeColor;
            }

            m_SelectedNodes.Clear();
            m_SelectedNode = null;
        }

        /// <summary>
        /// Selects or deselects the passed node.
        /// </summary>
        /// <param name="node">The node to select or deselect.</param>
        private void SelectNode(TreeNode node)
        {
            if (node == null)
                return;

            // If this is enables the performance of the treeview goes down masivly =(
            //BeginUpdate();

            // click + ctrl detected -> negate the selection state of the node.
            if (AllowMultiSelect && ModifierKeys == Keys.Control)
            {
                if (SelectCompleteBranch && m_FocusedNode != null)
                {
                    TreeNode root = node;
                    // get branch parent 
                    while (root.Parent != null)
                        root = root.Parent;

                    bool select = !m_SelectedNodes.Contains(node);
                    SelectNodeAndChildsRecursive(root, select);

                    m_FocusedNode = node;
                }
                else
                {
                    SelectNode(node, !m_SelectedNodes.Contains(node));
                }
            }

            // click + shift detexted -> select all from last selected nod to this node.
            else if (AllowMultiSelect && ModifierKeys == Keys.Shift)
            {
                // NO SHIFT SELECT SUPPORTED FOR NOW ///

                //// no selected node -> select the clicked one.
                //if (m_SelectedNodes.Count == 0)
                //    SelectNode(node, true);

                //// select all node between start end end node.
                //else
                //{
                //    TreeNode startNode = m_SelectedNodes[0];
                //    TreeNode endNode = node;

                //    // same parent just walk up or down till end node reached and select all visible nodes.
                //    if (startNode.Parent == endNode.Parent)
                //    {
                //        if (startNode.Index < endNode.Index)
                //        {
                //            // walk down
                //            while (startNode != endNode)
                //            {
                //                startNode = startNode.NextVisibleNode;
                //                if (startNode == null) break;
                //                SelectNode(startNode, true);
                //            }
                //        }
                //        else if (startNode.Index > endNode.Index)
                //        {
                //            // walk up
                //            while (startNode != endNode)
                //            {
                //                startNode = startNode.PrevVisibleNode;
                //                if (startNode == null) break;
                //                SelectNode(startNode, true);
                //            }
                //        }
                //        else
                //        {
                //            // same node clicked -> do nothing
                //        }
                //    }
                //    else
                //    {

                //    }
                //}
            }

            // normal click on a node -> select it
            else
            {
                ClearSelectedNodes();
                SelectNode(node, true);
            }

            OnAfterSelect(new TreeViewEventArgs(m_SelectedNode));

            // See BeginUpdate at the top.
            //EndUpdate();
        }

        /// <summary>
        /// Adds a node to the Selection or removes it depanding on select parameter.
        /// </summary>
        /// <param name="node">The node to de/select.</param>
        /// <param name="select">True if node should be selected otherwise false.</param>
        private void SelectNode(TreeNode node, bool select)
        {
            if (select)
            {
                m_SelectedNode = node;
                if (!m_SelectedNodes.Contains(node))
                    m_SelectedNodes.Add(node);

                node.BackColor = SystemColors.Highlight;
                node.ForeColor = SystemColors.HighlightText;
            }
            else
            {
                if (m_SelectedNodes.Contains(node))
                    m_SelectedNodes.Remove(node);

                node.BackColor = this.BackColor;
                node.ForeColor = this.ForeColor;
            }

            m_FocusedNode = node;
        }

        /// <summary>
        /// Selectes the passed node and all its childs down the tree.
        /// </summary>
        /// <param name="node">The node to select.</param>
        /// <param name="select">The select state for the passed node.</param>
        private void SelectNodeAndChildsRecursive(TreeNode node, bool select)
        {
            SelectNode(node, select);

            foreach (TreeNode child in node.Nodes)
                SelectNodeAndChildsRecursive(child, select);
        }

        #endregion

        #region TreeView CheckedChenge handling

        /// <summary>
        /// Changes all childs to the same chacked state like the Parent.
        /// (Selects all childs when parent is checked and/or uncheck parent if all childs where unchecked.
        /// </summary>
        /// <param name="node">Start node to chenge to checked state.</param>
        /// <param name="checkedState">The checked state to set to the node and its child.</param>
        public void ChangeCheckedRecursive(TreeNode node, bool checkedState, bool recursive = true)
        {
            if (m_UpdatingNodeCheckedState) return;

            m_UpdatingNodeCheckedState = true;
            ChangeNodeCheckedState(node, checkedState, recursive);
            m_UpdatingNodeCheckedState = false;
        }

        /// <summary>
        /// Changes all childs to the same chacked state like the Parent.
        /// (Selects all childs when parent is checked and/or uncheck parent if all childs where unchecked.
        /// </summary>
        /// <param name="node">Start node to change to checked state.</param>
        /// <param name="checkedState">The checked state to set to the node and its child.</param>
        private void ChangeNodeCheckedState(TreeNode node, bool checkedState, bool recursive = true)
        {
            if (CheckedStateChanging == null || !checkedState)
                node.Checked = checkedState;
            else if (CheckedStateChanging(node))
                node.Checked = checkedState;
            else
                return;

            if (!recursive)
                return;

            if (CheckedStateForwarding)
                foreach (TreeNode childNode in node.Nodes)
                    ChangeNodeCheckedState(childNode, checkedState, recursive);

            if (CheckedStateBackwarding)
                if (checkedState)
                {
                    // Check all parent nodes
                    TreeNode parent = node.Parent;
                    while (parent != null)
                    {
                        if (CheckedStateChanging == null)
                        {
                            parent.Checked = true;
                            parent = parent.Parent;
                        }
                        else if (CheckedStateChanging(node))
                        {
                            parent.Checked = true;
                            parent = parent.Parent;
                        }
                        else
                            break;
                    }
                }
                else
                {
                    // uncheck parent nodes if no childnode is checked.
                    UncheckParentNodes(node.Parent);
                }
        }

        /// <summary>
        /// Unchecks all parent nodes.
        /// </summary>
        /// <param name="node">Start node to change to checked state.</param>
        private void UncheckParentNodes(TreeNode node)
        {
            if (node == null) return;

            if (AllChildsSameCheckedState(node, false))
            {
                node.Checked = false;
                UncheckParentNodes(node.Parent);
            }
        }

        /// <summary>
        /// Checks if all child nodes have the same checked state.
        /// </summary>
        /// <param name="node">Start node to check from.</param>
        /// <param name="checkedState">The checked state to match.</param>
        /// <returns>True if all child nodes have the same checked state.</returns>
        private bool AllChildsSameCheckedState(TreeNode node, bool checkedState)
        {
            bool allSameCheckedState = true;
            foreach (TreeNode child in node.Nodes)
            {
                if (child.Checked != checkedState)
                {
                    allSameCheckedState = false;
                    break;
                }

                allSameCheckedState = AllChildsSameCheckedState(child, checkedState);
                if (!allSameCheckedState)
                    break;
            }

            return allSameCheckedState;
        }

        #endregion

        #endregion
    }
}
