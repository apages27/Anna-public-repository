using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace CineNetAutoDeployer
{
    public class ConfigurationCommander
    {
        private static Configuration _configuration;
        private AppSettingsSection _appSettings;
        private ConfigValueSet _retrievedConfigValues;
        private static string ServerName { get; set; }

        private CineNetAutoDeployerForm _cineNetAutoDeployerForm;
        private string _systemManagementDirectory;
        private string _remoteTempDirectory;

        public ConfigurationCommander(CineNetAutoDeployerForm cineNetAutoDeployerForm)
        {
            _cineNetAutoDeployerForm = cineNetAutoDeployerForm;
        }

        public void GetConfigValuesForFreshInstall(string serverName)
        {
            ServerName = serverName;

            var configValues = new ConfigValueForm(ServerName);
            configValues.ShowDialog();

            Application.EnableVisualStyles();

            _retrievedConfigValues = new ConfigValueSet();
            _retrievedConfigValues = configValues.ConfigValues;
        }

        public void SaveExistingConfigSettings(string serverName)
        {
            ServerName = serverName;

            _remoteTempDirectory = GetRemoteTempDirectory();

            GetAppSettings(GetConfigurationFileLocation("AlphaControlService"));

            CopySettingsFromConfigToTextFile("CineNetAlphaControlServiceTempConfigSettingsRepo.txt");

            GetAppSettings(GetConfigurationFileLocation("CineNetSystemManagement"));

            CopySettingsFromConfigToTextFile("CineNetSystemManagementTempConfigSettingsRepo.txt");
        }

        private string GetRemoteTempDirectory()
        {
            var tempDirectoryPath = "\\\\" + ServerName + "\\Testing\\Temp";

            Directory.CreateDirectory(tempDirectoryPath);

            return tempDirectoryPath;
        }

        private void GetAppSettings(string exePath)
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = exePath + ".config" };

            _configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            _appSettings = _configuration.AppSettings;
        }

        private string GetConfigurationFileLocation(string applicationName)
        {
            var exePath = applicationName == "CineNetSystemManagement" ? _cineNetAutoDeployerForm.PathMaster.SystemManagerApplicationPath : _cineNetAutoDeployerForm.PathMaster.AlphaControlServiceApplicationPath;

            return exePath;
        }

        private void CopySettingsFromConfigToTextFile(string tempFileName)
        {
            using (var writer = new StreamWriter(Path.Combine(_remoteTempDirectory, tempFileName)))
                foreach (var key in _configuration.AppSettings.Settings.AllKeys)
                {
                    writer.WriteLine(key);
                    writer.WriteLine(_configuration.AppSettings.Settings[key].Value);
                }
        }

        public void SaveExistingLicense()
        {
            _systemManagementDirectory = @"\\" + ServerName +
                                         @"\C$\Program Files (x86)\CineMassive\CineNet System Manager";

            if (File.Exists(_systemManagementDirectory + "\\license"))
            {
                var tempLicenseFile = Path.Combine(_remoteTempDirectory, "CineNetTempLicenseRepo.txt");

                using (var writer = new StreamWriter(tempLicenseFile))
                {
                    writer.WriteLine(!string.IsNullOrWhiteSpace(File.ReadAllText(_systemManagementDirectory + "\\license"))
                        ? File.ReadAllLines(_systemManagementDirectory + "\\license")[0]
                        : "NONE");
                }
            }
        }

        public void ApplySettingsToConfigFiles()
        {
            ApplySettingsToSystemManagementConfig();

            ApplySettingsToAlphaControlServiceConfig();
        }

        private void ApplySettingsToSystemManagementConfig()
        {
            GetAppSettings(GetConfigurationFileLocation("CineNetSystemManagement"));

            if (_cineNetAutoDeployerForm.FreshInstall)
            {
                _appSettings.Settings["SystemManagementUrl"].Value = _retrievedConfigValues.SystemManagementUrl;

                ApplyLicenseKey();
            }
            else
            {
                var tempConfigSettingsFile = Path.Combine(_remoteTempDirectory, "CineNetSystemManagementTempConfigSettingsRepo.txt");

                var settingsArray = File.ReadAllLines(tempConfigSettingsFile);

                CopySettingsFromTextFileToConfig(settingsArray);

                ApplyLicenseKey();
            }

            SaveSettings();
        }

        private void ApplySettingsToAlphaControlServiceConfig()
        {
            GetAppSettings(GetConfigurationFileLocation("AlphaControlService"));

            if (_cineNetAutoDeployerForm.FreshInstall)
            {
                _appSettings.Settings["SystemManagementUrl"].Value = _retrievedConfigValues.SystemManagementUrl;
                _appSettings.Settings["WallServiceUrl"].Value = _retrievedConfigValues.AlphaControlServiceUrl;
                _appSettings.Settings["InputSystemType"].Value = _retrievedConfigValues.InputSystemType;
                _appSettings.Settings["OutputSystemType"].Value = _retrievedConfigValues.OutputSystemType;
                _appSettings.Settings["VerticalPanels"].Value = _retrievedConfigValues.VerticalPanels;
                _appSettings.Settings["HorizonitalPanels"].Value = _retrievedConfigValues.HorizontalPanels;
            }
            else
            {
                var tempConfigSettingsFile = Path.Combine(_remoteTempDirectory, "CineNetAlphaControlServiceTempConfigSettingsRepo.txt");

                var settingsArray = File.ReadAllLines(tempConfigSettingsFile);

                CopySettingsFromTextFileToConfig(settingsArray);
            }

            SaveSettings();
        }

        private void CopySettingsFromTextFileToConfig(string[] settingsArray)
        {
            foreach (var key in _configuration.AppSettings.Settings.AllKeys)
                for (var entry = 0; entry < settingsArray.Length; entry++)
                {
                    var valuePosition = entry + 1;

                    if (settingsArray[entry] == key && !string.IsNullOrWhiteSpace(settingsArray[valuePosition])) _configuration.AppSettings.Settings[key].Value = settingsArray[valuePosition];
                    entry++;
                }
        }

        private void ApplyLicenseKey()
        {
            if (_cineNetAutoDeployerForm.FreshInstall) ApplyNewLicenseKey();
            else RestoreSavedLicenseKey();
        }

        private void ApplyNewLicenseKey()
        {
            using (var writer = new StreamWriter(@"\\" + ServerName + @"\C$\Program Files (x86)\CineMassive\CineNet System Manager\license"))
            {
                writer.WriteLine(_retrievedConfigValues.License);
            }
        }

        private void RestoreSavedLicenseKey()
        {
            var tempLicenseFile = Path.Combine(_remoteTempDirectory, "CineNetTempLicenseRepo.txt");

            if (File.Exists(tempLicenseFile))
            {
                using (var writer = new StreamWriter(_systemManagementDirectory + "\\license"))
                {
                    writer.WriteLine(File.ReadAllLines(tempLicenseFile)[0]);
                }
            }
            else
            {
                using (var writer = new StreamWriter(_systemManagementDirectory + "\\license"))
                {
                    writer.WriteLine("NONE");
                }
            }
        }

        private void SaveSettings()
        {
            _configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}