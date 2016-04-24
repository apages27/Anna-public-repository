using System;
using System.Management;
using System.Threading;

namespace CineNetAutoDeployer
{
    public class ServiceSultan
    {
        public void StopRunningServices(string processName)
        {
            var connection = new ConnectionOptions
            {
                Impersonation = ImpersonationLevel.Impersonate,
                EnablePrivileges = true,
                Username = "localadmin",
                Password = "cinemassive",
                Authentication = AuthenticationLevel.Packet
            };

            var path = new ManagementPath("\\\\" + CineNetAutoDeployerForm.ServerName + "\\root\\cimv2");

            var scope = new ManagementScope(path, connection);

            var objectQuery = new ObjectQuery("Select * From Win32_Process Where Name = \"" + processName + "\"");

            var objectSearcher = new ManagementObjectSearcher(scope, objectQuery);

            foreach (ManagementObject managementObject in objectSearcher.Get())

                if (string.Equals(managementObject["Name"].ToString(), processName, StringComparison.CurrentCultureIgnoreCase))
                {
                    var outParams = managementObject.InvokeMethod("Terminate",
                        new object[] { 0 });

                    Thread.Sleep(5000);
                }
        }

        public void StartServices()
        {
            var connection = new ConnectionOptions
            {
                Impersonation = ImpersonationLevel.Impersonate,
                EnablePrivileges = true,
                Username = "localadmin",
                Password = "cinemassive",
                Authentication = AuthenticationLevel.Packet
            };


            var scope = new ManagementScope("\\\\" + CineNetAutoDeployerForm.ServerName + "\\root\\CIMV2", connection);

            try
            {
                scope.Connect();
            }
            catch (Exception exception)
            {
                Thread.Sleep(90000);

                scope.Connect();
            }

            var path = new ManagementPath("Win32_Process");
            var classInstance = new ManagementClass(scope, path, null);

            var inParams = classInstance.GetMethodParameters("Create");

            inParams["CommandLine"] =
                @"C:\Program Files (x86)\CineMassive\CineNet Alpha Control Service\cinenetstartup.bat";
            inParams["CurrentDirectory"] = @"C:\Program Files (x86)\CineMassive\CineNet Alpha Control Service";

            var outParams = classInstance.InvokeMethod("Create", inParams, null);
            var retVal = outParams["ReturnValue"].ToString();
        }
    }
}