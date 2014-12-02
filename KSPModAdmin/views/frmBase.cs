using System.Windows.Forms;

namespace KSPModAdmin.Views
{
    public class frmBase : Form
    {
        /// <summary>
        /// Invokes the passed function if required.
        /// </summary>
        /// <param name="action"></param>
        public void InvokeIfRequired(MethodInvoker action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
    }
}
