using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KSPModAdmin.Utils;

namespace KSPModAdmin.Views
{
    public partial class frmShowConflicts : Form
    {
        private TreeNodeMod m_CollisionMod = null;


        public TreeNodeMod CollisionMod
        {
            get
            {
                return m_CollisionMod;
            }
            set
            {
                m_CollisionMod = value;

                if (m_CollisionMod == null) 
                    return;


                DataTable dt = new DataTable("CollisionMod");
                var zipRoot = m_CollisionMod.ZipRoot;
                if (!dt.Columns.Contains(zipRoot.Text))
                    dt.Columns.Add(zipRoot.Text);

                Dictionary<string, CollistionData> data = new Dictionary<string, CollistionData>();
                foreach (var collNode in ModRegister.GetAllCollisionNodes(m_CollisionMod))
                {
                    var row = dt.NewRow();
                    row[zipRoot.Text] = string.Format("{0} ({1})", collNode.Text, collNode.FullPath);
                    dt.Rows.Add(row);

                    if (!ModRegister.RegisterdModFiles.ContainsKey(collNode.Destination.ToLower()))
                        continue;

                    if (!data.ContainsKey(zipRoot.Text.ToLower()))
                        data.Add(zipRoot.Text.ToLower(), new CollistionData(zipRoot.Text.ToLower(), collNode));

                    foreach (var collFile in ModRegister.RegisterdModFiles[collNode.Destination.ToLower()])
                    {
                        if (!dt.Columns.Contains(collFile.ZipRoot.Text))
                            dt.Columns.Add(collFile.ZipRoot.Text);

                        if (!data.ContainsKey(collFile.ZipRoot.Text.ToLower()) && collFile != collNode)
                            data.Add(collFile.ZipRoot.Text.ToLower(), new CollistionData(zipRoot.Text.ToLower(), collFile));
                        else if (collFile != collNode)
                            if (data[collFile.ZipRoot.Text.ToLower()].Collisions.ContainsKey(collFile.Destination.ToLower()))
                                data[collFile.ZipRoot.Text.ToLower()].Collisions[collFile.Destination.ToLower()].Add(collFile);
                            else
                                data[collFile.ZipRoot.Text.ToLower()].Collisions.Add(collFile.Destination.ToLower(), new List<TreeNodeMod>(){ collFile });
                        
                        row = dt.Rows[dt.Rows.Count - 1];
                        row[collFile.ZipRoot.Text] = string.Format("{0} ({1})", collFile.Text, collFile.FullPath);
                    }
                }

                if (InvokeRequired)
                    Invoke((MethodInvoker)delegate() { dgvConflicts.DataSource = dt; });
                else
                {
                    dgvConflicts.DataSource = dt;
                }
            }
        }


        public frmShowConflicts()
        {
            InitializeComponent();
        }

        class CollistionData
        {
            public string ModName { get; set; }

            public Dictionary<string, List<TreeNodeMod>> Collisions { get; set; }


            public CollistionData()
            {
                ModName = string.Empty;
                Collisions = new Dictionary<string, List<TreeNodeMod>>();
            }

            public CollistionData(string modName, TreeNodeMod collisionFile)
            {
                ModName = modName;
                Collisions = new Dictionary<string, List<TreeNodeMod>>();
                Collisions.Add(collisionFile.Destination.ToLower(), new List<TreeNodeMod>() { collisionFile });
            }
        }
    }
}
