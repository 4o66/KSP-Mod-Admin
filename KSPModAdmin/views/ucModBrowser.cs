using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using KSPModAdmin.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;

namespace KSPModAdmin.Views
{
    /// <summary>
    /// Extands the WebBrowser so that we can intercept navigation of the browser (all link clicks)
    /// to handle the mod download by our own.
    /// </summary>
    public partial class ucModBrowser : ucBase
    {
        #region Members

        /// <summary>
        /// Flag to avoid AutoNavigation of the web browser.
        /// </summary>
        private bool m_DoNavigation = true;

        /// <summary>
        /// Flag to determine if a download is running or not.
        /// </summary>
        private bool m_Downloading = false;

        private AsyncTask<bool> m_AsyncJob = null;

        #endregion

        #region Properties

        /// <summary>
        /// The url of the currently displayed website.
        /// </summary>
        public string URL
        {
            get
            {
                if (webBrowser1.Url != null) 
                    return webBrowser1.Url.AbsoluteUri;
                else 
                    return string.Empty; 
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construktor for VS Designer only!
        /// </summary>
        public ucModBrowser()
        {
            InitializeComponent();

            // hook to NewWindow event to prevent popups.
            SHDocVw.WebBrowser_V1 Web_V1; //Interface to expose ActiveX methods
            Web_V1 = (SHDocVw.WebBrowser_V1)this.webBrowser1.ActiveXInstance;
            Web_V1.NewWindow += new SHDocVw.DWebBrowserEvents_NewWindowEventHandler(webBrowser_NewWindow);
        }

        #endregion

        #region Public

        /// <summary>
        /// Refreshes the ModBrowser tab.
        /// </summary>
        public void RefreshModBrowser()
        {
            tsbHome_Click(null, null);
        }

        /// <summary>
        /// Calls the Navigate methode of the WebBrowser.
        /// </summary>
        /// <param name="url">The url to navigate to.</param>
        public void Navigate(string url)
        {
            m_DoNavigation = true;

            if (!m_Downloading)
            {
                toolStripSeparator3.Visible = true;
                tsbLoading.Visible = true;
                tsProgressBar.Visible = true;
                tsProgressBar.Value = 0;
            }

            tsstbURL.Text = url;
            webBrowser1.Navigate(url);

            tsbNext.Enabled = webBrowser1.CanGoForward;
            tsbPrev.Enabled = webBrowser1.CanGoBack;

            m_DoNavigation = false;
        }

        #endregion

        #region Private

        #region Event handling

        /// <summary>
        /// Handles the Load event of the ucModBrowser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucModBrowser_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            Application.AddMessageFilter(new MouseNavigationMessageFilter(ParentForm, new EventHandler(tsbPrev_Click), new EventHandler(tsbNext_Click)));
        }

        /// <summary>
        /// Handles the click event of the tsbForum.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbForum_Click(object sender, EventArgs e)
        {
            //webBrowser1.Navigate("http://forum.kerbalspaceprogram.com/showthread.php/26031-Win-KSP-Mod-Admin-v1-3-2-Mod-install-with-2-klicks");
            webBrowser1.Navigate("http://forum.kerbalspaceprogram.com/forumdisplay.php/35-Add-on-Releases-and-Projects-Showcase");
        }

        /// <summary>
        /// Handles the click event of the tsbHome.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbHome_Click(object sender, EventArgs e)
        {
            Navigate("https://kerbalstuff.com");
        }

        /// <summary>
        /// Handles the click event of the tsbCurseForge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbCurseForge_Click(object sender, EventArgs e)
        {
            Navigate("http://kerbal.curseforge.com/projects");
        }

        private void tsbKerbalStuff_Click(object sender, EventArgs e)
        {
            Navigate("https://kerbalstuff.com");
        }

        /// <summary>
        /// Handles the click event of the tsbBestRating.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbBestRating_Click(object sender, EventArgs e)
        {
            Navigate("http://kerbalspaceport.com/?paged=1&orderby=meta_value_num&meta_key=realrate&order=DESC&s=+");
        }

        /// <summary>
        /// Handles the click event of the tsbNew.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbNew_Click(object sender, EventArgs e)
        {
            Navigate("http://kerbalspaceport.com/?orderby=date&order=DESC&s=%20");
        }

        /// <summary>
        /// Hanles the click event of the tsbRefresh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            Navigate(webBrowser1.Url.AbsoluteUri);
        }

        /// <summary>
        /// Handles the click event of the tsbPrev.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPrev_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();

            tsbNext.Enabled = webBrowser1.CanGoForward;
            tsbPrev.Enabled = webBrowser1.CanGoBack;
        }

        /// <summary>
        /// Handles the click event of the tsbNext.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbNext_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();

            tsbNext.Enabled = webBrowser1.CanGoForward;
            tsbPrev.Enabled = webBrowser1.CanGoBack;
        }

        /// <summary>
        /// Handels the Click event of the btnOpenDownloads.
        /// Opens the downloads folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbOpenDownloads_Click(object sender, EventArgs e)
        {
            string fullpath = MainForm.Options.GetValidDownloadPath();
            try
            {
                if (Directory.Exists(fullpath))
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fullpath;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                MainForm.AddError("Open download folder faild.", ex);
            }
        }

        /// <summary>
        /// Handles the click event of the tsbChooseDest.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbChooseDest_Click(object sender, EventArgs e)
        {
            MainForm.Options.GetValidDownloadPath(true);
        }

        /// <summary>
        /// Handles the click event of the tsbUpdateMod.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbUpdateModInfo_Click(object sender, EventArgs e)
        {
            MainForm.AddInfo("Downloading ModInfos ...");

            string url = webBrowser1.Url.ToString();

            ModInfo newModInfo = null;
            VersionControl versionControl = VersionControl.None;
            if (SpaceportHelper.IsValidURL(url))
            {
                newModInfo = SpaceportHelper.GetModInfo(url);
                versionControl = VersionControl.Spaceport;
            }
            else if (KSPForumHelper.IsValidURL(url))
            {
                newModInfo = KSPForumHelper.GetModInfo(url);
                versionControl = VersionControl.KSPForum;
            }
            else if (CurseForgeHelper.IsValidURL(CurseForgeHelper.GetCurseForgeModURL(url)))
            {
                newModInfo = CurseForgeHelper.GetModInfo(CurseForgeHelper.GetCurseForgeModURL(url));
                versionControl = VersionControl.CurseForge;
            }

            if (newModInfo == null || string.IsNullOrEmpty(newModInfo.ProductID))
            {
                MainForm.AddInfo("No ModInfos found.");
                return;
            }

            MainForm.AddInfo(string.Format("Searching mod \"{0}\" ...", newModInfo.Name));
            TreeNodeMod oldMod = (from mod in MainForm.ModSelection.Nodes
                                  where mod.ProductID == newModInfo.ProductID && mod.VersionControl == versionControl
                                  select mod).FirstOrDefault();
            if (oldMod == null)
            {
                MainForm.AddInfo(string.Format("Mod \"{0}\" ({1}) not found.", newModInfo.Name, newModInfo.ProductID));
                return;
            }

            MainForm.AddInfo(string.Format("Updating ModInfos of \"{0}\" ...", oldMod.Text));
            oldMod.ModInfo = newModInfo;
            MainForm.AddInfo("Done.");
        }

        /// <summary>
        /// Handles the click event of the tsbUpdateMod.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDownloadMod_Click(object sender, EventArgs e)
        {
            string url = webBrowser1.Url.ToString();
            if (string.IsNullOrEmpty(url) || (!SpaceportHelper.IsValidURL(url) && 
                                              !KSPForumHelper.IsValidURL(url) && 
                                              !CurseForgeHelper.IsValidURL(CurseForgeHelper.GetCurseForgeModURL(url))))
            {
                MessageBox.Show(this, "Sorry support only for KerbalStuff, CurseForge and KSP Forum URLs.", "");
                return;
            }

            if (SpaceportHelper.IsValidURL(url))
                MainForm.ModSelection.HandleModAddViaSpaceport(url);
            else if (KSPForumHelper.IsValidURL(url))
                MainForm.ModSelection.HandleModAddViaKSPForum(url);
            else if (CurseForgeHelper.IsValidURL(CurseForgeHelper.GetCurseForgeModURL(url)))
                MainForm.ModSelection.HandleModAddViaCurseForge(url);
        }

        /// <summary>
        /// Handles the click event of the tsbNavitageTo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbNavitageTo_Click(object sender, EventArgs e)
        {
            Navigate(tsstbURL.Text);
        }
        
        /// <summary>
        /// Callback if a NewPopup Window will be opened.
        /// Avoids New Windows and navigates to the popup url.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="flags"></param>
        /// <param name="targetFrameName"></param>
        /// <param name="postData"></param>
        /// <param name="headers"></param>
        /// <param name="processed"></param>
        private void webBrowser_NewWindow(string url, int flags, string targetFrameName, ref object postData, string headers, ref bool processed)
        {
            // Stop event from being processed
            processed = true;

            tsstbURL.Text = url;
            webBrowser1.Navigate(url);


            //// Move default WebBrowser to TabControl.
            //if (tabControl1.TabPages.Count == 1)
            //{
            //    this.Controls.Remove(webBrowser1);
            //    tabPage1.Controls.Add(webBrowser1);
            //    webBrowser1.Dock = DockStyle.Fill;
            //}

            //// Create new TabPage for new WebPage.
            //TabPage page = new TabPage();
            //tabControl1.TabPages.Add(page);
            //WebBrowserEx browser = new WebBrowserEx();
            //page.Controls.Add(browser);
            //browser.Dock = DockStyle.Fill;
            //browser.ScriptErrorsSuppressed = true;
            //browser.Navigated += new WebBrowserNavigatedEventHandler(webBrowser_Navigated);
            //browser.Navigating += new WebBrowserNavigatingEventHandler(webBrowser_Navigating);
            //browser.ProgressChanged += new WebBrowserProgressChangedEventHandler(webBrowser_ProgressChanged);
            //browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            //browser.FileDownloading += new EventHandler<FileDownloadEventArgs>(webBrowser_FileDownloading);
            //browser.Navigate(URL);
            //tabControl1.SelectedTab = page;
        }

        /// <summary>
        /// Handles the Navigating event of the webBrowser.
        /// To manage our own download of KSP mods.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebBrowserNavigatingEventArgs"/> instance containing the event data.</param>
        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.AbsoluteUri == "about:blank")
            {
                e.Cancel = true;
                return;
            }

            if (m_DoNavigation)
                return;

            if (e.Url.AbsoluteUri.StartsWith("http://kerbalspaceport.com/wp/wp-content/themes/kerbal/inc/download.php?f="))
            {
                // we handle the download
                //e.Cancel = true;
                //MainForm.AddInfo("Mod download started ...");
                //DownloadSpaceportMod(e.Url.AbsoluteUri, webBrowser1.Url.AbsoluteUri);

                e.Cancel = true;
                DownloadMod(webBrowser1.Url.ToString(), VersionControl.Spaceport);
            }
            else if (e.Url.AbsoluteUri.StartsWith("http://kerbal.curseforge.com/") &&
                     (e.Url.AbsoluteUri.EndsWith("/files/latest") || e.Url.AbsoluteUri.EndsWith("/download"))) // || e.Url.AbsoluteUri.EndsWith("/files") || e.Url.AbsoluteUri.EndsWith("/images")))
            {
                e.Cancel = true;
                DownloadMod(webBrowser1.Url.ToString(), VersionControl.CurseForge);
            }
            else if (e.Url.AbsoluteUri.StartsWith("https://kerbalstuff.com/") &&
         (e.Url.AbsoluteUri.Contains("/download/"))) // || e.Url.AbsoluteUri.EndsWith("/files") || e.Url.AbsoluteUri.EndsWith("/images")))
            {
                e.Cancel = true;
                DownloadMod(webBrowser1.Url.ToString(), VersionControl.KerbalStuff);
            }
            else
            {
                MainForm.AddInfo("Loading website ...");
                toolStripSeparator3.Visible = true;
                tsbLoading.Visible = true;
                tsProgressBar.Visible = true;
            }
        }

        /// <summary>
        /// Handles the Navigated event of the webBrowser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            tsbNext.Enabled = browser.CanGoForward;
            tsbPrev.Enabled = browser.CanGoBack;
        }

        /// <summary>
        /// Handles the ProgressChanged event of the webBrowser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (m_Downloading) return; // display the download progress not the siteloading!
            
            tsProgressBar.Maximum = (int)e.MaximumProgress;
            //if (tsProgressBar.Minimum <= (int)e.CurrentProgress && tsProgressBar.Maximum <= (int)e.CurrentProgress)
            if (e.CurrentProgress != -1 && (int)e.CurrentProgress <= tsProgressBar.Maximum)    
                tsProgressBar.Value = (int)e.CurrentProgress;
        }

        /// <summary>
        /// Handles the DocumentCompleted event of the webBrowser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!m_Downloading)
            {
                toolStripSeparator3.Visible = false;
                tsProgressBar.Visible = false;
                tsbLoading.Visible = false;
            }

            if (webBrowser1.Document != null)
            {
                tsstbURL.Text = webBrowser1.Document.Url.ToString();
                webBrowser1.Document.Click += htmlDoc_Click;
                webBrowser1.Document.ContextMenuShowing += htmlDoc_ContextMenuShowing;
            }
        }

        /// <summary>
        /// Handles the DocumentCompleted event of the webBrowser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser_FileDownloading(object sender, FileDownloadEventArgs e)
        {
            DownloadFile(e.DownloadUri.ToString());
        }

        /// <summary>
        /// Handles the ContextMenuShowing event of the htmlDoc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void htmlDoc_ContextMenuShowing(object sender, HtmlElementEventArgs e)
        {
            //e.ReturnValue = false;
        }

        /// <summary>
        /// Handles the Click event of the htmlDoc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void htmlDoc_Click(object sender, HtmlElementEventArgs e)
        {
            if (e.MouseButtonsPressed == System.Windows.Forms.MouseButtons.XButton1 && webBrowser1.CanGoBack)
                webBrowser1.GoBack();
            else if (e.MouseButtonsPressed == System.Windows.Forms.MouseButtons.XButton2 && webBrowser1.CanGoForward)
                webBrowser1.GoForward();

            //e.ReturnValue = false;
        }

        #region old code

        //private void tsbHome_Click1(object sender, EventArgs e)
        //{
        //    m_DoNavigation = true;

        //    HttpWebRequest webReq2 = (HttpWebRequest)WebRequest.Create("http://kerbalspaceport.com/wp/wp-content/themes/kerbal/css/reset.css");
        //    HttpWebResponse webResp2 = (HttpWebResponse)webReq2.GetResponse();
        //    Stream Answer2 = webResp2.GetResponseStream();
        //    StreamReader _Answer2 = new StreamReader(Answer2);
        //    string css = _Answer2.ReadToEnd() + Environment.NewLine;


        //    HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create("http://kerbalspaceport.com/?paged=1&orderby=meta_value_num&meta_key=realrate&order=DESC&s=+");
        //    HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();

        //    //string status = webResp.StatusCode + Environment.NewLine;
        //    //string server = webResp.Server + Environment.NewLine;

        //    Stream Answer = webResp.GetResponseStream();
        //    StreamReader _Answer = new StreamReader(Answer);

        //    //string docText = File.ReadAllText(@"C:\Users\bheinrich\Desktop\SP.html");

        //    string docText = _Answer.ReadToEnd() + Environment.NewLine;
        //    webBrowser1.DocumentText = bla(docText);

        //    // Best rating
        //    //webBrowser1.Url = new Uri("http://kerbalspaceport.com/?paged=1&orderby=meta_value_num&meta_key=realrate&order=DESC&s=+");
        //    // newest
        //    //webBrowser1.Url = new Uri("http://kerbalspaceport.com/?orderby=date&order=DESC&s=%20");

        //    m_DoNavigation = false;
        //}

        //private string bla(string docText)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    int index1 = docText.IndexOf("<title>");
        //    string temp = docText.Substring(0, index1);
        //    sb.Append(temp);
        //    docText = docText.Substring(index1);

        //    sb.AppendLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"http://kerbalspaceport.com/wp/wp-content/themes/kerbal/css/superfish.css\" media=\"screen\">");
        //    sb.AppendLine("<link rel=\"shortcut icon\" href=\"http://kerbalspaceport.com/wp/wp-content/themes/kerbal/favicon.ico\">");
        //    sb.AppendLine("<script type='text/javascript' src='http://kerbalspaceport.com/wp/wp-includes/js/jquery/jquery.js?ver=1.8.3'></script>");
        //    sb.AppendLine("<script type='text/javascript' src='http://kerbalspaceport.com/wp/wp-content/themes/kerbal/js/hoverIntent.js?ver=3.5.2'></script>");
        //    sb.AppendLine("<script type='text/javascript' src='http://kerbalspaceport.com/wp/wp-content/themes/kerbal/js/superfish.js?ver=3.5.2'></script>");
        //    sb.AppendLine("<script type='text/javascript' src='http://kerbalspaceport.com/wp/wp-content/themes/kerbal/js/general_config.js?ver=3.5.2'></script>");
        //    sb.AppendLine("<script type='text/javascript' src='http://kerbalspaceport.com/wp/wp-content/themes/kerbal/js/search_main.js?ver=3.5.2'></script>");
        //    sb.AppendLine("<script type='text/javascript' src='http://kerbalspaceport.com/wp/wp-content/themes/kerbal/js/jquery.urldecoder.js?ver=3.5.2'></script>");
        //    sb.AppendLine("<script type='text/javascript' src='http://kerbalspaceport.com/wp/wp-content/themes/kerbal/js/search_page.js?ver=3.5.2'></script>");
        //    sb.AppendLine("<script type='text/javascript' src='http://kerbalspaceport.com/wp/wp-content/themes/kerbal/js/order_search.js?ver=3.5.2'></script>");
        //    sb.AppendLine("<link rel=\"EditURI\" type=\"application/rsd+xml\" title=\"RSD\" href=\"http://kerbalspaceport.com/wp/xmlrpc.php?rsd\" />");
        //    sb.AppendLine("<link rel=\"wlwmanifest\" type=\"application/wlwmanifest+xml\" href=\"http://kerbalspaceport.com/wp/wp-includes/wlwmanifest.xml\" /> ");

        //    temp = docText.Substring(0, index1 + 7);
        //    sb.Append(temp);
        //    sb.Append("KSP Space Port");

        //    //skip original title
        //    docText = docText.Substring(index1);
        //    index1 = docText.IndexOf("<");
        //    docText = docText.Substring(index1);

        //    index1 = docText.IndexOf("<link");
        //    temp = docText.Substring(0, index1);
        //    sb.Append(temp);

        //    //skip ksp css
        //    index1 = docText.IndexOf("<meta");
        //    docText = docText.Substring(index1);

        //    index1 = docText.IndexOf("</head>");
        //    temp = docText.Substring(0, index1);
        //    sb.Append(temp);
        //    docText = docText.Substring(index1);

        //    // add our own css
        //    sb.AppendLine(Constants.MAIN_CSS);

        //    sb.AppendLine("</head>");
        //    sb.AppendLine("<body style=\"background:#888\">");
        //    sb.AppendLine("<div>");

        //    temp = docText;
        //    int index = docText.IndexOf("<div class=\"search_item pod\"");
        //    while (index != -1)
        //    {
        //        temp = temp.Substring(index);
        //        int endIndex = temp.IndexOf("<div class=\"search_item pod\"");
        //        if (endIndex <= 0)
        //            endIndex = temp.IndexOf("<div class=\"clear\">");

        //        string entry = temp.Substring(0, endIndex);//.Replace("search_item pod", "search_item");

        //        int i = temp.IndexOf("\r\n");
        //        sb.AppendLine(entry.Substring(0, i));

        //        sb.AppendLine("<table border=\"1\">");
        //        sb.AppendLine("<tr>");
        //        sb.AppendLine("<td style=\"background:#ffd\">");

        //        sb.Append(entry.Substring(i, entry.IndexOf("<div class=\"tooltip_loader\"></div>")));

        //        sb.AppendLine("</td>");
        //        sb.AppendLine("</tr>");
        //        sb.AppendLine("</table>");
        //        sb.AppendLine("</div><!-- search_item -->");

        //        temp = temp.Substring(temp.IndexOf("<div class=\"tooltip_loader\"></div>"));
        //        endIndex = temp.IndexOf("<div class=\"search_item pod\"");
        //        if (endIndex == -1)
        //            endIndex = temp.IndexOf("<div class=\"clear\">");
        //        temp = temp.Substring(endIndex);

        //        index = temp.IndexOf("<div class=\"search_item pod\"");
        //    }

        //    sb.AppendLine("</div>");

        //    index1 = docText.IndexOf("<div class=\"pagination-search\">");
        //    docText = docText.Substring(index1);

        //    int index2 = docText.IndexOf("</div><!-- End body CONTENT -->");
        //    temp = docText.Substring(0, index2);
        //    // add site numbers 

        //    string c2 = "<span class='page-numbers current'>";
        //    temp = temp.Substring(temp.IndexOf(c2) + c2.Length);
        //    string cSite = temp.Substring(0, temp.IndexOf("</"));

        //    temp = temp.Substring(temp.LastIndexOf("<a class='page-numbers'"));
        //    temp = temp.Substring(temp.IndexOf(">") + 1);
        //    string maxSite = temp.Substring(0, temp.IndexOf("<"));

        //    tslSiteCount.Text = cSite + "/" + maxSite;

        //    string c1 = "</div><!-- End body FOOTER -->";
        //    sb.AppendLine(docText.Substring(docText.IndexOf(c1) + c1.Length));

        //    return sb.ToString();
        //}




        ///// <summary>
        ///// Handles the Navigating event of the webBrowser.
        ///// To manage our own download of KSP mods.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void webBrowser_Navigating1(object sender, WebBrowserNavigatingEventArgs e)
        //{
        //    if (!m_DoNavigation)
        //    {
        //        Uri url = e.Url;
        //        if (e.Url.AbsoluteUri.StartsWith("http://kerbalspaceport.com/wp/wp-content/themes/kerbal/inc/download.php?f="))
        //        {
        //            // we handle the download
        //            e.Cancel = true;

        //            // get download
        //            HttpWebRequest webReq2 = (HttpWebRequest)WebRequest.Create(e.Url);
        //            HttpWebResponse webResp2 = (HttpWebResponse)webReq2.GetResponse();
        //            Stream Answer2 = webResp2.GetResponseStream();
        //            StreamReader _Answer2 = new StreamReader(Answer2);

        //            // get save fullpath 
        //            int start = e.Url.AbsoluteUri.LastIndexOf("/") + 1;
        //            string filename = e.Url.AbsoluteUri.Substring(start, e.Url.AbsoluteUri.Length - start);
        //            string fullpath = Path.Combine(@"C:\Users\bheinrich\Desktop", filename);

        //            // write file to save folder.
        //            Stream file = File.OpenWrite(fullpath);
        //            byte[] buffer = new byte[8 * 1024];
        //            int len;
        //            while ((len = Answer2.Read(buffer, 0, buffer.Length)) > 0)
        //                file.Write(buffer, 0, len);

        //            file.Close();
        //        }
        //    }

        //    tsbNext.Enabled = webBrowser1.CanGoForward;
        //    tsbPrev.Enabled = webBrowser1.CanGoBack;
        //}

        #endregion

        #endregion

        private void DownloadMod(string url, VersionControl versionControl)
        {
            MainForm.AddInfo("Mod download started ...");
            tsbLoading.Visible = true;
            tsProgressBar.Visible = true;
            new AsyncTask<TreeNodeMod>(
                () =>
                {
                    TreeNodeMod newMod = null;
                    switch (versionControl)
                    {
                        case VersionControl.Spaceport:
                            newMod = MainForm.Instance.ModSelection.HandleModAddViaSpaceport(
                                                    url,
                                                    string.Empty,
                                                    false,
                                                    (o, args) =>
                                                    {
                                                        tsProgressBar.Maximum = (int)args.TotalBytesToReceive;
                                                        tsProgressBar.Value = (int)args.BytesReceived;
                                                    });
                            break;
                        case VersionControl.CurseForge:
                            newMod = MainForm.Instance.ModSelection.HandleModAddViaCurseForge(
                                                    url,
                                                    string.Empty,
                                                    false,
                                                    (o, args) =>
                                                    {
                                                        tsProgressBar.Maximum = (int)args.TotalBytesToReceive;
                                                        tsProgressBar.Value = (int)args.BytesReceived;
                                                    });
                            break;
                        case VersionControl.KerbalStuff:
                            newMod = MainForm.Instance.ModSelection.HandleModAddViaKerbalStuff(
                                                    url,
                                                    string.Empty,
                                                    false,
                                                    (o, args) =>
                                                    {
                                                        tsProgressBar.Maximum = (int)args.TotalBytesToReceive;
                                                        tsProgressBar.Value = (int)args.BytesReceived;
                                                    });
                            break;
                    }
                    return newMod;
                },
                (newMod, ex) =>
                {
                    if (ex != null)
                    {
                        MainForm.AddError("Mod download failed!", ex);
                    }
                    else
                    {
                        MainForm.AddInfo("Done!");
                        tsbLoading.Visible = false;
                        tsProgressBar.Visible = false;
                    }
                }).Run();
        }

        /// <summary>
        /// Downloads the Mod from the given url and adds it to the Mods TreeView.
        /// </summary>
        /// <param name="url">The url of the file to download.</param>
        /// <param name="spacePortURL"></param>
        private void DownloadSpaceportMod(string url, string spacePortURL)
        {
            if (m_Downloading)
            {
                MessageBox.Show(this, "Just one download at a time!", "", MessageBoxButtons.OK);
                return;
            }

            // get a download path.
            MainForm.Options.GetValidDownloadPath();
            if (string.IsNullOrEmpty(MainForm.Options.DownloadPath))
                return;

            // get save path 
            int start = url.LastIndexOf("/") + 1;
            string filename = url.Substring(start, url.Length - start);
            string fullpath = Path.Combine(MainForm.Options.DownloadPath, filename);

            toolStripSeparator3.Visible = true;
            tsbLoading.Visible = true;
            tsProgressBar.Visible = true;
            tsProgressBar.Value = 0;
            tsProgressBar.Maximum = 100;

            m_Downloading = true;

            // start async download.
            m_AsyncJob = new AsyncTask<bool>();
            m_AsyncJob.SetDownloadCallbackFunctions(url, fullpath, delegate(bool result, Exception ex)
            {
                if (ex != null)
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    DownloadSucceeded(new ModInfo(fullpath, spacePortURL));

                tsProgressBar.Visible = false;
                tsbLoading.Visible = false;
                toolStripSeparator3.Visible = false;

                m_Downloading = false;
            },
            delegate(int value)
            {
                tsProgressBar.Value = value;
            });

            m_AsyncJob.RunDownload();
        }

        /// <summary>
        /// Handles the DownloadSucceeded event of the ucModBrowser.
        /// </summary>
        /// <param name="modInfo">ModInfo of the downloaded mod.</param>
        private void DownloadSucceeded(ModInfo modInfo)
        {
            if (string.IsNullOrEmpty(modInfo.LocalPath))
                return;

            if (!String.IsNullOrEmpty(modInfo.SpaceportURL))
                FillModInfo(modInfo);
            else
                FillModInfoFinished(modInfo);
        }

        /// <summary>
        /// Files the Properties of the SpaceportMod (Connects to KSP Spaceport to get Mod infos).
        /// </summary>
        /// <param name="modInfo">The modInfo to fill (with valid spaceport URL!)</param>
        private void FillModInfo(ModInfo modInfo)
        {
            toolStripSeparator3.Visible = true;
            tsbLoading.Visible = true;
            
            m_Downloading = true;

            // start async download.
            AsyncTask<ModInfo>.DoWork(delegate()
                                      {
                                          ModInfo newModInfo = null;
                                          if (SpaceportHelper.IsValidURL(modInfo.SpaceportURL))
                                              newModInfo = SpaceportHelper.GetModInfo(modInfo.SpaceportURL);
                                          else if (KSPForumHelper.IsValidURL(modInfo.ForumURL))
                                              newModInfo = KSPForumHelper.GetModInfo(modInfo.ForumURL);
                                          newModInfo.LocalPath = modInfo.LocalPath;
                                          return newModInfo;
                                      },
                                      delegate(ModInfo result, Exception ex)
                                      {
                                          if (ex != null)
                                              MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                          else
                                              FillModInfoFinished(result);
                
                                          tsbLoading.Visible = false;
                                          toolStripSeparator3.Visible = false;
                
                                          m_Downloading = false;
                                      });
        }

        /// <summary>
        /// Called after all Mod infos where read from the spaceport site.
        /// (Adds the mod to the modselection)
        /// </summary>
        /// <param name="modInfo"></param>
        private void FillModInfoFinished(ModInfo modInfo)
        {
            if (modInfo == null)
                return;

            MainForm.AddInfo("Download done.");
            if (modInfo.IsArchive)
                MainForm.ModSelection.AddModsAsync(new ModInfo[] { modInfo });

            else if (modInfo.IsCraft)
            {
                MainForm.AddInfo("Creating Ziparchive for craft file");
                modInfo.LocalPath = MainForm.ModSelection.CreateZipOfCraftFile(modInfo.LocalPath);
                if (!string.IsNullOrEmpty(modInfo.LocalPath))
                {
                    MainForm.AddInfo(string.Format("Craft zipped to \"{0}\"", modInfo.LocalPath));
                    MainForm.ModSelection.AddModsAsync(new ModInfo[] {modInfo});
                }
            }

            else
            {
                MainForm.AddInfo("The downloaded file format is not supported!");
                MessageBox.Show(this,
                    "The downloaded file format is not supported. \"" + Path.GetFileName(modInfo.LocalPath) + "\"",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Downloads the Mod from the given url and adds it to the Mods TreeView.
        /// </summary>
        /// <param name="url">The url of the file to download.</param>
        private void DownloadFile(string url)
        {
            if (m_Downloading)
            {
                MessageBox.Show(this, "Just one download at a time!", "", MessageBoxButtons.OK);
                return;
            }

            // get a doanload path.
            MainForm.Options.GetValidDownloadPath();
            if (string.IsNullOrEmpty(MainForm.Options.DownloadPath))
                return;

            toolStripSeparator3.Visible = true;
            tsbLoading.Visible = true;
            tsProgressBar.Visible = true;
            tsProgressBar.Value = 0;
            tsProgressBar.Maximum = 100;

            m_Downloading = true;

            string fullpath = Path.Combine(MainForm.Options.DownloadPath, "kspma_download.xxx");

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            // start async download.
            m_AsyncJob = new AsyncTask<bool>();
            m_AsyncJob.SetDownloadCallbackFunctions(url, fullpath, delegate(bool result, Exception ex)
            {
                if (ex != null)
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    int start = -1;
                    string fileName = string.Empty;
                    string contentDisposition = m_AsyncJob.WebClient.ResponseHeaders["content-disposition"];
                    if (!string.IsNullOrEmpty(contentDisposition))
                    {
                        string lookFor = "filename=\"";
                        int index = contentDisposition.IndexOf(lookFor, StringComparison.CurrentCultureIgnoreCase);
                        if (index >= 0)
                        {
                            fileName = contentDisposition.Substring(index + lookFor.Length);
                            index = fileName.IndexOf("\"");
                            if (index > -1)
                                fileName = fileName.Substring(0, index);

                            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                            {
                                MainForm.AddError(string.Format("Invalid filename! \"{0}\"", fileName));
                                fileName = string.Empty;
                            }
                            else
                                fileName = Path.Combine(MainForm.Options.DownloadPath, fileName);
                        }
                    }
                    else if ((start = url.ToLower().IndexOf(Constants.EXT_7ZIP)) > -1)
                    {
                        start += 3;
                        fileName = url.Substring(0, start);
                        fileName = Path.Combine(MainForm.Options.DownloadPath, url.Substring(fileName.LastIndexOf("/") + 1));
                    }
                    else if ((start = url.ToLower().IndexOf(Constants.EXT_ZIP)) > -1)
                    {
                        start += 4;
                        fileName = url.Substring(0, start);
                        fileName = Path.Combine(MainForm.Options.DownloadPath, url.Substring(fileName.LastIndexOf("/") + 1));
                    }
                    else if ((start = url.ToLower().IndexOf(Constants.EXT_RAR)) > -1)
                    {
                        start += 4;
                        fileName = url.Substring(0, start);
                        fileName = Path.Combine(MainForm.Options.DownloadPath, url.Substring(fileName.LastIndexOf("/") + 1));
                    }
                    else if ((start = url.ToLower().IndexOf(Constants.EXT_CRAFT)) > -1)
                    {
                        start += 6;
                        fileName = url.Substring(0, start);
                        fileName = Path.Combine(MainForm.Options.DownloadPath, url.Substring(fileName.LastIndexOf("/") + 1));
                    }

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        if (File.Exists(fileName))
                            File.Delete(fileName);

                        if (!File.Exists(fullpath))
                            MainForm.AddInfo(string.Format("Downloaded file not found! \"{0}\"", fullpath));
                        else
                            File.Move(fullpath, fileName);

                        DownloadFileSucceded(fileName);
                    }
                }

                tsProgressBar.Visible = false;
                tsbLoading.Visible = false;
                toolStripSeparator3.Visible = false;

                m_Downloading = false;
            },
            delegate(int value)
            {
                tsProgressBar.Value = value;
            });

            m_AsyncJob.RunDownload();
        }

        /// <summary>
        /// Handles the DownloadSucceded event of the ucModBrowser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="fullpath">the path of the Zip-file.</param>
        private void DownloadFileSucceded(string fullpath)
        {
            if (string.IsNullOrEmpty(fullpath))
                return;

            if (fullpath.ToLower().EndsWith(Constants.EXT_ZIP) ||
                fullpath.ToLower().EndsWith(Constants.EXT_7ZIP) ||
                fullpath.ToLower().EndsWith(Constants.EXT_RAR))
                MainForm.ModSelection.AddModsAsync(new string[] { fullpath });

            else if (fullpath.ToLower().EndsWith(Constants.EXT_CRAFT))
            {
                fullpath = MainForm.ModSelection.CreateZipOfCraftFile(fullpath);
                if (!string.IsNullOrEmpty(fullpath))
                    MainForm.ModSelection.AddModsAsync(new string[] { fullpath });
            }

            else
                MessageBox.Show(this, "The downloaded file format is not supported. \"" + Path.GetFileName(fullpath) + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
        
        /// <summary>
        /// MessageFilter class that handles the mouse extra buttons.
        /// </summary>
        public class MouseNavigationMessageFilter : IMessageFilter
        {
            const int WM_XBUTTONDOWN = 0x020B;
            const int MK_XBUTTON1 = 65568;
            const int MK_XBUTTON2 = 131136;

            private Form m_ParentForm;
            private EventHandler m_Backevent;
            private EventHandler m_Forwardevent;

            /// <summary>
            /// Initializes a new instance of the <see cref="MouseNavigationMessageFilter"/> class.
            /// </summary>
            /// <param name="form">The form.</param>
            /// <param name="backevent">The backevent.</param>
            /// <param name="forwardevent">The forwardevent.</param>
            public MouseNavigationMessageFilter(Form form, EventHandler backevent, EventHandler forwardevent)
            {
                m_ParentForm = form;
                m_Backevent = backevent;
                m_Forwardevent = forwardevent;
            }

            /// <summary>
            /// Filters out a message before it is dispatched.
            /// </summary>
            /// <param name="msg">The message to be dispatched. You cannot modify this message.</param>
            /// <returns>
            /// true to filter the message and stop it from being dispatched; false to allow the message to continue to the next filter or control.
            /// </returns>
            public bool PreFilterMessage(ref Message msg)
            {
                bool handled = false;
                if (msg.Msg == WM_XBUTTONDOWN)
                {
                    int w = msg.WParam.ToInt32();
                    if (w == MK_XBUTTON1)
                    {
                        m_Backevent.Invoke(m_ParentForm, EventArgs.Empty);
                        handled = true;
                    }
                    else if (w == MK_XBUTTON2)
                    {
                        m_Forwardevent.Invoke(m_ParentForm, EventArgs.Empty);
                        handled = true;
                    }
                }
                return handled;
            }
        }

        private void tsstbURL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Navigate(tsstbURL.Text);
        }
    }
}
