using System.Diagnostics;
using System.IO;

namespace LinkerTool
{
    public class LinkMaster
    {
        public string NewLocation { get; set; }
        public string OriginalItem { get; set; }

        public string CreateLink(string originalItem, string newLocation)
        {
            OriginalItem = originalItem;

            NewLocation = newLocation;

            string resultText;

            try
            {
                CopyOriginalItemToNewLocation();

                CreateLinkInOriginalLocation();

                resultText = "Link created successfully!";
            }
            catch (System.Exception exception)
            {
                resultText = $"Link failed. {exception.Message}";
            }

            return resultText;
        }

        private void CopyOriginalItemToNewLocation()
        {
            if (!Directory.Exists(OriginalItem)) return;

            var startInfo = new ProcessStartInfo
                            {
                                    CreateNoWindow = true,
                                    UseShellExecute = false,
                                    FileName = "xcopy",
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    Arguments = OriginalItem + " " + NewLocation + " /s /e /h /y /i /k"
                            };

            var process = Process.Start(startInfo);

            process?.WaitForExit();

            Directory.Delete(OriginalItem, true);
        }

        private void CreateLinkInOriginalLocation()
        {
            var startInfo = new ProcessStartInfo
                            {
                                    CreateNoWindow = true,
                                    UseShellExecute = false,
                                    FileName = "cmd.exe",
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    Arguments = "/c mklink /D " + OriginalItem + " " + NewLocation
                            };

            var process = Process.Start(startInfo);

            process?.WaitForExit();
        }
    }
}