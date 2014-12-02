using System;

namespace KSPModAdmin.Utils
{
    [Serializable()]
    public class PathInfo
    {
        public string FullPath { get; set; }

        public string Note { get; set; }

        public bool ValidPath { get; set; }


        public PathInfo()
        {
            FullPath = string.Empty;
            Note = string.Empty;
            ValidPath = false;
        }

        public PathInfo(string fullPath, string note, bool validPath)
        {
            FullPath = fullPath;
            Note = note;
            ValidPath = validPath;
        }
    }
}
