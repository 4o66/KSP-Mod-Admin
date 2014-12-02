using System;
using System.Threading;
using System.Windows.Forms;
using KSPModAdmin.Utils;

namespace KSPModAdmin
{
    static class Program
    {
        static Mutex mMutex = new Mutex(true, "{4802959C-27DD-4A9C-8CD2-36A239346FFE}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if !DEBUG
            try
            {
#endif
#if !MONOBUILD
                // check if there is already a running instance of KSP MA.
                if(mMutex.WaitOne(TimeSpan.Zero, true))
                {
#endif
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
#if !MONOBUILD
                mMutex.ReleaseMutex();
                }
                else
                {
                    // send message to make the currently running instance
                    // jump on top of all the other windows
                    NativeMethods.PostMessage(
                        (IntPtr)NativeMethods.HWND_BROADCAST,
                        NativeMethods.WM_SHOWME,
                        IntPtr.Zero,
                        IntPtr.Zero);
                }
#endif
#if !DEBUG
            }
            catch (Exception e)
            {
                string msg = "Unexpected error." + Environment.NewLine + Environment.NewLine;
                msg += "If you want to help." + Environment.NewLine;
                msg += "Press ctrl & c to copy this text and send it to macKerbal@mactee.de" + Environment.NewLine;
                msg += "Thank you!" + Environment.NewLine + Environment.NewLine;
                msg += "Message: " + e.Message + Environment.NewLine;
                msg += "Stacktrace:" + Environment.NewLine;
                msg += e.StackTrace;
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif
        }
    }
}
