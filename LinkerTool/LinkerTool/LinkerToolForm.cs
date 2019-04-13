using System;
using System.Windows.Forms;

namespace LinkerTool
{
    public partial class LinkerTool : Form
    {
        public LinkMaster LinkMaster { get; set; }

        public string NewLocation { get; set; }

        public string OriginalItem { get; set; }

        public LinkerTool()
        {
            InitializeComponent();

            LinkMaster = new LinkMaster();
        }

        private void linkButton_Click(object sender, EventArgs e)
        {
            var result = LinkMaster.CreateLink(OriginalItem, NewLocation);

            resultText.Text = result;
        }

        private void newItemLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            NewLocation = newItemLocationTextBox.Text;
        }

        private void NewLocationFolderBrowserButton_Click(object sender, EventArgs e)
        {
            var result = newLocationFolderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                newItemLocationTextBox.Text = newLocationFolderBrowserDialog.SelectedPath;
            }
        }

        private void OriginalItemFolderBrowserButton_Click(object sender, EventArgs e)
        {
            var result = originalItemFolderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                originalItemTextBox.Text = originalItemFolderBrowserDialog.SelectedPath;
            }
        }

        private void originalItemTextBox_TextChanged(object sender, EventArgs e)
        {
            OriginalItem = originalItemTextBox.Text;
        }
    }
}