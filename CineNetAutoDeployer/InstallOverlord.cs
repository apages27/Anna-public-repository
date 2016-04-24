using System.Management;

namespace CineNetAutoDeployer
{
    public class InstallOverlord
    {
        private CineNetAutoDeployerForm _cineNetAutoDeployerForm;
        private readonly PermissionsMistress _permissionsMistress;
        public static string MainBranchInstallersLocation = @"\\cinenas\ProductDev\Deploy\CineNet\CineNet Main\";

        public InstallOverlord(CineNetAutoDeployerForm cineNetAutoDeployerForm)
        {
            _permissionsMistress = new PermissionsMistress(cineNetAutoDeployerForm);
            _cineNetAutoDeployerForm = cineNetAutoDeployerForm;
        }

        public PermissionsMistress PermissionsMistress
        {
            get { return _permissionsMistress; }
        }

        public void InstallChosenApplications()
        {
            switch (_cineNetAutoDeployerForm.UserApplicationChoice)
            {
                case "System Management and Alpha Control Service":
                    InstallAlphaControlServiceAndSystemManagement();
                    break;

                case "CineNet Client":
                    //InstallClient();
                    break;

                case "Services and Client":
                    InstallAlphaControlServiceAndSystemManagement();
                    //InstallClient();
                    break;
            }
        }

        private void InstallClient()
        {
            var clientInstallerPath = MainBranchInstallersLocation + CineNetAutoDeployerForm.BuildNumber + @"\Install CineNet Live Client.msi";

            var clientApplicationPath = @"\\" + CineNetAutoDeployerForm.ServerName + @"\Program Files (x86)\CineMassive\CineNet Client\CineNet.exe";

            InstallApplication(clientInstallerPath);

            _cineNetAutoDeployerForm.ServiceSultan.StartServices();
        }

        private void InstallAlphaControlServiceAndSystemManagement()
        {
            _cineNetAutoDeployerForm.ServiceSultan.StopRunningServices("CineNetSystemManagement.exe");

            _cineNetAutoDeployerForm.ServiceSultan.StopRunningServices("AlphaControlService.exe");

            _cineNetAutoDeployerForm.ServiceSultan.StopRunningServices("CineNet_ViewServer.exe");

            _permissionsMistress.SetNasPermissions();

            InstallApplication(_cineNetAutoDeployerForm.PathMaster.SystemManagementInstallerPath);

            InstallApplication(_cineNetAutoDeployerForm.PathMaster.AlphaControlServiceInstallerPath);
        }

        private static void InstallApplication(string installerPath)
        {
            var connection = new ConnectionOptions
            {
                Username = "localadmin",
                Password = "cinemassive",
                Authentication = AuthenticationLevel.Packet
            };

            var scope = new ManagementScope("\\\\" + CineNetAutoDeployerForm.ServerName + "\\root\\CIMV2", connection);
            scope.Connect();

            var path = new ManagementPath("Win32_Product");
            var classInstance = new ManagementClass(scope, path, null);

            var inParams = classInstance.GetMethodParameters("Install");
            inParams["AllUsers"] = true;
            inParams["Options"] = string.Empty;
            inParams["PackageLocation"] = installerPath;

            var outParams = classInstance.InvokeMethod("Install", inParams, null);
            var retVal = outParams["ReturnValue"].ToString();
        }
    }
}