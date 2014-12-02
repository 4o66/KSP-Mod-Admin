using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using KerbalStuff;


namespace KSPModAdmin.Utils
{
    public static class KerbalStuffHelper
    {
        
        /// <summary>
        /// Checks if the passed url is a KerbalStuff link (url).
        /// </summary>
        /// <param name="url">The url to check.</param>
        /// <returns>True if the passed url is a valid Spaceport link, otherwise false.</returns>
        public static bool IsValidURL(string url)
        {
            return (!string.IsNullOrEmpty(url) && (url.ToLower().StartsWith("http://kerbalstuff.com/") || url.ToLower().StartsWith("https://kerbalstuff.com/")));
        }

        /// <summary>
        /// Gets the content of the site of the passed URL and parses it for ModInfos.
        /// </summary>
        /// <param name="url">The URL of the site to parse the ModInfos from.</param>
        /// <returns>The ModInfos parsed from the site of the passed URL.</returns>
        public static ModInfo GetModInfo(string url)
        {
            ModInfo modInfo = new ModInfo();
            modInfo.KerbalStuffURL = url;
            modInfo.VersionControl = VersionControl.KerbalStuff;
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
        public static string GetDownloadURL(string produktID)
        {
            ModVersion modversion = KerbalStuffReadOnly.ModLatest(Convert.ToInt64(produktID));
            return "https://kerbalstuff.com" + modversion.DownloadPath; 
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
            string[] urlArray = downloadURL.Split("/");
            Array.Reverse(urlArray);
            string filename = urlArray[2].Replace("%20", "_") + "-" + urlArray[0] + ".zip";
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
            string[] urlArray = downloadURL.Split("/");
            Array.Reverse(urlArray);
            string filename = urlArray[2].Replace("%20", "_") + "-" + urlArray[0] + ".zip";
            modInfo.LocalPath = Path.Combine(MainForm.Instance.Options.DownloadPath, filename);

            AsyncTask<bool> asyncJob = new AsyncTask<bool>();
            asyncJob.SetDownloadCallbackFunctions(modInfo.KerbalStuffURL, modInfo.LocalPath, finished, progressChanged);
            asyncJob.RunDownload();
        }

        /// <summary>
        /// Parses the siteContent for ModInfo data.
        /// </summary>
        /// <param name="url">The KerbalStuff site content for a mod.</param>
        /// <param name="modInfo">The ModInfo to fill with the data of the site.</param>
        private static bool ParseSite(string url, ref ModInfo modInfo)
        {
            
            // get productID
            string[] urlArray = url.Split("/");
            Array.Reverse(urlArray);
            modInfo.ProductID = urlArray[1];

            Mod mod = KerbalStuffReadOnly.ModInfo(Convert.ToInt64(urlArray[1]));
            ModVersion modversion = KerbalStuffReadOnly.ModLatest(Convert.ToInt64((urlArray[1])));
            
            // get author

            modInfo.Author = mod.Author;

            // get title

            modInfo.Name = mod.Name;

            // get downloads

            modInfo.Downloads = mod.Downloads.ToString();
            modInfo.KerbalStuffURL = "https://kerbalstuff.com" + modversion.DownloadPath;
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

        public static string GetKerbalStuffModURL(string kerbalStuffURL)
        {
            if (kerbalStuffURL.EndsWith("/files/latest"))
                return kerbalStuffURL.Replace("/files/latest", "");
            if (kerbalStuffURL.EndsWith("/files"))
                return kerbalStuffURL.Replace("/files", "");
            if (kerbalStuffURL.EndsWith("/images"))
                return kerbalStuffURL.Replace("/images", "");

            return kerbalStuffURL;
        }
    }
}
