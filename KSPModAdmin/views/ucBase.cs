using System.Windows.Forms;

namespace KSPModAdmin.Views
{
    public partial class ucBase : UserControl
    {
        private MainForm m_MainForm = null;

        public MainForm MainForm { get { return m_MainForm; } set { m_MainForm = value; } }

        /// <summary>
        /// Construktor for VS Designer only!
        /// </summary>
        public ucBase()
        {
            InitializeComponent();
        }

        public void InvokeIfRequired(MethodInvoker action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
    }
}
