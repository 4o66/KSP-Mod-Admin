using System;
using System.Drawing;
using System.IO;
using System.Xml;
using KSPModAdmin.Utils;
using System.Collections.Generic;

namespace KSPModAdmin.Config
{
    /// <summary>
    /// The config for all needed infos of the KSP MOD Admin.
    /// </summary>
    public class AdminConfig
    {
        #region Members

        /// <summary>
        /// Version of the save file.
        /// </summary>
        protected const string mVersion = "v1.0";

        #endregion

        #region Properties

        /// <summary>
        /// Last position of the main window.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Last size of the main window.
        /// </summary>
        public Size Size { get { return new Size(Width, Height); } set { Width = value.Width; Height = value.Height; } }

        /// <summary>
        /// Last width of the main window.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Last height of the main window.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The KSP installation path.
        /// </summary>
        public bool HasValidKSP_Path
        {
            get
            {
                bool result = false;
                try
                {
                    if (Directory.Exists(KSP_Path))
                        result = true;
                }
                catch (Exception ex)
                {
                    string NoWarningPLS = ex.Message;
                }

                return result;
            }
        }

        /// <summary>
        /// The last selected KSP installation path.
        /// </summary>
        public string KSP_Path { get; set; }

        /// <summary>
        /// Gets or sets the list of known KSP install paths.
        /// </summary>
        public List<PathInfo> KnownPaths { get; set; }

        /// <summary>
        /// The action the should be performed after an update download.
        /// </summary>
        public PostDownloadAction PostDownloadAction { get; set; }

        /// <summary>
        /// Flag to determine if a update check should be done on startup.
        /// </summary>
        public bool CheckForUpdates { get; set; }

        /// <summary>
        /// DateTime of the last mod update try.
        /// </summary>
        public DateTime LastModUpdateTry { get; set; }

        /// <summary>
        /// The interval of mod updating.
        /// </summary>
        public ModUpdateInterval ModUpdateInterval { get; set; }

        /// <summary>
        /// The interval of mod updating.
        /// </summary>
        public ModUpdateBehavior ModUpdateBehavior { get; set; }

        /// <summary>
        /// Flag to determine if the Form should start in maximized status.
        /// </summary>
        public bool Maximized { get; set; }

        /// <summary>
        /// Flag to determine if the Form should start in minimized status.
        /// </summary>
        public bool Minimized { get; set; }


        public bool ShowConflictSolver { get; set; }
        public bool ConflictDetection { get; set; }
        public bool FolderConflictDetection { get; set; }

        public Color ColorDestinationDetected { get; set; }
        public Color ColorDestinationMissing { get; set; }
        public Color ColorDestinationConflict { get; set; }
        public Color ColorModInstalled { get; set; }
        public Color ColorModArchiveMissing { get; set; }
        public Color ColorModOutdated { get; set; }

        #endregion


        public AdminConfig()
        {
            Position = Point.Empty;
            Width = 480;
            Height = 650;
            KSP_Path = Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX;
            KnownPaths = new List<PathInfo>();
            PostDownloadAction = PostDownloadAction.Ask;
            CheckForUpdates = true;
            LastModUpdateTry = DateTime.MinValue;
            ModUpdateInterval = ModUpdateInterval.OnceADay;
            ModUpdateBehavior = ModUpdateBehavior.CopyDestination;
            Maximized = false;
            Minimized = false;
            ConflictDetection = true;
            FolderConflictDetection = false;
            ShowConflictSolver = false;
            ColorDestinationConflict = Color.Orange;
            ColorDestinationDetected = Color.Black;
            ColorDestinationMissing = Color.FromArgb(255, 130, 130, 130);
            ColorModArchiveMissing = Color.Red;
            ColorModInstalled = Color.Green;
            ColorModOutdated = Color.Blue;
        }


        /// <summary>
        /// Loads the config.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Load(string path)
        {
            bool result = false;
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
                            result = LoadV1_0(doc);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MainForm.Instance.AddError("Error: " + ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Saves the config.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Save(string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode root = doc.CreateElement(Constants.ROOTNODE);
            doc.AppendChild(root);

            XmlNode node = doc.CreateElement(Constants.VERSION);
            node.InnerText = mVersion;
            root.AppendChild(node);

            XmlNode generalNode = doc.CreateElement(Constants.GENERAL);
            root.AppendChild(generalNode);

            node = doc.CreateElement(Constants.POSITION);
            XmlAttribute nodeAttribute = doc.CreateAttribute(Constants.X);
            nodeAttribute.Value = Position.X.ToString();
            node.Attributes.Append(nodeAttribute);
            nodeAttribute = doc.CreateAttribute(Constants.Y);
            nodeAttribute.Value = Position.Y.ToString();
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            node = doc.CreateElement(Constants.SIZE);
            nodeAttribute = doc.CreateAttribute(Constants.WIDTH);
            nodeAttribute.Value = Width.ToString();
            node.Attributes.Append(nodeAttribute);
            nodeAttribute = doc.CreateAttribute(Constants.HEIGHT);
            nodeAttribute.Value = Height.ToString();
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            node = doc.CreateElement(Constants.WINDOWSTATE);
            nodeAttribute = doc.CreateAttribute(Constants.MAXIM);
            nodeAttribute.Value = Maximized.ToString();
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            int i = 0;
            node = doc.CreateElement(Constants.MODSELECTIONCOLUMNS);
            foreach (var col in MainForm.Instance.ModSelection.tvModSelection.Columns)
            {
                XmlNode columnNode = doc.CreateElement(Constants.COLUMN);

                nodeAttribute = doc.CreateAttribute(Constants.ID);
                nodeAttribute.Value = i.ToString();
                columnNode.Attributes.Append(nodeAttribute);

                nodeAttribute = doc.CreateAttribute(Constants.WIDTH);
                nodeAttribute.Value = col.Width.ToString();
                columnNode.Attributes.Append(nodeAttribute);

                node.AppendChild(columnNode);
                i++;
            }
            generalNode.AppendChild(node);

            node = doc.CreateElement(Constants.CONFLICTDETECTIONOPTIONS);
            nodeAttribute = doc.CreateAttribute(Constants.ONOFF);
            nodeAttribute.Value = ConflictDetection.ToString();
            node.Attributes.Append(nodeAttribute);
            nodeAttribute = doc.CreateAttribute(Constants.FOLDERCONFLICTDETECTION);
            nodeAttribute.Value = FolderConflictDetection.ToString();
            node.Attributes.Append(nodeAttribute);
            nodeAttribute = doc.CreateAttribute(Constants.SHOWCONFLICTSOLVER);
            nodeAttribute.Value = ShowConflictSolver.ToString();
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            node = doc.CreateElement(Constants.NODECOLORS);
            generalNode.AppendChild(node);

            XmlNode childNode = doc.CreateElement(Constants.DESTINATIONDETECTED);
            nodeAttribute = doc.CreateAttribute(Constants.COLOR);
            nodeAttribute.Value = String.Format("{0};{1};{2}", ColorDestinationDetected.R, ColorDestinationDetected.G, ColorDestinationDetected.B);
            childNode.Attributes.Append(nodeAttribute);
            node.AppendChild(childNode);

            childNode = doc.CreateElement(Constants.DESTINATIONMISSING);
            nodeAttribute = doc.CreateAttribute(Constants.COLOR);
            nodeAttribute.Value = String.Format("{0};{1};{2}", ColorDestinationMissing.R, ColorDestinationMissing.G, ColorDestinationMissing.B);
            childNode.Attributes.Append(nodeAttribute);
            node.AppendChild(childNode);

            childNode = doc.CreateElement(Constants.DESTINATIONCONFLICT);
            nodeAttribute = doc.CreateAttribute(Constants.COLOR);
            nodeAttribute.Value = String.Format("{0};{1};{2}", ColorDestinationConflict.R, ColorDestinationConflict.G, ColorDestinationConflict.B);
            childNode.Attributes.Append(nodeAttribute);
            node.AppendChild(childNode);

            childNode = doc.CreateElement(Constants.MODINSTALLED);
            nodeAttribute = doc.CreateAttribute(Constants.COLOR);
            nodeAttribute.Value = String.Format("{0};{1};{2}", ColorModInstalled.R, ColorModInstalled.G, ColorModInstalled.B);
            childNode.Attributes.Append(nodeAttribute);
            node.AppendChild(childNode);

            childNode = doc.CreateElement(Constants.MODARCHIVEMISSING);
            nodeAttribute = doc.CreateAttribute(Constants.COLOR);
            nodeAttribute.Value = String.Format("{0};{1};{2}", ColorModArchiveMissing.R, ColorModArchiveMissing.G, ColorModArchiveMissing.B);
            childNode.Attributes.Append(nodeAttribute);
            node.AppendChild(childNode);

            childNode = doc.CreateElement(Constants.MODOUTDATED);
            nodeAttribute = doc.CreateAttribute(Constants.COLOR);
            nodeAttribute.Value = String.Format("{0};{1};{2}", ColorModOutdated.R, ColorModOutdated.G, ColorModOutdated.B);
            childNode.Attributes.Append(nodeAttribute);
            node.AppendChild(childNode);

            XmlNode pathNode = doc.CreateElement(Constants.KSP_PATH);
            XmlAttribute pathNodeAttribute = doc.CreateAttribute(Constants.NAME);
            pathNodeAttribute.Value = KSP_Path;
            pathNode.Attributes.Append(pathNodeAttribute);
            generalNode.AppendChild(pathNode);

            XmlNode pathNodes = doc.CreateElement(Constants.KNOWN_KSP_PATHS);
            foreach (PathInfo info in KnownPaths)
            {
                pathNode = doc.CreateElement(Constants.KNOWN_KSP_PATH);

                pathNodeAttribute = doc.CreateAttribute(Constants.FULLPATH);
                pathNodeAttribute.Value = info.FullPath;
                pathNode.Attributes.Append(pathNodeAttribute);

                pathNodeAttribute = doc.CreateAttribute(Constants.NOTE);
                pathNodeAttribute.Value = info.Note;
                pathNode.Attributes.Append(pathNodeAttribute);

                pathNodes.AppendChild(pathNode);
            }
            generalNode.AppendChild(pathNodes);

            node = doc.CreateElement(Constants.POSTDOWNLOADACTION);
            nodeAttribute = doc.CreateAttribute(Constants.VALUE);
            nodeAttribute.Value = ((int)PostDownloadAction).ToString();
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            node = doc.CreateElement(Constants.CHECKFORUPDATES);
            nodeAttribute = doc.CreateAttribute(Constants.VALUE);
            nodeAttribute.Value = CheckForUpdates.ToString();
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            node = doc.CreateElement(Constants.LASTMODUPDATETRY);
            nodeAttribute = doc.CreateAttribute(Constants.VALUE);
            nodeAttribute.Value = LastModUpdateTry.ToString();
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            node = doc.CreateElement(Constants.MODUPDATEINTERVAL);
            nodeAttribute = doc.CreateAttribute(Constants.VALUE);
            nodeAttribute.Value = ((int)ModUpdateInterval).ToString();
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            node = doc.CreateElement(Constants.MODUPDATEBEHAVIOR);
            nodeAttribute = doc.CreateAttribute(Constants.VALUE);
            nodeAttribute.Value = ((int)ModUpdateBehavior).ToString();
            node.Attributes.Append(nodeAttribute);
            generalNode.AppendChild(node);

            doc.Save(path);

            return true;
        }

        #region Load

        /// <summary>
        /// v1.0 load function.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private bool LoadV1_0(XmlDocument doc)
        {
            XmlNodeList pos = doc.GetElementsByTagName(Constants.POSITION);
            if (pos.Count >= 1)
            {
                int x = 0;
                int y = 0;
                foreach (XmlAttribute att in pos[0].Attributes)
                {
                    if (att.Name == Constants.X)
                        int.TryParse(att.Value, out x);
                    else if (att.Name == Constants.Y)
                        int.TryParse(att.Value, out y);
                }

                Position = new Point(x, y);
            }

            XmlNodeList size = doc.GetElementsByTagName(Constants.SIZE);
            if (size.Count >= 1)
            {
                Width = int.Parse(size[0].Attributes[Constants.WIDTH].Value);
                Height = int.Parse(size[0].Attributes[Constants.HEIGHT].Value);
            }

            XmlNodeList maxim = doc.GetElementsByTagName(Constants.WINDOWSTATE);
            if (maxim.Count >= 1)
            {
                foreach (XmlAttribute att in maxim[0].Attributes)
                {
                    if (att.Name == Constants.MAXIM && att.Value != null)
                        Maximized = (att.Value.ToLower() == "true");
                }
            }
            
            XmlNodeList colWidths = doc.GetElementsByTagName(Constants.COLUMN);
            if (colWidths.Count >= 1)
            {
                foreach (XmlNode col in colWidths)
                {
                    int id = -1;
                    int width = 0;
                    foreach (XmlAttribute att in col.Attributes)
                    {
                        if (att.Name == Constants.ID && !string.IsNullOrEmpty(att.Value))
                            id = int.Parse(att.Value);
                        else if (att.Name == Constants.WIDTH && !string.IsNullOrEmpty(att.Value))
                            width = int.Parse(att.Value);
                    }

                    if (id >= 0 && MainForm.Instance.ModSelection.tvModSelection.Columns.Count > id)
                    {
                        MainForm.Instance.ModSelection.tvModSelection.Columns[id].Width = width;
                        MainForm.Instance.ModSelection.tvModSelection.Columns[id].MinWidth = width;
                    }
                }
            }

            XmlNodeList conflictDetectionOnnOff = doc.GetElementsByTagName(Constants.CONFLICTDETECTIONOPTIONS);
            if (conflictDetectionOnnOff.Count >= 1)
            {
                foreach (XmlAttribute att in conflictDetectionOnnOff[0].Attributes)
                {
                    if (att.Name == Constants.ONOFF && att.Value != null)
                        ConflictDetection = (att.Value.ToLower() == "true");
                    else if (att.Name == Constants.FOLDERCONFLICTDETECTION && att.Value != null)
                        FolderConflictDetection = (att.Value.ToLower() == "true");
                    else if (att.Name == Constants.SHOWCONFLICTSOLVER && att.Value != null)
                        ShowConflictSolver = (att.Value.ToLower() == "true");
                }
            }

            XmlNodeList colorDestinationDetected = doc.GetElementsByTagName(Constants.DESTINATIONDETECTED);
            if (colorDestinationDetected.Count >= 1)
            {
                foreach (XmlAttribute att in colorDestinationDetected[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        ColorDestinationDetected = GetColor(att.Value);
                }
            }

            XmlNodeList colorDestinationMissing = doc.GetElementsByTagName(Constants.DESTINATIONMISSING);
            if (colorDestinationMissing.Count >= 1)
            {
                foreach (XmlAttribute att in colorDestinationMissing[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        ColorDestinationMissing = GetColor(att.Value);
                }
            }

            XmlNodeList colorDestinationConflict = doc.GetElementsByTagName(Constants.DESTINATIONCONFLICT);
            if (colorDestinationConflict.Count >= 1)
            {
                foreach (XmlAttribute att in colorDestinationConflict[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        ColorDestinationConflict = GetColor(att.Value);
                }
            }

            XmlNodeList colorModInstalled = doc.GetElementsByTagName(Constants.MODINSTALLED);
            if (colorModInstalled.Count >= 1)
            {
                foreach (XmlAttribute att in colorModInstalled[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        ColorModInstalled = GetColor(att.Value);
                }
            }

            XmlNodeList colorModArchiveMissing = doc.GetElementsByTagName(Constants.MODARCHIVEMISSING);
            if (colorModArchiveMissing.Count >= 1)
            {
                foreach (XmlAttribute att in colorModArchiveMissing[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        ColorModArchiveMissing = GetColor(att.Value);
                }
            }

            XmlNodeList colorModOutdated = doc.GetElementsByTagName(Constants.MODOUTDATED);
            if (colorModOutdated.Count >= 1)
            {
                foreach (XmlAttribute att in colorModOutdated[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        ColorModOutdated = GetColor(att.Value);
                }
            }

            XmlNodeList nodes = doc.GetElementsByTagName(Constants.KSP_PATH);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if (att.Name == Constants.NAME && !string.IsNullOrEmpty(att.Value))
                    {
                        PathInfo pInfo = GetKSPMA_PathInfo(att.Value, string.Empty);
                        if (pInfo.ValidPath)
                            KSP_Path = att.Value;
                        else
                            KSP_Path = Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX;
                        break;
                    }
                }
            }

            nodes = doc.GetElementsByTagName(Constants.KNOWN_KSP_PATH);
            if (nodes.Count >= 1)
            {
                foreach (XmlNode node in nodes)
                {
                    string kspPath = string.Empty;
                    string noteValue = string.Empty;
                    foreach (XmlAttribute att in node.Attributes)
                    {
                        if (att.Name == Constants.FULLPATH)
                            kspPath = att.Value;
                        else if (att.Name == Constants.NOTE)
                            noteValue = att.Value;
                    }
                    PathInfo pInfo = GetKSPMA_PathInfo(kspPath, noteValue);
                    if (pInfo.ValidPath)
                        KnownPaths.Add(pInfo);
                }
            }

            if (KSP_Path.Contains(Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX.Trim()) && KnownPaths.Count > 0)
                KSP_Path = KnownPaths[0].FullPath;

            nodes = doc.GetElementsByTagName(Constants.POSTDOWNLOADACTION);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if (att.Name == Constants.VALUE)
                    {
                        try
                        {
                            switch (int.Parse(att.Value))
                            {
                                case (int)PostDownloadAction.Ask:
                                    PostDownloadAction = PostDownloadAction.Ask;
                                    break;
                                case (int)PostDownloadAction.AutoUpdate:
                                    PostDownloadAction = PostDownloadAction.AutoUpdate;
                                    break;
                                case (int)PostDownloadAction.Ignore:
                                    PostDownloadAction = PostDownloadAction.Ignore;
                                    break;
                            }
                        }
                        catch
                        {
                            PostDownloadAction = PostDownloadAction.Ask;
                        }
                    }
                }
            }

            nodes = doc.GetElementsByTagName(Constants.CHECKFORUPDATES);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if ((att.Name == Constants.VALUE || att.Name == Constants.CHECKFORUPDATES) && att.Value != null)
                        CheckForUpdates = (att.Value.ToLower() == "true");
                }
            }

            nodes = doc.GetElementsByTagName(Constants.LASTMODUPDATETRY);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if (att.Name == Constants.VALUE && att.Value != null)
                        try { LastModUpdateTry = DateTime.Parse(att.Value); }
                        catch { }
                }
            }

            nodes = doc.GetElementsByTagName(Constants.MODUPDATEINTERVAL);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if (att.Name == Constants.VALUE && att.Value != null)
                        try { ModUpdateInterval = (ModUpdateInterval)int.Parse(att.Value); }
                        catch { }
                }
            }

            nodes = doc.GetElementsByTagName(Constants.MODUPDATEBEHAVIOR);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if (att.Name == Constants.VALUE && att.Value != null)
                        try { ModUpdateBehavior = (ModUpdateBehavior)int.Parse(att.Value); }
                        catch { }
                }
            }

            // fallback for older version then 1.2.6
            if (KnownPaths.Count == 0 && !KSP_Path.Contains(Constants.MESSAGE_SELECT_KSP_FOLDER_TXTBOX.Trim()))
                KnownPaths.Add(GetKSPMA_PathInfo(KSP_Path, ""));

            return true;
        }

        private Color GetColor(string colorAsString)
        {
            string[] rgb = colorAsString.Split(';');
            return Color.FromArgb(255, int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
        }

        private static PathInfo GetKSPMA_PathInfo(string kspPath, string note)
        {
            PathInfo info = new PathInfo();
            info.FullPath = kspPath;
            info.Note = note;

            bool valid = false;
            try
            {
                if (File.Exists(Path.Combine(info.FullPath, Constants.KSP_EXE)) ||
                    File.Exists(Path.Combine(info.FullPath, Constants.KSP_EXE + "_fake")) ||
                    File.Exists(Path.Combine(info.FullPath, Constants.KSP_X64_EXE)))
                    valid = true;
            }
            catch (Exception ex)
            {
                string NoWarningPLS = ex.Message;
            }
            info.ValidPath = valid;
            return info;
        }

        #endregion
    }
}
