namespace CineNetAutoDeployer
{
    public class PathMaster
    {
        public string AlphaControlServiceApplicationPath;
        public string SystemManagerApplicationPath;
        public string SystemManagementInstallerPath;
        public string AlphaControlServiceInstallerPath;

        public void SetApplicationPaths()
        {
            SetApplicationInstallerPaths();

            SetApplicationExecutablePaths();
        }

        private void SetApplicationInstallerPaths()
        {
            SystemManagementInstallerPath = InstallOverlord.MainBranchInstallersLocation + CineNetAutoDeployerForm.BuildNumber +
                                             @"\Install CineNet Live System Manager.msi";

            AlphaControlServiceInstallerPath = InstallOverlord.MainBranchInstallersLocation + CineNetAutoDeployerForm.BuildNumber +
                                                @"\Install CineNet Live Alpha Control Service.msi";
        }

        private void SetApplicationExecutablePaths()
        {
            AlphaControlServiceApplicationPath = @"\\" + CineNetAutoDeployerForm.ServerName + @"\C$\Program Files (x86)\CineMassive\CineNet Alpha Control Service\AlphaControlService.exe";
            SystemManagerApplicationPath = @"\\" + CineNetAutoDeployerForm.ServerName + @"\C$\Program Files (x86)\CineMassive\CineNet System Manager\CineNetSystemManagement.exe";
        }
    }
}