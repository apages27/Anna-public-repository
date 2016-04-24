using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AlphaAutoSetup
{
    public class TightVNCInstaller
    {
        private static string _TightVNCInstallerPath;

        public void SetUpTightVNC()
        {
            try
            {
                DownloadTightVNC();

                InstallTightVNC(_TightVNCInstallerPath);

                Console.WriteLine("TightVNC installed!");
            }
            catch (Exception)
            {
                Console.WriteLine("There was a problem installing TightVNC.");
                throw;
            }
        }

        private static void DownloadTightVNC()
        {
            var client = new WebClient();
            _TightVNCInstallerPath = @"C:\Users\" + Environment.UserName + @"\Downloads\tightVNCsetup.msi";

            if (!File.Exists(_TightVNCInstallerPath)) client.DownloadFile("http://www.tightvnc.com/download/2.7.10/tightvnc-2.7.10-setup-64bit.msi", _TightVNCInstallerPath);
        }

        private static void InstallTightVNC(string TightVNCInstallerPath)
        {
            var TightVNCArguments =
                    string.Format(@"/i " + TightVNCInstallerPath +
                                  @" /quiet /norestart ADDLOCAL=Server SET_USEVNCAUTHENTICATION=1 VALUE_OF_USEVNCAUTHENTICATION=1 SET_PASSWORD=1 VALUE_OF_PASSWORD=cinemassive");

            var installProcess = new Process();

            installProcess.StartInfo = new ProcessStartInfo("msiexec.exe", TightVNCArguments);
            installProcess.StartInfo.UseShellExecute = false;

            installProcess.Start();
            installProcess.WaitForExit();
        }
    }
}
