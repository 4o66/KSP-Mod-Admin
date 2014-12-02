using System;
using System.IO;

namespace KSPModAdmin
{
    public enum VersionControl
    {
        Spaceport,
        KSPForum,
        CurseForge,
        KerbalStuff,
        None
    }

    public class ModInfo
    {
        public string Name { get; set; }

        public string SpaceportURL { get; set; }

        public string ForumURL { get; set; }

        public string CurseForgeURL { get; set; }

        public string KerbalStuffURL { get; set; }

        public string LocalPath { get; set; }

        public string DownloadDate { get; set; }

        public DateTime DownloadDateAsDateTime
        {
            get
            {
                if (string.IsNullOrEmpty(DownloadDate))
                    return DateTime.MinValue;
                else
                {
                    DateTime value = DateTime.MinValue;
                    if (DateTime.TryParse(DownloadDate, out value))
                        return value;
                    else
                        return DateTime.MinValue;
                }
            }
            set
            {
                if (value == DateTime.MinValue)
                    DownloadDate = string.Empty;
                else
                    DownloadDate = value.ToString();
            }
        }

        public string CreationDate { get; set; }

        public DateTime CreationDateAsDateTime
        {
            get
            {
                if (string.IsNullOrEmpty(CreationDate))
                    return DateTime.MinValue;
                else
                {
                    DateTime value = DateTime.MinValue;
                    if (DateTime.TryParse(CreationDate, out value))
                        return value;
                    else
                        return DateTime.MinValue;
                }
            }
            set
            {
                if (value == DateTime.MinValue)
                    CreationDate = string.Empty;
                else
                    CreationDate = value.ToString();
            }
        }

        public string Rating { get; set; }

        public string Downloads { get; set; }

        public string Author { get; set; }

        public string ProductID { get; set; }

        public VersionControl VersionControl { get; set; }


        public bool IsArchive
        {
            get
            {
                if (LocalPath.ToLower().EndsWith(Constants.EXT_ZIP) ||
                    LocalPath.ToLower().EndsWith(Constants.EXT_7ZIP) ||
                    LocalPath.ToLower().EndsWith(Constants.EXT_RAR))
                    return true;

                return false;
            }
        }

        public bool IsCraft
        {
            get
            {
                return LocalPath.ToLower().EndsWith(Constants.EXT_CRAFT);
            }
        }


        public ModInfo()
        {
            Name = string.Empty;

            SpaceportURL = string.Empty;
            ForumURL = string.Empty;
            CurseForgeURL = string.Empty;
            KerbalStuffURL = string.Empty;

            LocalPath = string.Empty;

            DownloadDate = DateTime.Now.ToString();
            CreationDate = DownloadDate;

            Rating = "0 (0)";

            Downloads = "0";

            Author = string.Empty;

            ProductID = "";

            VersionControl = VersionControl.None;
        }

        public ModInfo(string localPath, string spaceportURL = null)
        {
            Name = Path.GetFileNameWithoutExtension(localPath);

            SpaceportURL = (string.IsNullOrEmpty(spaceportURL)) ? string.Empty : spaceportURL;
            ForumURL = string.Empty;
            CurseForgeURL = string.Empty;

            LocalPath = localPath;

            DownloadDate = DateTime.Now.ToString();
            CreationDate = DownloadDate;

            Rating = "0 (0)";

            Downloads = "0";

            Author = string.Empty;

            ProductID = "";

            VersionControl = (string.IsNullOrEmpty(spaceportURL)) ? VersionControl.None : VersionControl.Spaceport;
        }
    }
}
