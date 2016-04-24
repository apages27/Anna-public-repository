using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CineNetAutoDeployer
{
    public partial class ConfigValueForm : Form
    {
        private string ServerName { get; set; }
        public ConfigValueSet ConfigValues { get; set; }

        public ConfigValueForm(string serverName)
        {
            InitializeComponent();

            ServerName = serverName;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            ConfigValues = new ConfigValueSet
            {
                InputSystemType = inputSystemTypeComboBox.SelectedItem.ToString(),
                OutputSystemType = outputSystemTypeComboBox.SelectedItem.ToString(),
                VerticalPanels = verticalPanelsTextBox.Text.Trim(),
                HorizontalPanels = horizontalPanelsTextBox.Text.Trim(),
                AlphaControlServiceUrl = ServerName,
                SystemManagementUrl = ServerName,
                License = GetLicenseForAlpha(ServerName)
            };

            Application.Exit();
        }

        private string GetLicenseForAlpha(string serverName)
        {
            switch (serverName)
            {
                case "enterprise":
                    return "3G2oaVQ7XtjRI9HRYs0Y+jG6rikzRhivboqP7j7nk4szHzphFikwsIpVaY4M2PIGoApsiC/eZuk4CB0dUwu47grMex454XMVsTUUiA4Ae3nYyumR7NZaUA==";
                case "tigersclaw":
                    return "H6V0vcBXnJ6LrIxVyarUkCYj/RncPUMwl+REKNRi+OQ70wh0UHWmpsdEgNohlhRxHxFjXsDvd6f/ZBNFag6N6ulv9EbMTs1ll4/If82iFCQldD4JsIgm5qA4/yIBIwc7";
                case "normandy":
                    return "YcJ6d+b8Y2ZmbipbldTNnODqKTaODeB2TQO5qHRZV9HmcTTL2P11hzqQdc74h6nrffkaZG95CuNkdhxvnJBBtRg6dbYCTFkg";
                case "galactica":
                    return "Jftw/FG0AXq99LMymQhyl9gyw47YfYgNDULzr9SdQUkTb2whOX0sy4mnjTCeHXvcFTJhSQjmdtHox0DKRrlQeukWrcpr7zWrMRAfr+nwv1U=";
                case "executor":
                    return "ZjpAWsRlOxz/8nqC0Y4iDb3nTaWVdfOGVoNlpj0lteL/RiJwlJWZRrJtkkP8LwDVUS63efqC9cp4jO80no9K2Vqmr1wfNAKb0CHGgnkx59S5Syjy3aVkBg==";
                case "jupiterii":
                    return "CZ7U24eRX+5jIZlafZ1fMUDz2fQvFRmVCAA1ppDqdh8WLRPO0SG76TVp+CphCa19ltMH/As5FtgReQnU+Xof0XVrl5XA/5cwev62LzzAfho=";
            }
            return "NONE";
        }
    }
}
