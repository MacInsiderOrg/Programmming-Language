using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ProgrammingLanguage
{
    static class Program
    {
        static Mutex mutex = new Mutex(false, "jMutex");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!mutex.WaitOne(TimeSpan.FromSeconds(2), false))
            {
                // Another application instance is running
                return;
            }
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var targetApplication = Process.GetCurrentProcess().ProcessName + ".exe";
                int ie_emulation = 11000;

                SetIeVersioneKeyforWebBrowserControl(targetApplication, ie_emulation);

                Application.Run(new Form1());
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        private static void SetIeVersioneKeyforWebBrowserControl(string appName, int ieval)
        {
            RegistryKey Regkey = null;
            try
            {
                Regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);

                // If the path is not correct or if user't have priviledges to access registry 
                if (Regkey == null)
                {
                    return;
                }

                string FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                // Check if key is already present 
                if (FindAppkey == ieval.ToString())
                {
                    Regkey.Close();
                    return;
                }

                // If key is not present or different from desired, add/modify the key , key value 
                Regkey.SetValue(appName, unchecked((int)ieval), RegistryValueKind.DWord);

                // Check for the key after adding 
                FindAppkey = Convert.ToString(Regkey.GetValue(appName));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Application FEATURE_BROWSER_EMULATION setting failed; " + ex.Message);
            }
            finally
            {
                // Close the Registry 
                if (Regkey != null)
                    Regkey.Close();
            }
        }
    }
}