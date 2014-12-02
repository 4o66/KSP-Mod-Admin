using System.Collections.Generic;
using System.Linq;
using KSPModAdmin.Utils.CommonTools;

namespace KSPModAdmin.Utils
{
    /// <summary>
    /// The ModRegister class takes track of all file destinations of all added mods.
    /// If two mod files have the same destination they will be marked as colliding.
    /// To solve collisions the ModRegister class will remove the destination of all colliding mods files except the chosen one.
    /// </summary>
    public static class ModRegister
    {
        /// <summary>
        /// Dictionary of all registered mod file destinations.
        /// For destination collision detection.
        /// </summary>
        private static Dictionary<string, List<TreeNodeMod>> m_RegisterdModFiles = new Dictionary<string, List<TreeNodeMod>>();

        
        /// <summary>
        /// Dictionary of all registered mod file destinations.
        /// For destination collision detection.
        /// </summary>
        public static Dictionary<string, List<TreeNodeMod>> RegisterdModFiles
        {
            get { return m_RegisterdModFiles; }
            set { m_RegisterdModFiles = value; }
        }


        /// <summary>
        /// Registers all mod files that have a destination.
        /// </summary>
        /// <param name="modRoot">The root node of the mod to register the file nodes from.</param>
        /// <returns>True if a collision with another mod was detected.</returns>
        public static bool RegisterMod(TreeNodeMod modRoot)
        {
            bool collisionDetected = false;
            List<TreeNodeMod> fileNodes = GetAllNodesWithDestination(modRoot);
            foreach (TreeNodeMod fileNode in fileNodes)
                if (RegisterModFile(fileNode))
                    collisionDetected = true;

            return collisionDetected;
        }

        /// <summary>
        /// Registers the mod file if it has a destination.
        /// </summary>
        /// <param name="fileNode">The file node to register.</param>
        /// <returns>True if a collision with another mod was detected.</returns>
        public static bool RegisterModFile(TreeNodeMod fileNode)
        {
            if (!MainForm.Instance.Options.ConflictDetection)
                return false;

            if (string.IsNullOrEmpty(fileNode.Destination))
                return false;

            if (!m_RegisterdModFiles.ContainsKey(fileNode.Destination))
            {
                // Add fileNode to register
                List<TreeNodeMod> list = new List<TreeNodeMod> { fileNode };
                m_RegisterdModFiles.Add(fileNode.Destination, list);
            }
            else
            {
                if (!m_RegisterdModFiles[fileNode.Destination].Contains(fileNode)) //!AlreadyKnown(fileNode))
                {
                    m_RegisterdModFiles[fileNode.Destination].Add(fileNode);

                    // Set collision flag
                    if (fileNode.IsFile || fileNode.HasChildCollision)
                    {
                        foreach (TreeNodeMod node in m_RegisterdModFiles[fileNode.Destination])
                            node.HasCollision = true;
                    }
                    else
                    {
                        if (HaveCollisionsSameRoot(fileNode) || fileNode.Text.Trim() == Constants.GAMEDATA)
                            return false;

                        foreach (TreeNodeMod node in m_RegisterdModFiles[fileNode.Destination])
                            node.HasCollision = true;
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Clears the complete mod register.
        /// </summary>
        public static void Clear()
        {
            List<TreeNodeMod> toUnregister = new List<TreeNodeMod>();
            foreach (var dest in m_RegisterdModFiles)
                toUnregister.AddRange(dest.Value);

            foreach (var file in toUnregister)
                RemoveRegisteredModFile(file);

            m_RegisterdModFiles.Clear();
        }

        /// <summary>
        /// Removes the registered mod files of a mod.
        /// </summary>
        /// <param name="modRoot">The root node of the mod from which the files should be unregistered.</param>
        public static void RemoveRegisteredMod(TreeNodeMod modRoot)
        {
            RemoveRegisteredModFile(modRoot);
            foreach (TreeNodeMod child in modRoot.Nodes)
                RemoveRegisteredMod(child);
        }

        /// <summary>
        /// Removes the mod file from the registration.
        /// </summary>
        /// <param name="fileNode">The file node to unregister.</param>
        public static void RemoveRegisteredModFile(TreeNodeMod fileNode)
        {
            if (!string.IsNullOrEmpty(fileNode.Destination) && m_RegisterdModFiles.ContainsKey(fileNode.Destination) &&
                m_RegisterdModFiles[fileNode.Destination].Contains(fileNode))
            {
                m_RegisterdModFiles[fileNode.Destination].Remove(fileNode);
                fileNode.HasCollision = false;

                if  (m_RegisterdModFiles[fileNode.Destination].Count == 0)
                    m_RegisterdModFiles.Remove(fileNode.Destination);
                else if  (m_RegisterdModFiles[fileNode.Destination].Count == 1)
                    m_RegisterdModFiles[fileNode.Destination][0].HasCollision = false;
                else if (m_RegisterdModFiles[fileNode.Destination].Count > 1)
                {
                    if (HaveCollisionsSameRoot(fileNode))
                        m_RegisterdModFiles[fileNode.Destination][0].HasCollision = false;
                }
            }
        }

        /// <summary>
        /// Solves the destination collision for a Mod.
        /// Removes the destination of all mod files registered to the destination of 
        /// </summary>
        /// <param name="modRoot">The mod to keep the destination.</param>
        public static void SolveCollisions(TreeNodeMod modRoot)
        {
            var collidingNodes = GetAllCollisionNodes(modRoot);
            foreach (TreeNodeMod collidingNode in collidingNodes)
            {
                if (string.IsNullOrEmpty(collidingNode.Destination) || !m_RegisterdModFiles.ContainsKey(collidingNode.Destination))
                {
                    collidingNode.HasCollision = false;
                    continue;
                }

                List<TreeNodeMod> removeDestinationNodes = m_RegisterdModFiles[collidingNode.Destination].Where(node => node != collidingNode).ToList();
                foreach (TreeNodeMod delNode in removeDestinationNodes)
                {
                    RemoveRegisteredModFile(delNode);

                    MainForm.Instance.ModSelection.tvModSelection.ChangeCheckedState(delNode, false, true, true); 

                    if (!delNode.IsFile && delNode.IsInstalled)
                        MainForm.Instance.ModSelection.ProcessNodes(new TreeNodeMod[] {delNode}, true);

                    MainForm.Instance.ModSelection.SetDestinationPaths(delNode, "");
                }
            }
        }

        public static List<TreeNodeMod> GetCollisionModFiles(TreeNodeMod modNode)
        {
            List<TreeNodeMod> result = new List<TreeNodeMod>();
            if (!MainForm.Instance.Options.ConflictDetection)
                return result;

            if (modNode.IsFile)
            {
                if (m_RegisterdModFiles.ContainsKey(modNode.Destination))
                    result.AddRange(m_RegisterdModFiles[modNode.Destination].Where(node => node != modNode));
            }
            else
            {
                foreach (var fileNode in MainForm.Instance.ModSelection.GetAllFileNodes(modNode.ZipRoot))
                {
                    if (m_RegisterdModFiles.ContainsKey(fileNode.Destination))
                        result.AddRange(m_RegisterdModFiles[fileNode.Destination].Where(regFileNode => regFileNode != modNode));
                }
            }
            return result;
        }

        public static List<TreeNodeMod> GetCollisionModsByCollisionMod(TreeNodeMod value)
        {
            List<TreeNodeMod> result = new List<TreeNodeMod>();
            if (!MainForm.Instance.Options.ConflictDetection)
                return result;

            foreach (var fileNode in MainForm.Instance.ModSelection.GetAllFileNodes(value.ZipRoot))
            {
                if (m_RegisterdModFiles.ContainsKey(fileNode.Destination))
                {
                    foreach (var regFileNode in m_RegisterdModFiles[fileNode.Destination])
                    {
                        if (regFileNode != value && !result.Contains(regFileNode.ZipRoot))
                            result.Add(regFileNode.ZipRoot);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a list of TreeNodeMod that have a colliding destination.
        /// </summary>
        /// <param name="node">The node to start the search from.</param>
        /// <param name="fileNodes">For recursive calls! List of already found file nodes.</param>
        /// <returns>A list of TreeNodeMod that have a colliding destination.</returns>
        public static List<TreeNodeMod> GetAllCollisionNodes(TreeNodeMod node, List<TreeNodeMod> fileNodes = null)
        {
            if (fileNodes == null)
                fileNodes = new List<TreeNodeMod>();

            if (!MainForm.Instance.Options.ConflictDetection)
                return fileNodes;

            if (node.HasCollision)
                fileNodes.Add(node);

            foreach (TreeNodeMod childNode in node.Nodes)
                GetAllCollisionNodes(childNode, fileNodes);

            return fileNodes;
        }


        private static bool HaveCollisionsSameRoot(TreeNodeMod fileNode)
        {
            bool differentRootFound = false;

            TreeNodeMod zipRoot = fileNode.ZipRoot;
            foreach (TreeNodeMod node in m_RegisterdModFiles[fileNode.Destination])
            {
                if (node.ZipRoot == zipRoot)
                    continue;

                differentRootFound = true;
                break;
            }

            return !differentRootFound;
        }

        private static List<TreeNodeMod> GetAllNodesWithDestination(TreeNodeMod node, List<TreeNodeMod> list = null)
        {
            if (list == null)
                list = new List<TreeNodeMod>();

            if (node.HasDestination)
                list.Add(node);

            foreach (TreeNodeMod n in node.Nodes)
                GetAllNodesWithDestination(n, list);

            return list;
        }
    }
}
