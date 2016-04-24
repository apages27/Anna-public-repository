namespace CineNetAutoDeployer
{
    partial class ConfigValueForm
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
            this.inputSystemTypeLabel = new System.Windows.Forms.Label();
            this.outputSystemTypeLabel = new System.Windows.Forms.Label();
            this.verticalPanelsLabel = new System.Windows.Forms.Label();
            this.horizontalPanelsLabel = new System.Windows.Forms.Label();
            this.inputSystemTypeComboBox = new System.Windows.Forms.ComboBox();
            this.outputSystemTypeComboBox = new System.Windows.Forms.ComboBox();
            this.verticalPanelsTextBox = new System.Windows.Forms.TextBox();
            this.horizontalPanelsTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputSystemTypeLabel
            // 
            this.inputSystemTypeLabel.AutoSize = true;
            this.inputSystemTypeLabel.Location = new System.Drawing.Point(12, 20);
            this.inputSystemTypeLabel.Name = "inputSystemTypeLabel";
            this.inputSystemTypeLabel.Size = new System.Drawing.Size(98, 13);
            this.inputSystemTypeLabel.TabIndex = 0;
            this.inputSystemTypeLabel.Text = "Input System Type:";
            // 
            // outputSystemTypeLabel
            // 
            this.outputSystemTypeLabel.AutoSize = true;
            this.outputSystemTypeLabel.Location = new System.Drawing.Point(12, 62);
            this.outputSystemTypeLabel.Name = "outputSystemTypeLabel";
            this.outputSystemTypeLabel.Size = new System.Drawing.Size(106, 13);
            this.outputSystemTypeLabel.TabIndex = 1;
            this.outputSystemTypeLabel.Text = "Output System Type:";
            // 
            // verticalPanelsLabel
            // 
            this.verticalPanelsLabel.AutoSize = true;
            this.verticalPanelsLabel.Location = new System.Drawing.Point(12, 104);
            this.verticalPanelsLabel.Name = "verticalPanelsLabel";
            this.verticalPanelsLabel.Size = new System.Drawing.Size(80, 13);
            this.verticalPanelsLabel.TabIndex = 2;
            this.verticalPanelsLabel.Text = "Vertical Panels:";
            // 
            // horizontalPanelsLabel
            // 
            this.horizontalPanelsLabel.AutoSize = true;
            this.horizontalPanelsLabel.Location = new System.Drawing.Point(12, 145);
            this.horizontalPanelsLabel.Name = "horizontalPanelsLabel";
            this.horizontalPanelsLabel.Size = new System.Drawing.Size(92, 13);
            this.horizontalPanelsLabel.TabIndex = 3;
            this.horizontalPanelsLabel.Text = "Horizontal Panels:";
            // 
            // inputSystemTypeComboBox
            // 
            this.inputSystemTypeComboBox.FormattingEnabled = true;
            this.inputSystemTypeComboBox.Items.AddRange(new object[] {
            "DataPath",
            "Matrox"});
            this.inputSystemTypeComboBox.Location = new System.Drawing.Point(116, 17);
            this.inputSystemTypeComboBox.Name = "inputSystemTypeComboBox";
            this.inputSystemTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.inputSystemTypeComboBox.TabIndex = 6;
            // 
            // outputSystemTypeComboBox
            // 
            this.outputSystemTypeComboBox.FormattingEnabled = true;
            this.outputSystemTypeComboBox.Items.AddRange(new object[] {
            "DataPath",
            "Matrox",
            "AlphaFX"});
            this.outputSystemTypeComboBox.Location = new System.Drawing.Point(124, 59);
            this.outputSystemTypeComboBox.Name = "outputSystemTypeComboBox";
            this.outputSystemTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.outputSystemTypeComboBox.TabIndex = 7;
            // 
            // verticalPanelsTextBox
            // 
            this.verticalPanelsTextBox.Location = new System.Drawing.Point(98, 101);
            this.verticalPanelsTextBox.Name = "verticalPanelsTextBox";
            this.verticalPanelsTextBox.Size = new System.Drawing.Size(100, 20);
            this.verticalPanelsTextBox.TabIndex = 8;
            // 
            // horizontalPanelsTextBox
            // 
            this.horizontalPanelsTextBox.Location = new System.Drawing.Point(110, 142);
            this.horizontalPanelsTextBox.Name = "horizontalPanelsTextBox";
            this.horizontalPanelsTextBox.Size = new System.Drawing.Size(100, 20);
            this.horizontalPanelsTextBox.TabIndex = 9;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(98, 207);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // ConfigValueForm
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 246);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.horizontalPanelsTextBox);
            this.Controls.Add(this.verticalPanelsTextBox);
            this.Controls.Add(this.outputSystemTypeComboBox);
            this.Controls.Add(this.inputSystemTypeComboBox);
            this.Controls.Add(this.horizontalPanelsLabel);
            this.Controls.Add(this.verticalPanelsLabel);
            this.Controls.Add(this.outputSystemTypeLabel);
            this.Controls.Add(this.inputSystemTypeLabel);
            this.Name = "ConfigValueForm";
            this.Text = "Configuration Values Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label inputSystemTypeLabel;
        private System.Windows.Forms.Label outputSystemTypeLabel;
        private System.Windows.Forms.Label verticalPanelsLabel;
        private System.Windows.Forms.Label horizontalPanelsLabel;
        private System.Windows.Forms.ComboBox inputSystemTypeComboBox;
        private System.Windows.Forms.ComboBox outputSystemTypeComboBox;
        private System.Windows.Forms.TextBox verticalPanelsTextBox;
        private System.Windows.Forms.TextBox horizontalPanelsTextBox;
        private System.Windows.Forms.Button saveButton;
    }
}