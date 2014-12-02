using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Policy;

namespace KSPModAdmin.Utils
{
    public static class CurseForgeHelper
    {
        /// <summary>
        /// Gets the url of the KSP Spaceport site that returns the download URL
        /// </summary>
        public static string DownloadFormUrl
        {
            get
            {
                return "http://kerbalspaceport.com/wp/wp-admin/admin-ajax.php";
            }
        }


        /// <summary>
        /// Checks if the passed url is a spaceport link (url).
        /// </summary>
        /// <param name="url">The url to check.</param>
        /// <returns>True if the passed url is a valid Spaceport link, otherwise false.</returns>
        public static bool IsValidURL(string url)
        {
            return (!string.IsNullOrEmpty(url) && (url.ToLower().StartsWith("http://kerbal.curseforge.com/") || url.ToLower().StartsWith("http://www.kerbal.curseforge.com/")));
        }

        /// <summary>
        /// Gets the content of the site of the passed URL and parses it for ModInfos.
        /// </summary>
        /// <param name="url">The URL of the site to parse the ModInfos from.</param>
        /// <returns>The ModInfos parsed from the site of the passed URL.</returns>
        public static ModInfo GetModInfo(string url)
        {
            ModInfo modInfo = new ModInfo();
            modInfo.CurseForgeURL = url;
            modInfo.VersionControl = VersionControl.CurseForge;
            if (ParseSite(wwwHelper.Load(url), ref modInfo))
                return modInfo;
            else
                return null;
        }

        /// <summary>
        /// Connects to the KSP Spaceport and gets the download url of the mod with the passed ProducID.
        /// </summary>
        /// <param name="produktID">The ProductID of the mod to get the download url for.</param>
        /// <returns>The download url of the mod with the passed ProducID.</returns>
        public static string GetDownloadURL(ModInfo modInfo)
        {
            if (modInfo.CurseForgeURL.EndsWith("/"))
                return modInfo.CurseForgeURL + "files/latest";
            else
                return modInfo.CurseForgeURL + "/files/latest";
        }

        /// <summary>
        /// Connects to the KSP Spaceport and gets the download url of the mod with the passed ProducID.
        /// </summary>
        /// <param name="produktID">The ProductID of the mod to get the download url for.</param>
        /// <returns>The download url of the mod with the passed ProducID.</returns>
        public static string GetFilesURL(ModInfo modInfo)
        {
            if (modInfo.CurseForgeURL.EndsWith("/"))
                return modInfo.CurseForgeURL + "files";
            else
                return modInfo.CurseForgeURL + "/files";
        }

        /// <summary>
        /// Downloads a mod from KSP Spaceport.
        /// </summary>
        /// <param name="modInfo"></param>
        /// <returns></returns>
        public static bool DownloadMod(ref ModInfo modInfo, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
            if (modInfo == null)
                return false;

            string downloadURL = GetDownloadURL(modInfo);

            string siteContent = wwwHelper.Load(GetFilesURL(modInfo));
            string filename = GetFileName(siteContent);
            modInfo.LocalPath = Path.Combine(MainForm.Instance.Options.DownloadPath, filename);

            wwwHelper.DownloadFile(downloadURL, modInfo.LocalPath, downloadProgressHandler);
            
            return File.Exists(modInfo.LocalPath);
        }

        private static string GetFileName(string siteContent)
        {
            siteContent = siteContent.Replace(Environment.NewLine, "");

            string searchString = "<li class=\"e-project-file-name\">";
            int i1 = siteContent.IndexOf(searchString) + searchString.Length;
            if (i1 < 0) return "";
            int i2 = siteContent.IndexOf("</a>", i1);
            if (i2 < 0) return "";
            siteContent = siteContent.Substring(i1, i2 - i1);
            int index = siteContent.IndexOf("\">") + 2;
            string fileName = siteContent.Substring(index);
            return fileName.Trim();
        }

        /// <summary>
        /// Starts a async download of a mod from KSP Spaceport.
        /// </summary>
        /// <param name="modInfo"></param>
        /// <param name="finished"></param>
        /// <param name="progressChanged"></param>
        public static void DownloadModAsync(ref ModInfo modInfo, AsyncResultHandler<bool> finished = null, AsyncProgressChangedHandler progressChanged = null)
        {
            string downloadURL = GetDownloadURL(modInfo);

            // get save path 
            int start = downloadURL.LastIndexOf("/") + 1;
            string filename = downloadURL.Substring(start, downloadURL.Length - start);
            modInfo.LocalPath = Path.Combine(MainForm.Instance.Options.DownloadPath, filename);

            AsyncTask<bool> asyncJob = new AsyncTask<bool>();
            asyncJob.SetDownloadCallbackFunctions(modInfo.SpaceportURL, modInfo.LocalPath, finished, progressChanged);
            asyncJob.RunDownload();
        }

        /// <summary>
        /// Parses the siteContent for ModInfo data.
        /// </summary>
        /// <param name="siteContent">The KSP Spaceport site content for a mod.</param>
        /// <param name="modInfo">The ModInfo to fill with the data of the site.</param>
        private static bool ParseSite(string siteContent, ref ModInfo modInfo)
        {
            int i1 = modInfo.CurseForgeURL.LastIndexOf("/") + 1;
            if (i1 < 0) return false;
            int i2 = modInfo.CurseForgeURL.IndexOf("-", i1);
            if (i2 < 0) return false;
            int l = i2 - i1;
            if (l <= 0) return false;
            modInfo.ProductID = modInfo.CurseForgeURL.Substring(i1, l);

            string searchString = "<h1 class=\"project-title\">";
            i1 = siteContent.IndexOf(searchString) + searchString.Length;
            if (i1 < 0) return false;
            i2 = siteContent.IndexOf("</h1>", i1);
            if (i2 < 0) return false;
            modInfo.Name = GetName(siteContent.Substring(i1, i2 - i1));
            siteContent = siteContent.Substring(i2);

            // get creation date
            searchString = "<li>Created: <span><abbr class=\"tip standard-date standard-datetime\" title=\"";
            int index = siteContent.IndexOf(searchString);
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + searchString.Length);
            index = siteContent.IndexOf("\"");
            if (index < 0) return false;
            string creationDate = siteContent.Substring(0, index).Trim();
            modInfo.CreationDate = GetDateTime(creationDate).ToString();

            // get last released file date
            searchString = "<li>Last Released File: <span><abbr class=\"tip standard-date standard-datetime\" title=\"";
            index = siteContent.IndexOf(searchString);
            if (index >= 0)
            { 
                siteContent = siteContent.Substring(index + searchString.Length);
                index = siteContent.IndexOf("\"");
                if (index >= 0)
                { 
                    creationDate = siteContent.Substring(0, index).Trim();
                    if (GetDateTime(creationDate) > modInfo.CreationDateAsDateTime)
                        modInfo.CreationDate = GetDateTime(creationDate).ToString();
                }
            }

            // get rating count
            index = siteContent.IndexOf("<li>Total Downloads: <span>");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + 27);
            index = siteContent.IndexOf("</span>");
            if (index < 0) return false;
            modInfo.Downloads = siteContent.Substring(0, index).Trim();

            // more infos could be parsed here (like: short description, Tab content (overview, installation, ...), comments, ...)
            return true;
        }

        private static string GetName(string titleHTMLString)
        {
            int index = titleHTMLString.IndexOf("title=\"");
            titleHTMLString = titleHTMLString.Substring(index + 7);
            index = titleHTMLString.IndexOf("\">");
            string name = titleHTMLString.Substring(0, index);
            return name.Trim();
        }

        private static DateTime GetDateTime(string curseForgeDateString)
        {
            int index = curseForgeDateString.IndexOf(",") + 2;
            curseForgeDateString = curseForgeDateString.Substring(index);
            index = curseForgeDateString.IndexOf(" ") + 1;
            index = curseForgeDateString.IndexOf(" ", index) + 1;
            index = curseForgeDateString.IndexOf(" ", index) + 1;
            index = curseForgeDateString.IndexOf(" ", index);
            string date = curseForgeDateString.Substring(0, index);
            index = curseForgeDateString.IndexOf("(");
            string tempTimeZone = curseForgeDateString.Substring(index).Substring(0);
            index = tempTimeZone.IndexOf("-");
            if (index < 0)
                index = tempTimeZone.IndexOf("+") + 1;
            else
                index += 1;
            string timeZone = tempTimeZone.Substring(0, index);
            tempTimeZone = tempTimeZone.Substring(index);
            index = tempTimeZone.IndexOf(":");
            if (index < 2)
                timeZone += "0" + tempTimeZone;
            else
                timeZone += tempTimeZone;

            TimeZoneInfo curseZone = null;
            foreach (TimeZoneInfo zone in TimeZoneInfo.GetSystemTimeZones())
            {
                if (!zone.DisplayName.StartsWith(timeZone))
                    continue;

                curseZone = zone;
                break;
            }

            DateTime myDate = DateTime.MinValue;
            if (DateTime.TryParse(date, out myDate) && curseZone != null)
                return TimeZoneInfo.ConvertTime(myDate, curseZone, TimeZoneInfo.Local);
            else
                return DateTime.MinValue;
        }

        public static string GetCurseForgeModURL(string curseForgeURL)
        {
            if (curseForgeURL.EndsWith("/files/latest"))
                return curseForgeURL.Replace("/files/latest", "");
            if (curseForgeURL.EndsWith("/files"))
                return curseForgeURL.Replace("/files", "");
            if (curseForgeURL.EndsWith("/images"))
                return curseForgeURL.Replace("/images", "");

            return curseForgeURL;
        }
    }

}
