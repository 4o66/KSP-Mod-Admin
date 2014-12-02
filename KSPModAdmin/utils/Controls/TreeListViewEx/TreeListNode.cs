using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace KSPModAdmin.Utils.CommonTools
{
	public class Node
	{
		NodeCollection		m_owner = null;
		Node				m_prevSibling = null;
		Node				m_nextSibling = null;
		NodeCollection		m_children = null;
		bool				m_hasChildren = false;
		bool				m_expanded = false;
		int					m_imageId = -1;
		int					m_id = -1;
		object				m_tag = null;

		public Node Parent
		{
			get 
			{ 
				if (m_owner != null)
					return m_owner.Owner; 
				return null;
			}
		}
		public Node PrevSibling
		{
			get { return m_prevSibling; }
		}
		public Node NextSibling
		{
			get { return m_nextSibling; }
		}
		public bool HasChildren
		{
			get 
			{
				if (m_children != null && m_children.IsEmpty() == false)
					return true;
				return m_hasChildren;
			}
			set
			{
				m_hasChildren = value;
			}
		}
		public int ImageId
		{
			get { return m_imageId; }
			set { m_imageId = value; }
		}
		public virtual NodeCollection Owner
		{
			get { return m_owner; }
		}
		public virtual NodeCollection Nodes
		{
			get
			{
				if (m_children == null)
					m_children = new NodeCollection(this);
				return m_children;
			}
		}
		public bool Expanded
		{
			get { return m_expanded && HasChildren; }
			set
			{
				if (m_expanded == value)
					return;
				NodeCollection root = GetRootCollection();
				if (root != null)
					root.NodetifyBeforeExpand(this, value);

				int oldcount = VisibleNodeCount;
				m_expanded = value;
				if (m_expanded)
					UpdateOwnerTotalCount(1, VisibleNodeCount);
				else
					UpdateOwnerTotalCount(oldcount, 1);

				if (root != null)
					root.NodetifyAfterExpand(this, value);
			}
		}
		public void Collapse()
		{
			Expanded = false;
		}
		public void Expand()
		{
			Expanded = true;
		}
		public void ExpandAll()
		{
			Expanded = true;
			if (HasChildren)
			{
				foreach (Node node in Nodes)
					node.ExpandAll();
			}
		}
		public object Tag
		{
			get { return m_tag; }
			set { m_tag = value; }
		}
		public Node()
        {
            m_UniqueID = ++m_NextID; // Add by BH
			m_data = new object[1];
		}
		public Node(string text)
        {
            m_UniqueID = ++m_NextID; // Add by BH
			m_data = new object[1] {text};
		}
		public Node(object[] fields)
		{
            m_UniqueID = ++m_NextID; // Add by BH
			SetData(fields);
		}
		object[] m_data = null;
		public object this [string fieldname]
		{
			get
			{
				return this[Owner.FieldIndex(fieldname)];
			}
			set
			{
				this[Owner.FieldIndex(fieldname)] = value;
			}
		}
		public object this [int index]
		{
			get
			{
				if (index < 0 || index >= m_data.Length)
					return null;
				return m_data[index];
			}
			set
			{
				if (index >= 0 && index < 100) // within this range just increase
				{
					if (m_data == null || index >= m_data.Length)
					{
						object[] newdata = new object[index+1];
						if (m_data != null)
							m_data.CopyTo(newdata, 0);
						m_data = newdata;
					}
				}
				AssertData(index);
				m_data[index] = value;
			}
		}
		public void SetData(object[] fields)
		{
			m_data = new object[fields.Length];
			fields.CopyTo(m_data, 0);
		}
		public int VisibleNodeCount
		{
			get 
			{ 
				// can not use Expanded property here as it returns false node has no children
				if (m_expanded)
					return m_childVisibleCount + 1; 
				return 1;
			}
		}
		/// <summary>
		/// MakeVisible will expand all the parents up the tree.
		/// </summary>
		public void MakeVisible()
		{
			Node parent = Parent;
			while (parent != null)
			{
				parent.Expanded = true;
				parent = parent.Parent;
			}
		}
		/// <summary>
		/// IsVisible returns true if all parents are expanded, else false
		/// </summary>
		public bool IsVisible()
		{
			Node parent = Parent;
			while (parent != null)
			{
				// parent not expanded, so this node is not visible
				if (parent.Expanded == false)
					return false;
				// parent not hooked up to a collection, so this node is not visible
				if (parent.Owner == null)
					return false;
				parent = parent.Parent;
			}
			return true;
		}
		public Node GetRoot()
		{
			Node parent = this;
			while (parent.Parent != null)
				parent = parent.Parent;
			return parent;
		}
		public NodeCollection GetRootCollection()
		{
			return GetRoot().Owner;
		}
		public string GetId()
		{
			StringBuilder sb = new StringBuilder(32);
			Node node = this;
			while (node != null)
			{
				node.Owner.UpdateChildIds(false);
				if (node.Parent != null)
					sb.Insert(0, "." + node.Id.ToString());
				else
					sb.Insert(0, node.Id.ToString());
				node = node.Parent;
			}
			return sb.ToString();
		}
		internal void InsertBefore(Node insertBefore, NodeCollection owner)
		{
			this.m_owner = owner;
			Node next = insertBefore;
			Node prev = null;
			if (next != null)
			{
				prev = insertBefore.PrevSibling;
				next.m_prevSibling = this;
			}
			if (prev != null)
				prev.m_nextSibling = this;

			this.m_nextSibling = next;
			this.m_prevSibling = prev;
			UpdateOwnerTotalCount(0, VisibleNodeCount);
		}
		internal void InsertAfter(Node insertAfter, NodeCollection owner)
		{
			this.m_owner = owner;
			Node prev = insertAfter;
			Node next = null;
			if (prev != null)
			{
				next = prev.NextSibling;
				prev.m_nextSibling = this;
				this.m_prevSibling = prev;
			}
			if (next != null)
				next.m_prevSibling = this;
			this.m_nextSibling = next;
			UpdateOwnerTotalCount(0, VisibleNodeCount);
		}
		internal void Remove()
		{
			Node prev = this.PrevSibling;
			Node next = this.NextSibling;
			if (prev != null)
				prev.m_nextSibling = next;
			if (next != null)
				next.m_prevSibling = prev;

			this.m_nextSibling = null;
			this.m_prevSibling = null;
			UpdateOwnerTotalCount(VisibleNodeCount, 0);
			this.m_owner = null;
			this.m_id = -1;
		}
		internal static void SetHasChildren(Node node, bool hasChildren)
		{
			if (node != null)
				node.m_hasChildren = hasChildren;
		}
		public int NodeIndex
		{
			get { return Id;}
		}
		internal int Id
		{
			get 
			{ 
				if (m_owner == null)
					return -1;
				m_owner.UpdateChildIds(false);
				return m_id; 
			}
			set { m_id = value; }
		}
		int m_childVisibleCount = 0;
		void UpdateTotalCount(int oldValue, int newValue)
		{
			int old = VisibleNodeCount;
			m_childVisibleCount += (newValue - oldValue);
			UpdateOwnerTotalCount(old, VisibleNodeCount);
		}
		void UpdateOwnerTotalCount(int oldValue, int newValue)
		{
			if (Owner != null)
				Owner.internalUpdateNodeCount(oldValue, newValue);
			if (Parent != null)
				Parent.UpdateTotalCount(oldValue, newValue);
		}

		void AssertData(int index)
		{
			Debug.Assert(index >= 0, "index >= 0");
			Debug.Assert(index < m_data.Length, "index < m_data.Length");
		}

        #region Added by BH

        private static long m_NextID = 0;
        private long m_UniqueID = 0;

        public string Name { get; set; }
        public string Text
        {
            get
            {
                if (Data != null && Data.Length > 0) 
                    return Data[0].ToString();
                else
                    return "";
            }
            set
            {
                if (Data != null && Data.Length > 0)
                    Data[0] = value;
            }
        }
        public bool Checked
        {
            get;
            set;
        }
        public long UniqueID { get { return m_UniqueID; } }
        public int Depth
        {
            get
            {
                int depth = 0;
                CommonTools.Node node = Parent;
                while (node != null)
                {
                    ++depth;
                    node = node.Parent;
                }
                return depth;
            }
        }
        public object[] Data
        {
            get { return m_data; }
            set { SetData(value); }
        }
        public bool IsDataEqual(object[] dataToCompateWith)
        {
            return (m_data == dataToCompateWith);
        }
        public string FullPath
        {
            get
            {
                string fullPath = Text;

                Node parent = Parent;
                while (parent != null)
                {
                    fullPath = parent.Text + "\\" + fullPath;
                    parent = parent.Parent;
                }
                return fullPath; 
            }
        }

        public virtual object Clone()
        {
            Node clone = new Node();
            clone.Name = Name;
            clone.Id = Id;
            clone.ImageId = ImageId;
            clone.Tag = Tag;
            clone.Checked = Checked;
            clone.Expanded = Expanded;
            clone.HasChildren = (Nodes.Count > 0);

            if (Data.Length > 0)
            {
                clone.Data = new string[Data.Length];
                for (int i = 0; i < Data.Length; ++i)
                    clone.Data[i] = Data[i];
            }

            foreach (Node child in Nodes)
                clone.Nodes.Add((Node)child.Clone());

            return clone;
        }

        #endregion
    }
	public class NodeCollection : IEnumerable
	{
		internal int m_version = 0;
		int		m_nextId = 0;
		int		m_IdDirty = 0;

		Node[]	m_nodesInternal = null;
		Node	m_owner = null;
		Node	m_firstNode = null;
		Node	m_lastNode = null;
		int		m_count = 0;
		public Node Owner
		{
			get { return m_owner; }
		}
		public Node FirstNode
		{
			get { return m_firstNode; }
		}
		public Node LastNode
		{
			get { return m_lastNode; }
		}
		public bool IsEmpty()
		{
			return m_firstNode == null;
		}
		public int Count
		{
			get { return m_count; }
		}
		public NodeCollection(Node owner)
		{
			m_owner = owner;
		}
		public virtual void Clear()
		{
			m_version++;
			while (m_firstNode != null)
			{
				Node node = m_firstNode;
				m_firstNode = node.NextSibling;
				node.Remove();
			}
			m_firstNode = null;
			m_lastNode = null;
			m_count = 0;
			m_totalNodeCount = 0;
			m_IdDirty = 0;
			m_nextId = 0;
			ClearInternalArray();
			Node.SetHasChildren(m_owner, m_count != 0);
		}
		public Node Add(string text)
		{
			return Add(new Node(text));
		}
		public Node Add(Node newnode)
		{
			m_version++;
			ClearInternalArray();
            //Debug.Assert(newnode != null && newnode.Owner == null, "Add(Node newnode)");
            if (m_firstNode == null)
                m_firstNode = newnode;
			newnode.InsertAfter(m_lastNode, this);
			m_lastNode = newnode;
			newnode.Id = m_nextId++;
			m_count++;
			return newnode;
		}
		public void Remove(Node node)
		{
			if (m_lastNode == null)
				return;
			m_version++;
			ClearInternalArray();
			Debug.Assert(node != null && object.ReferenceEquals(node.Owner, this), "Remove(Node node)");
			
			Node prev = node.PrevSibling;
			Node next = node.NextSibling;
			node.Remove();

			if (prev == null) // first node
				m_firstNode = next;
			if (next == null) // last node
				m_lastNode = prev;
			m_IdDirty++;
			m_count--;
			Node.SetHasChildren(m_owner, m_count != 0);
		}
		public void InsertAfter(Node node, Node insertAfter)
		{
			m_version++;
			ClearInternalArray();
			Debug.Assert(node.Owner == null, "node.Owner == null");
			if (insertAfter == null)
			{
				node.InsertBefore(m_firstNode, this);
				m_firstNode = node;
			}
			else
			{
				node.InsertAfter(insertAfter, this);
			}
			if (m_lastNode == insertAfter)
			{
				m_lastNode = node;
				node.Id = m_nextId++;
			}
			else
				m_IdDirty++;
			m_count++;
		}
		public Node this [int index]
		{
			get
			{
				Debug.Assert(index >= 0 && index < Count, "Index out of range");
				if (index >= Count)
					throw new IndexOutOfRangeException(string.Format("Node this [{0}], Collection Count {1}", index, Count));
				EnsureInternalArray();
				return m_nodesInternal[index];
            }
            set
            {
                EnsureInternalArray();
                m_nodesInternal[index] = value;
            }
		}
		
		public Node NodeAtIndex(int index)
		{
			Node node = FirstNode;
			while (index-- > 0 && node != null)
				node = node.NextSibling;
			return node;
		}
		public int GetNodeIndex(Node node)
		{
			int index = 0;
			Node tmp = FirstNode;
			while (tmp != null && tmp != node)
			{
				tmp = tmp.NextSibling;
				index++;
			}
			if (tmp == null)
				return -1;
			return index;
		}
		public virtual int FieldIndex(string fieldname)
		{
			NodeCollection rootCollection = this;
			while (rootCollection.Owner != null && rootCollection.Owner.Owner != null)
				rootCollection = rootCollection.Owner.Owner;
			return rootCollection.GetFieldIndex(fieldname);
		}
		public Node FirstVisibleNode()
		{
			return FirstNode;
		}
		public Node LastVisibleNode(bool recursive)
		{
			if (recursive)
				return FindNodesBottomLeaf(LastNode, true);
			return LastNode;
		}
		public virtual void NodetifyBeforeExpand(Node nodeToExpand, bool expanding)
		{
		}
		public virtual void NodetifyAfterExpand(Node nodeToExpand, bool expanding)
		{
		}
		internal void UpdateChildIds(bool recursive)
		{
			if (recursive == false && m_IdDirty == 0)
				return;
			m_IdDirty = 0;
			m_nextId = 0;
			foreach (Node node in this)
			{
				node.Id = m_nextId++;
				if (node.HasChildren && recursive)
					node.Nodes.UpdateChildIds(true);
			}
		}
		protected virtual int GetFieldIndex(string fieldname)
		{
			return -1;
		}
		public Node slowGetNodeFromVisibleIndex(int index)
		{
			int startindex = index;
			CommonTools.Tracing.StartTrack(0);
			RecursiveNodesEnumerator iterator = new RecursiveNodesEnumerator(m_firstNode, true);
			while (iterator.MoveNext())
			{
				index--;
				if (index < 0)
				{
					CommonTools.Tracing.EndTrack(0, "slowGetNodeFromVisibleIndex({0})", startindex);
					return iterator.Current as Node;
				}
			}
			CommonTools.Tracing.EndTrack(0, "slowGetNodeFromVisibleIndex (null)");
			return null;
		}
		
		public IEnumerator GetEnumerator()
		{
			return new NodesEnumerator(m_firstNode);
		}
		/// <summary>
		/// TotalRowCount returns the total number of 'visible' nodes. Visible meaning visible for the
		/// tree view, This is used to determine the size of the scroll bar
		/// If 10 nodes each has 10 children and 9 of them are expanded, then 100 will be returned.
		/// </summary>
		/// <returns></returns>
		public virtual int slowTotalRowCount(bool mustBeVisible)
		{
			int cnt = 0;
			RecursiveNodesEnumerator iterator = new RecursiveNodesEnumerator(this, mustBeVisible);
			while (iterator.MoveNext())
				cnt++;
			//Debug.Assert(cnt == m_totalNodeCount);
			return cnt;
		}
		public virtual int VisibleNodeCount
		{
			get { return m_totalNodeCount; }
		}
		/// <summary>
		/// Returns the number of pixels required to show all visible nodes
		/// </summary>

		void EnsureInternalArray()
		{
			if (m_nodesInternal != null)
			{
				Debug.Assert(m_nodesInternal.Length == Count, "m_nodesInternal.Length == Count");
				return;
			}
			m_nodesInternal = new Node[Count];
			int index = 0;
			foreach (Node xnode in this)
				m_nodesInternal[index++] = xnode;
		}
		void ClearInternalArray()
		{
			m_nodesInternal = null;
		}

		int m_totalNodeCount = 0;
		protected virtual void UpdateNodeCount(int oldvalue, int newvalue)
		{
			m_totalNodeCount += (newvalue - oldvalue);
		}
		internal void internalUpdateNodeCount(int oldvalue, int newvalue)
		{
			UpdateNodeCount(oldvalue, newvalue);
		}
		internal class NodesEnumerator : IEnumerator
		{
			Node m_firstNode;
			Node m_current = null;
			public NodesEnumerator(Node firstNode)
			{
				m_firstNode = firstNode;
			}
			public object Current
			{
				get { return m_current; }
			}
			public bool MoveNext()
			{
				if (m_firstNode == null)
					return false;
				if (m_current == null)
				{
					m_current = m_firstNode;
					return true;
				}
				m_current = m_current.NextSibling;
				return m_current != null;
			}
			public void Reset()
			{
				m_current = null;
			}
		}
		internal class RecursiveNodesEnumerator : IEnumerator<Node>
		{
			class NodeCollIterator : IEnumerator<Node>
			{
				Node m_firstNode;
				Node m_current;
				bool m_visible;
				public NodeCollIterator(NodeCollection collection, bool mustBeVisible)
				{
					m_firstNode = collection.FirstNode;
					m_visible = mustBeVisible;
				}
				public Node Current
				{
					get { return m_current; }
				}
				object IEnumerator.Current
				{
					get { return m_current; }
				}
				public bool MoveNext()
				{
					if (m_firstNode == null)
						return false;
					if (m_current == null)
					{
						m_current = m_firstNode;
						return true;
					}
					if (m_current.HasChildren && m_current.Nodes.FirstNode != null)
					{
						if (m_visible == false || m_current.Expanded)
						{
							m_current = m_current.Nodes.FirstNode;
							return true;
						}
					}
					if (m_current == m_firstNode)
					{
						m_firstNode = m_firstNode.NextSibling;
						m_current = m_firstNode;
						return m_current != null;
					}
					if (m_current.NextSibling != null)
					{
						m_current = m_current.NextSibling;
						return true;
					}

					// search up the parent tree
					while (m_current.Parent != null)
					{
						m_current = m_current.Parent;
						// back at collection level, now go to next sibling
						if (m_current == m_firstNode)
						{
							m_firstNode = m_firstNode.NextSibling;
							m_current = m_firstNode;
							return m_current != null;
						}
						if (m_current.NextSibling != null)
						{
							m_current = m_current.NextSibling;
							return true;
						}
					}
					m_current = null;
					return false;
				}
				public void Reset()
				{
					m_current = null;
				}
				public void Dispose()
				{
					throw new Exception("The method or operation is not implemented.");
				}
			}
			IEnumerator<Node> m_enumerator = null;
			public RecursiveNodesEnumerator(Node firstNode, bool mustBeVisible)
			{
				m_enumerator = new ForwardNodeEnumerator(firstNode, mustBeVisible);
			}
			public RecursiveNodesEnumerator(NodeCollection collection, bool mustBeVisible)
			{
				m_enumerator = new NodeCollIterator(collection, mustBeVisible);
			}
			public Node Current
			{
				get { return m_enumerator.Current; }
			}
			object IEnumerator.Current
			{
				get { return m_enumerator.Current; }
			}
			public bool MoveNext()
			{
				return m_enumerator.MoveNext();
			}
			public void Reset()
			{
				m_enumerator.Reset();
			}
			public void Dispose()
			{
				m_enumerator.Dispose();
			}
		}
		internal class ForwardNodeEnumerator : IEnumerator<Node>
		{
			Node m_firstNode;
			Node m_current;
			bool m_visible;
			public ForwardNodeEnumerator(Node firstNode, bool mustBeVisible)
			{
				m_firstNode = firstNode;
				m_visible = mustBeVisible;
			}
			public Node Current
			{
				get { return m_current; }
			}
			public void Dispose()
			{
			}
			object IEnumerator.Current
			{
				get { return m_current; }
			}
			public bool MoveNext()
			{
				if (m_firstNode == null)
					return false;
				if (m_current == null)
				{
					m_current = m_firstNode;
					return true;
				}
				if (m_current.HasChildren && m_current.Nodes.FirstNode != null)
				{
					if (m_visible == false || m_current.Expanded)
					{
						m_current = m_current.Nodes.FirstNode;
						return true;
					}
				}
				if (m_current.NextSibling != null)
				{
					m_current = m_current.NextSibling;
					return true;
				}
				// search up the paret tree until we find a parent with a sibling
				while (m_current.Parent != null && m_current.Parent.NextSibling == null)
				{
					m_current = m_current.Parent;
				}

				if (m_current.Parent != null && m_current.Parent.NextSibling != null)
				{
					m_current = m_current.Parent.NextSibling;
					return true;
				}
				m_current = null;
				return false;
			}
			public void Reset()
			{
				m_current = m_firstNode;
			}
		}
		internal class ReverseNodeEnumerator : IEnumerator<Node>
		{
			Node m_firstNode;
			Node m_current;
			bool m_visible;
			public ReverseNodeEnumerator(Node firstNode, bool mustBeVisible)
			{
				m_firstNode = firstNode;
				m_visible = mustBeVisible;
			}
			public Node Current
			{
				get { return m_current; }
			}
			public void Dispose()
			{
			}
			object IEnumerator.Current
			{
				get { return m_current; }
			}
			public bool MoveNext()
			{
				if (m_firstNode == null)
					return false;
				if (m_current == null)
				{
					m_current = m_firstNode;
					return true;
				}
				if (m_current.PrevSibling != null)
				{
					m_current = FindNodesBottomLeaf(m_current.PrevSibling, m_visible);
					return true;
				}
				if (m_current.Parent != null)
				{
					m_current = m_current.Parent;
					return true;
				}
				m_current = null;
				return false;
			}
			public void Reset()
			{
				m_current = m_firstNode;
			}
		}

		public static Node GetNextNode(Node startingNode, int searchOffset)
		{
			if (searchOffset == 0)
				return startingNode;
			if (searchOffset > 0)
			{
				ForwardNodeEnumerator iterator = new ForwardNodeEnumerator(startingNode, true);
				while (searchOffset-- >= 0 && iterator.MoveNext());
				return iterator.Current;
			}
			if (searchOffset < 0)
			{
				ReverseNodeEnumerator iterator = new ReverseNodeEnumerator(startingNode, true);
				while (searchOffset++ <= 0 && iterator.MoveNext());
				return iterator.Current;
			}
			return null;
		}
		
		public static IEnumerable ReverseNodeIterator(Node firstNode, Node lastNode, bool mustBeVisible)
		{
			bool m_done = false;
			ReverseNodeEnumerator iterator = new ReverseNodeEnumerator(firstNode, mustBeVisible);
			while (iterator.MoveNext())
			{
				if (m_done)
					break;
				if (iterator.Current == lastNode)
					m_done = true;
				yield return iterator.Current;
			}
		}
		public static IEnumerable ForwardNodeIterator(Node firstNode, Node lastNode, bool mustBeVisible)
		{
			bool m_done = false;
			ForwardNodeEnumerator iterator = new ForwardNodeEnumerator(firstNode, mustBeVisible);
			while (iterator.MoveNext())
			{
				if (m_done)
					break;
				if (iterator.Current == lastNode)
					m_done = true;
				yield return iterator.Current;
			}
		}
		public static IEnumerable ForwardNodeIterator(Node firstNode, bool mustBeVisible)
		{
				ForwardNodeEnumerator iterator = new ForwardNodeEnumerator(firstNode, mustBeVisible);
				while (iterator.MoveNext())
					yield return iterator.Current;
		}
		public static int GetVisibleNodeIndex(Node node)
		{
			if (node == null || node.IsVisible() == false || node.GetRootCollection() == null)
				return -1;

			// Finding the node index is done by searching up the tree and use the visible node count from each node.
			// First all previous siblings are searched, then when first sibling in the node collection is reached
			// the node is switch to the parent node and the again a search is done up the sibling list.
			// This way only higher up the tree are being iterated while nodes at the same level are skipped.
			// Worst case scenario is if all nodes are at the same level. In that case the search is a linear search.
			
			// adjust count for the visible count of the current node.
			int count = -node.VisibleNodeCount;
			while (node != null)
			{
				count += node.VisibleNodeCount;
				if (node.PrevSibling != null)
					node = node.PrevSibling;
				else
				{
					node = node.Parent;
					if (node != null)
						count -= node.VisibleNodeCount - 1; // -1 is for the node itself
				}
			}
			return count;
		}
		public static Node FindNodesBottomLeaf(Node node, bool mustBeVisible)
		{
			if (mustBeVisible && node.Expanded == false)
				return node;
			if (node.HasChildren == false || node.Nodes.LastNode == null)
				return node;
			node = node.Nodes.LastNode;
			return FindNodesBottomLeaf(node, mustBeVisible);
        }

        #region added by BH

        public bool Contains(Node node)
        {
            foreach (Node child in this)
            {
                if (child.Name == node.Name)
                    return true;
            }

            return false;
        }

        public bool ContainsKey(string nodeName)
        {
            return (null != this[nodeName]);
        }

        public Node this[string nodeName]
        {
            get
            {
                EnsureInternalArray();
                foreach (Node node in m_nodesInternal)
                    if (node.Name == nodeName)
                        return node;

                return null;
            }
            set
            {
                EnsureInternalArray();
                Node n = null;
                foreach (Node node in m_nodesInternal)
                    if (node.Name == nodeName)
                    {
                        n = value;
                        break;
                    }

                n = value;
            }
        }

        public void Sort(SortEventHandler callback)
        {
            Sort(callback, 0, Count - 1);

            Node[] nodes = m_nodesInternal;
            this.Clear();
            foreach (Node n in nodes)
                this.Add(n);
        }

        private void Sort(SortEventHandler callback, int left, int right)
        {
            if (callback == null) return;

            int i = left, j = right;

            Node pivot = this[(left + right) / 2];
            
            while (i <= j)
            {
                while (callback(this[i], pivot) < 0)
                    i++;

                while (callback(this[j], pivot) > 0)
                    j--;

                if (i <= j)
                {
                    // Swap
                    Node tmp = this[i];
                    this[i] = this[j];
                    this[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
                Sort(callback, left, j);

            if (i < right)
                Sort(callback, i, right);
        }

        #endregion
    }

    public delegate int SortEventHandler(Node c1, Node c2);

	public class NodesSelection : IEnumerable
	{
		List<Node>				m_nodes = new List<Node>();
		Dictionary<Node, int>	m_nodesMap = new Dictionary<Node,int>();
		public void Clear()
		{
			m_nodes.Clear();
			m_nodesMap.Clear();
		}
		public IEnumerator GetEnumerator()
		{
			return m_nodes.GetEnumerator();
		}
		public Node this[int index]
		{
			get { return m_nodes[index]; }
		}
		public int Count
		{
			get { return m_nodes.Count; }
		}
		public void Add(Node node)
		{
			m_nodes.Add(node);
			m_nodesMap.Add(node, 0);
		}
		public void Remove(Node node)
		{
			m_nodes.Remove(node);
			m_nodesMap.Remove(node);
		}
		public bool Contains(Node node)
		{
			return m_nodesMap.ContainsKey(node);
		}

		public IList<Node> GetSortedNodes()
		{
			SortedList<string, Node> list = new SortedList<string,Node>();
			foreach (Node node in m_nodes)
				list.Add(node.GetId(), node);
			return list.Values;
		}

	}
}
