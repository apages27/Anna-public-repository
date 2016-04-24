using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AlphaAutoSetup
{
    public class MachinePowerSchemeSetter
    {
        [DllImport("PowrProf.dll")]
        public static extern uint PowerSetActiveScheme(IntPtr UserRootPowerKey, ref Guid SchemeGuid);

        public void SetMonitorTimeout()
        {
            var arguments = string.Format("/c powercfg -setacvalueindex 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c 7516b95f-f776-4464-8c53-06167f40cc99 3c0bc021-c8a8-4e07-a973-6b14cbcb2b7e 0");

            var commandPromptRunner = new Process();

            commandPromptRunner.StartInfo = new ProcessStartInfo("cmd.exe", arguments);
            commandPromptRunner.StartInfo.UseShellExecute = false;

            commandPromptRunner.Start();
            commandPromptRunner.WaitForExit(1000);
        }
    }
}
