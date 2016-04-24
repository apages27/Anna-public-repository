using System.IO;
using System.Security.AccessControl;

namespace CineNetAutoDeployer
{
    public class PermissionsMistress
    {
        private CineNetAutoDeployerForm _cineNetAutoDeployerForm;

        public PermissionsMistress(CineNetAutoDeployerForm cineNetAutoDeployerForm)
        {
            _cineNetAutoDeployerForm = cineNetAutoDeployerForm;
        }

        public void SetNasPermissions()
        {
            SetPermissionsForChosenFile(_cineNetAutoDeployerForm.PathMaster.SystemManagementInstallerPath);

            SetPermissionsForChosenFile(_cineNetAutoDeployerForm.PathMaster.AlphaControlServiceInstallerPath);
        }

        private void SetPermissionsForChosenFile(string fileName)
        {
            var directoryInfo = new DirectoryInfo(InstallOverlord.MainBranchInstallersLocation + CineNetAutoDeployerForm.BuildNumber);
            var directorySecurity = directoryInfo.GetAccessControl();

            directorySecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit |
                                                                                                               InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

            directoryInfo.SetAccessControl(directorySecurity);

            var installerFile = new FileInfo(fileName);
            var fileSecurity = installerFile.GetAccessControl();

            fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));

            installerFile.SetAccessControl(fileSecurity);
        }
    }
}