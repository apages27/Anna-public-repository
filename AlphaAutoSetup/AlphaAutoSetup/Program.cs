using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace AlphaAutoSetup
{
    class Program
    {
        private static MachinePowerSchemeSetter _schemeSetter = new MachinePowerSchemeSetter();
        private static FirewallDisabler _disabler = new FirewallDisabler();
        private static SharedDirectoryCreator _directoryCreator = new SharedDirectoryCreator();
        private static AlphaFXDriverConfigurator configurator = new AlphaFXDriverConfigurator();
        private static TightVNCInstaller _TightVNCInstaller = new TightVNCInstaller();
        private static UtilityServiceRunnerInstaller _utilityServiceRunnerInstaller = new UtilityServiceRunnerInstaller();
        private static Guid _highPerformancePowerSchemeGuid = Guid.Parse("8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");
        private static bool _isAlphaFx;

        static void Main(string[] args)
        {
            //GetDriverTypeFromUser();

            MachinePowerSchemeSetter.PowerSetActiveScheme(IntPtr.Zero, ref _highPerformancePowerSchemeGuid);

            _schemeSetter.SetMonitorTimeout();

            _TightVNCInstaller.SetUpTightVNC();

            _utilityServiceRunnerInstaller.SetUpUtilityServiceRunner();

            _disabler.SetFirewallStatus(_disabler.GetCurrentPolicy(), false);

            _directoryCreator.CreateSharedTestingDirectory();
        }

        private static void GetDriverTypeFromUser()
        {
            Console.Write("Is this an AlphaFX? Y/N ");

            _isAlphaFx = Console.ReadLine().ToUpper() == "Y";

            if (!_isAlphaFx) return;

            configurator.ConfigureAlphaFXDriver();
        }

       
    }
}
