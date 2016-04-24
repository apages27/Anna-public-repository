using System;
using System.Windows.Forms;

namespace CineNetAutoDeployer
{
    public partial class CineNetAutoDeployerForm : Form
    {
        public string UserApplicationChoice { get; set; }
        public static string BuildNumber { get; set; }
        public static string ServerName { get; set; }

        public ConfigurationCommander ConfigurationCommander
        {
            get { return _configurationCommander; }
        }

        public ServiceSultan ServiceSultan
        {
            get { return _serviceSultan; }
        }

        public PathMaster PathMaster
        {
            get { return _pathMaster; }
        }

        public InstallOverlord InstallOverlord
        {
            get { return _installOverlord; }
        }

        public bool FreshInstall;
        private readonly ConfigurationCommander _configurationCommander;
        private readonly ServiceSultan _serviceSultan;
        private readonly PathMaster _pathMaster;
        private readonly InstallOverlord _installOverlord;

        public CineNetAutoDeployerForm()
        {
            InitializeComponent();
            _configurationCommander = new ConfigurationCommander(this);
            _serviceSultan = new ServiceSultan();
            _pathMaster = new PathMaster();
            _installOverlord = new InstallOverlord(this);
        }

        private void deployButton_Click(object sender, EventArgs e)
        {
            UserApplicationChoice = applicationChoiceBox.SelectedItem.ToString();

            BuildNumber = buildNumberTextBox.Text;

            ServerName = serverTextBox.Text.ToLower();

            _pathMaster.SetApplicationPaths();

            if (freshInstallCheckBox.Checked)
            {
                FreshInstall = true;

                if (ActiveForm != null) ActiveForm.Visible = false;

                _configurationCommander.GetConfigValuesForFreshInstall(ServerName);

                Cursor = Cursors.WaitCursor;

                _installOverlord.InstallChosenApplications();

                _configurationCommander.ApplySettingsToConfigFiles();
            }
            else
            {
                FreshInstall = false;

                Cursor = Cursors.WaitCursor;

                _configurationCommander.SaveExistingConfigSettings(ServerName);

                _configurationCommander.SaveExistingLicense();

                _installOverlord.InstallChosenApplications();

                _configurationCommander.ApplySettingsToConfigFiles();
            }

            //_serviceSultan.StartServices();

            Application.Exit();
        }
    }
}
