namespace CineNetAutoDeployer
{
    partial class CineNetAutoDeployerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.applicationChoiceLabel = new System.Windows.Forms.Label();
            this.buildNumberLabel = new System.Windows.Forms.Label();
            this.serverLabel = new System.Windows.Forms.Label();
            this.buildNumberTextBox = new System.Windows.Forms.TextBox();
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.deployButton = new System.Windows.Forms.Button();
            this.applicationChoiceBox = new System.Windows.Forms.ComboBox();
            this.freshInstallCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // applicationChoiceLabel
            // 
            this.applicationChoiceLabel.AutoSize = true;
            this.applicationChoiceLabel.Location = new System.Drawing.Point(12, 23);
            this.applicationChoiceLabel.Name = "applicationChoiceLabel";
            this.applicationChoiceLabel.Size = new System.Drawing.Size(188, 13);
            this.applicationChoiceLabel.TabIndex = 0;
            this.applicationChoiceLabel.Text = "Choose which application(s) to deploy:";
            // 
            // buildNumberLabel
            // 
            this.buildNumberLabel.AutoSize = true;
            this.buildNumberLabel.Location = new System.Drawing.Point(12, 141);
            this.buildNumberLabel.Name = "buildNumberLabel";
            this.buildNumberLabel.Size = new System.Drawing.Size(73, 13);
            this.buildNumberLabel.TabIndex = 1;
            this.buildNumberLabel.Text = "Build Number:";
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(12, 174);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(41, 13);
            this.serverLabel.TabIndex = 2;
            this.serverLabel.Text = "Server:";
            // 
            // buildNumberTextBox
            // 
            this.buildNumberTextBox.Location = new System.Drawing.Point(95, 138);
            this.buildNumberTextBox.Name = "buildNumberTextBox";
            this.buildNumberTextBox.Size = new System.Drawing.Size(141, 20);
            this.buildNumberTextBox.TabIndex = 4;
            // 
            // serverTextBox
            // 
            this.serverTextBox.Location = new System.Drawing.Point(95, 171);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(141, 20);
            this.serverTextBox.TabIndex = 5;
            // 
            // deployButton
            // 
            this.deployButton.Location = new System.Drawing.Point(95, 252);
            this.deployButton.Name = "deployButton";
            this.deployButton.Size = new System.Drawing.Size(75, 23);
            this.deployButton.TabIndex = 6;
            this.deployButton.Text = "Deploy";
            this.deployButton.UseVisualStyleBackColor = true;
            this.deployButton.Click += new System.EventHandler(this.deployButton_Click);
            // 
            // applicationChoiceBox
            // 
            this.applicationChoiceBox.AutoCompleteCustomSource.AddRange(new string[] {
            "System Manager and Alpha Control Service",
            "CineNet Client",
            "Services and Client"});
            this.applicationChoiceBox.FormattingEnabled = true;
            this.applicationChoiceBox.Items.AddRange(new object[] {
            "System Management and Alpha Control Service",
            "CineNet Client",
            "Services and Client"});
            this.applicationChoiceBox.Location = new System.Drawing.Point(15, 62);
            this.applicationChoiceBox.Name = "applicationChoiceBox";
            this.applicationChoiceBox.Size = new System.Drawing.Size(257, 21);
            this.applicationChoiceBox.TabIndex = 7;
            // 
            // freshInstallCheckBox
            // 
            this.freshInstallCheckBox.AutoSize = true;
            this.freshInstallCheckBox.Location = new System.Drawing.Point(15, 223);
            this.freshInstallCheckBox.Name = "freshInstallCheckBox";
            this.freshInstallCheckBox.Size = new System.Drawing.Size(123, 17);
            this.freshInstallCheckBox.TabIndex = 9;
            this.freshInstallCheckBox.Text = "Is this a fresh install?";
            this.freshInstallCheckBox.UseVisualStyleBackColor = true;
            // 
            // CineNetAutoDeployerForm
            // 
            this.AcceptButton = this.deployButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 287);
            this.Controls.Add(this.freshInstallCheckBox);
            this.Controls.Add(this.applicationChoiceBox);
            this.Controls.Add(this.deployButton);
            this.Controls.Add(this.serverTextBox);
            this.Controls.Add(this.buildNumberTextBox);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.buildNumberLabel);
            this.Controls.Add(this.applicationChoiceLabel);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "CineNetAutoDeployerForm";
            this.Text = "CineNet Auto-Deployer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label applicationChoiceLabel;
        private System.Windows.Forms.Label buildNumberLabel;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.TextBox buildNumberTextBox;
        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.Button deployButton;
        private System.Windows.Forms.ComboBox applicationChoiceBox;
        private System.Windows.Forms.CheckBox freshInstallCheckBox;
    }
}