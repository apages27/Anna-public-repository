using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;

namespace AlphaAutoSetup
{
    public class UtilityServiceRunnerInstaller
    {
        public void SetUpUtilityServiceRunner()
        {
            var utilityServiceRunnerDirectory = @"C:\Users\" + Environment.UserName + @"\Desktop\Utility Service Runner";

            try
            {
                ExtractUtilityServiceRunner(utilityServiceRunnerDirectory);

                AddUtilityServiceRunnerToStartup(utilityServiceRunnerDirectory);

                Process.Start(utilityServiceRunnerDirectory + "/UtilityServiceRunner.exe");

                Console.WriteLine("Utility Service Runner has been installed and started!");
            }
            catch (Exception)
            {
                Console.WriteLine("There was a problem installing or running Utility Service Runner.");
                throw;
            }
        }

        private static void AddUtilityServiceRunnerToStartup(string utilityServiceRunnerDirectory)
        {
            var wsh = new IWshShell_Class();

            var utilityServiceRunnerShortcut = wsh.CreateShortcut("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\Launch Utility Service Runner.lnk") as IWshShortcut;
            utilityServiceRunnerShortcut.TargetPath = Path.Combine(utilityServiceRunnerDirectory, "UtilityServiceRunner.exe");
            utilityServiceRunnerShortcut.WorkingDirectory = utilityServiceRunnerDirectory;
            utilityServiceRunnerShortcut.Save();
        }

        private static void ExtractUtilityServiceRunner(string utilityServiceRunnerDirectory)
        {
            if (Directory.Exists(utilityServiceRunnerDirectory)) return;

            ZipFile.ExtractToDirectory("ZippedFiles/UtilityServiceRunner.zip", utilityServiceRunnerDirectory);
        }
    }
}
