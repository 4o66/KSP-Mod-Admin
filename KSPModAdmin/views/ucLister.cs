using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KSPModAdmin.Views
{
    public partial class ucLister : ucBase
    {
        #region Members

        /// <summary>
        /// Flag to abort the Lister reading.
        /// </summary>
        bool mAbort = false;

        #endregion

        #region Constructors

        public ucLister()
        {
            InitializeComponent();
        }

        #endregion

        #region Private

        #region Event handling

        private void btnUpdateList_Click(object sender, EventArgs e)
        {
            if (btnListerRefresh.Text == Constants.TXT_BTN_UPDATE_LIST_UPDATE)
                UpdateList();
            else
                mAbort = true;
        }

        #endregion

        private void UpdateList()
        {
            if (cBoxLists.SelectedItem == null)
            {
                MessageBox.Show(this, Constants.MESSAGE_SELECT_LIST);
                return;
            }

            switch (cBoxLists.SelectedItem.ToString().ToLower())
            {
                case Constants.PARTS:
                    HandleParts();
                    break;
            }
        }

        private void HandleParts()
        {
            dgvList.DataSource = null;

            btnListerRefresh.Text = Constants.TXT_BTN_UPDATE_LIST_ABORT;
            picLoading.Visible = true;
            mAbort = false;

            AsyncTask<DataTable> task = new AsyncTask<DataTable>();
            task.SetCallbackFunctions(
                                delegate()
                                {
                                    DataTable dtable = new DataTable();
                                    DataColumn dc = dtable.Columns.Add("Folder");
                                    dc = dtable.Columns.Add("Path");
                                    dc = dtable.Columns.Add("MOD");
                                    dc = dtable.Columns.Add("name");
                                    dc = dtable.Columns.Add("category");

                                    List<string> dirs = Directory.EnumerateDirectories(MainForm.GetPath(KSP_Paths.Parts)).ToList<string>();
                                    int nb = dirs.Count;
                                    int count = 0;
                                    foreach (string dir in dirs)
                                    {
                                        ReadPartInfos(dtable, dir);
                                        ++count;
                                        task.PercentFinished = (int)(((decimal)count / (decimal)nb) * (decimal)100);

                                        if (mAbort)
                                            break;
                                    }

                                    return dtable;
                                },
                                delegate(DataTable result, Exception ex)
                                {
                                    dgvList.DataSource = result;
                                    picLoading.Visible = false;
                                    btnListerRefresh.Text = Constants.TXT_BTN_UPDATE_LIST_UPDATE;
                                },
                                delegate(int percentFinished)
                                {
                                }, true);
            task.Run();
        }

        private void ReadPartInfos(DataTable dt, string dir)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (string file in Directory.EnumerateFiles(dir, "*" + Constants.EXT_CFG))
            {
                if (file.EndsWith(Constants.EXT_CFG))
                {
                    bool ignore = false;
                    foreach (string line in File.ReadLines(file))
                    {
                        string tLine = line.Trim();
                        if (ignore)
                        {
                            continue;
                        }
                        else if (tLine.StartsWith("{"))
                        {
                            ignore = true;
                            continue;
                        }
                        else if (tLine.StartsWith("}"))
                        {
                            ignore = false;
                            continue;
                        }
                        else if (!tLine.StartsWith("//") && tLine.Contains("="))
                        {
                            AddParameter(dt, parameters, tLine);
                        }
                    }
                    AddRowToGrid(dt, parameters);
                    break;
                }
            }
        }

        private void AddRowToGrid(DataTable dt, Dictionary<string, string> parameters)
        {
            if (parameters.Count > 0)
            {
                DataRow row = dt.NewRow();

                foreach (KeyValuePair<string, string> pair in parameters)
                    row[pair.Key] = pair.Value.ToString();

                dt.Rows.Add(row);
            }
        }

        private void AddParameter(DataTable dt, Dictionary<string, string> parameters, string line)
        {
            KeyValuePair<string, string> pair = new KeyValuePair<string, string>();

            string[] tempPair = line.Split('=');
            if (tempPair.Count<string>() == 2)
                pair = new KeyValuePair<string, string>(tempPair[0].Trim(), tempPair[1].Trim());
            //else
            //    AddMessage("Parameter count mismatch!");

            if (pair.Key == string.Empty)
                return;

            if (!dt.Columns.Contains(pair.Key))
                dt.Columns.Add(pair.Key);

            if (!parameters.ContainsKey(pair.Key))
                parameters.Add(pair.Key, pair.Value);
            //else
            //    AddMessage("Multiple parameter found!");
        }

        private void SetListerEnableStates(bool enable)
        {
            dgvList.Enabled = enable;
            cBoxLists.Enabled = enable;
            btnListerRefresh.Enabled = enable;
        }

        #endregion
    }
}
