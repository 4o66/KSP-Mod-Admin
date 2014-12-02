using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Web;

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
            if (ParseSite(url, ref modInfo))
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

            string searchString = "<a class=\"overflow-tip\" href=\"";
            int i1 = siteContent.IndexOf(searchString) + searchString.Length;
            if (i1 <= searchString.Length) return string.Empty;
            int i2 = siteContent.IndexOf("</a>", i1);
            if (i2 < 0) return string.Empty;
            siteContent = siteContent.Substring(i1, i2 - i1);
            int index = siteContent.IndexOf("\">") + 2;
            string fileName = siteContent.Substring(index);
            if (fileName.Contains("<span title=\""))
            {
                i1 = fileName.IndexOf("<span title=\"") + 13;
                if (i1 < 0) return string.Empty;
                i2 = fileName.IndexOf("\"", i1);
                if (i2 < 0) return string.Empty;
                fileName = fileName.Substring(i1, i2 - i1);
            }
            return HttpUtility.HtmlDecode(fileName.Trim());
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
        private static bool ParseSite(string url, ref ModInfo modInfo)
        {
            HtmlWeb web = new HtmlWeb();

            modInfo.ProductID = string.Empty;

            HtmlDocument htmlDoc = web.Load(url);

            htmlDoc.OptionFixNestedTags = true;

            HtmlNode nameNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='project-details-container']/div[@class='project-user']/h1[@class='project-title']/a/span");
            HtmlNode idNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='sidebar-actions']/ul/li[@class='view-on-curse']/a");
            HtmlNode createNode = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='cf-details project-details']/li/div[starts-with(., 'Last Released')]/following::div[1]/abbr");
            HtmlNode downloadNode = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='cf-details project-details']/li/div[starts-with(., 'Total Downloads')]/following::div[1]");

            if (nameNode != null)
            {
                modInfo.Name = nameNode.InnerHtml;
                modInfo.ProductID = idNode.Attributes["href"].Value;
                modInfo.ProductID = modInfo.ProductID.Substring(modInfo.ProductID.LastIndexOf("/") + 1);
                modInfo.CreationDate = createNode.InnerHtml;
                modInfo.Downloads = downloadNode.InnerHtml;
            }

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
