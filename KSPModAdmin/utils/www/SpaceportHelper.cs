using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace KSPModAdmin.Utils
{
    public static class SpaceportHelper
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
            return (!string.IsNullOrEmpty(url) && (url.ToLower().StartsWith("http://kerbalspaceport.com/") || url.ToLower().StartsWith("http://www.kerbalspaceport.com/")));
        }

        /// <summary>
        /// Gets the content of the site of the passed URL and parses it for ModInfos.
        /// </summary>
        /// <param name="url">The URL of the site to parse the ModInfos from.</param>
        /// <returns>The ModInfos parsed from the site of the passed URL.</returns>
        public static ModInfo GetModInfo(string url)
        {
            ModInfo modInfo = new ModInfo();
            modInfo.SpaceportURL = url;
            modInfo.VersionControl = VersionControl.Spaceport;
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
        public static string GetDownloadURL(string produktID)
        {
            return wwwHelper.Load(DownloadFormUrl, new Dictionary<string, string> { { "addonid", produktID }, { "action", "downloadfileaddon" } });
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

            string downloadURL = GetDownloadURL(modInfo.ProductID);

            // get save path 
            int start = downloadURL.LastIndexOf("/") + 1;
            string filename = downloadURL.Substring(start, downloadURL.Length - start);
            modInfo.LocalPath = Path.Combine(MainForm.Instance.Options.DownloadPath, filename);

            wwwHelper.DownloadFile(downloadURL, modInfo.LocalPath, downloadProgressHandler);

            return File.Exists(modInfo.LocalPath);
        }

        /// <summary>
        /// Starts a async download of a mod from KSP Spaceport.
        /// </summary>
        /// <param name="modInfo"></param>
        /// <param name="finished"></param>
        /// <param name="progressChanged"></param>
        public static void DownloadModAsync(ref ModInfo modInfo, AsyncResultHandler<bool> finished = null, AsyncProgressChangedHandler progressChanged = null)
        {
            string downloadURL = GetDownloadURL(modInfo.ProductID);

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
            // get creation date
            int index = siteContent.IndexOf("Created:");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + 8);
            index = siteContent.IndexOf("</li>");
            if (index < 0) return false;
            string creationDate = siteContent.Substring(0, index).Trim();
            DateTime dtCreationDate = DateTime.Now;
            modInfo.CreationDate = (DateTime.TryParse(creationDate, out dtCreationDate)) ? dtCreationDate.ToString() : DateTime.Now.ToString();

            // get rating count
            index = siteContent.IndexOf("Average Rating");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index);
            index = siteContent.IndexOf("(");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + 1);
            index = siteContent.IndexOf(")");
            if (index < 0) return false;
            string ratingCount = siteContent.Substring(0, index).Trim();

            // get rating
            index = siteContent.IndexOf("<span class=\"rating");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index);
            index = siteContent.IndexOf("</li>");
            if (index < 0) return false;
            string ratingHTML = siteContent.Substring(0, index).Trim();
            modInfo.Rating = string.Format("{0} ({1})", GetRatingFromHTML(ratingHTML), ratingCount);

            // get downloads
            index = siteContent.IndexOf("Downloads:");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + 10);
            index = siteContent.IndexOf("</li>");
            if (index < 0) return false;
            modInfo.Downloads = siteContent.Substring(0, index).Trim();

            // get author
            index = siteContent.IndexOf("Posts by");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + 8);
            index = siteContent.IndexOf("\"");
            if (index < 0) return false;
            modInfo.Author = siteContent.Substring(0, index).Trim();

            // get productID
            index = siteContent.IndexOf("Product ID:");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + 11);
            index = siteContent.IndexOf("</li>");
            if (index < 0) return false;
            modInfo.ProductID = siteContent.Substring(0, index).Trim();

            // get title
            index = siteContent.IndexOf("<h1>");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + 4);
            index = siteContent.IndexOf("</h1>");
            if (index < 0) return false;
            modInfo.Name = siteContent.Substring(0, index).Trim();

            // more infos could be parsed here (like: short description, Tab content (overview, installation, ...), comments, ...)
            return true;
        }

        /// <summary>
        /// Parses the Rating of the mod from the HTML rating spann.
        /// </summary>
        /// <param name="ratingHTML">The HTML rating spann.</param>
        /// <returns>Value between 0 and 5.</returns>
        private static int GetRatingFromHTML(string ratingHTML)
        {
            #region expected content of ratingHTML
            //<span class="rating " id="12988">
            //  <span class="full star-1">
            //    <img src="http://kerbalspaceport.com/wp/wp-content/themes/kerbal/images/spacer.gif"  width="16" height="16" />
            //  </span>
            //  <span class="full star-2">
            //    <img src="http://kerbalspaceport.com/wp/wp-content/themes/kerbal/images/spacer.gif"  width="16" height="16" />
            //  </span>
            //  <span class="full star-3">
            //    <img src="http://kerbalspaceport.com/wp/wp-content/themes/kerbal/images/spacer.gif"  width="16" height="16" />
            //  </span>
            //  <span class="full star-4">
            //    <img src="http://kerbalspaceport.com/wp/wp-content/themes/kerbal/images/spacer.gif"  width="16" height="16" />
            //  </span>
            //  <span class="empty star-5">
            //    <img src="http://kerbalspaceport.com/wp/wp-content/themes/kerbal/images/spacer.gif"  width="16" height="16" />
            //  </span>
            //</span>                        
            //</span>
            #endregion
            int count = 0;
            while (ratingHTML.Contains("full star-"))
            {
                ratingHTML = ratingHTML.Substring(ratingHTML.IndexOf("full star-") + 10);
                ++count;
            }
            return count;
        }
    }
}
