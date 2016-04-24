using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AlphaAutoSetup
{
    public class SharedDirectoryCreator
    {
        public void CreateSharedTestingDirectory()
        {
            var testingDirectoryPath = @"C:\Testing";
            
            Directory.CreateDirectory(testingDirectoryPath);

            var directoryInfo = new DirectoryInfo(testingDirectoryPath);
            var directorySecurity = directoryInfo.GetAccessControl();

            directorySecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | 
                InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

            directoryInfo.SetAccessControl(directorySecurity);


            var manager = new ManagementClass("win32_share");
            var inParameters = manager.GetMethodParameters("Create");
            inParameters["Description"] = "Shared Testing Folder";
            inParameters["Name"] = "Testing";
            inParameters["Path"] = testingDirectoryPath;
            inParameters["Type"] = 0x0;
            inParameters["MaximumAllowed"] = null;
            inParameters["Password"] = null;
            inParameters["Access"] = null;

            var outParameters = manager.InvokeMethod("Create", inParameters, null);

            var ntAccount = new NTAccount("Everyone");

            var userSID = (SecurityIdentifier)ntAccount.Translate(typeof(SecurityIdentifier));
            var userSIDArray = new byte[userSID.BinaryLength];

            userSID.GetBinaryForm(userSIDArray, 0);

            ManagementObject userTrustee = new ManagementClass(new ManagementPath("Win32_Trustee"), null);
            userTrustee["Name"] = "Everyone";
            userTrustee["SID"] = userSIDArray;

            ManagementObject userACE = new ManagementClass(new ManagementPath("Win32_Ace"), null);
            userACE["AccessMask"] = 2032127; //Full access
            userACE["AceFlags"] = AceFlags.ObjectInherit | AceFlags.ContainerInherit;
            userACE["AceType"] = AceType.AccessAllowed;
            userACE["Trustee"] = userTrustee;

            ManagementObject userSecurityDescriptor = new ManagementClass(new ManagementPath("Win32_SecurityDescriptor"), null);
            userSecurityDescriptor["ControlFlags"] = 4; //SE_DACL_PRESENT 
            userSecurityDescriptor["DACL"] = new object[] { userACE };

            var share = new ManagementObject(manager.Path + ".Name='" + inParameters["Name"] + "'");

            share.InvokeMethod("SetShareInfo", new[] { Int32.MaxValue, inParameters["Description"], userSecurityDescriptor });
        }
    }
}
