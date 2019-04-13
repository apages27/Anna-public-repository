namespace LinkerTool
{
    partial class LinkerTool
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
            this.originalItemLabel = new System.Windows.Forms.Label();
            this.newItemLocationLabel = new System.Windows.Forms.Label();
            this.originalItemTextBox = new System.Windows.Forms.TextBox();
            this.newItemLocationTextBox = new System.Windows.Forms.TextBox();
            this.linkButton = new System.Windows.Forms.Button();
            this.originalItemFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.originalItemFolderBrowserButton = new System.Windows.Forms.Button();
            this.newLocationFolderBrowserButton = new System.Windows.Forms.Button();
            this.newLocationFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.resultText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // originalItemLabel
            // 
            this.originalItemLabel.AutoSize = true;
            this.originalItemLabel.Location = new System.Drawing.Point(12, 28);
            this.originalItemLabel.Name = "originalItemLabel";
            this.originalItemLabel.Size = new System.Drawing.Size(135, 13);
            this.originalItemLabel.TabIndex = 0;
            this.originalItemLabel.Text = "Original Item To Be Linked:";
            // 
            // newItemLocationLabel
            // 
            this.newItemLocationLabel.AutoSize = true;
            this.newItemLocationLabel.Location = new System.Drawing.Point(12, 109);
            this.newItemLocationLabel.Name = "newItemLocationLabel";
            this.newItemLocationLabel.Size = new System.Drawing.Size(99, 13);
            this.newItemLocationLabel.TabIndex = 1;
            this.newItemLocationLabel.Text = "New Item Location:";
            // 
            // originalItemTextBox
            // 
            this.originalItemTextBox.Location = new System.Drawing.Point(15, 54);
            this.originalItemTextBox.Name = "originalItemTextBox";
            this.originalItemTextBox.Size = new System.Drawing.Size(307, 20);
            this.originalItemTextBox.TabIndex = 2;
            this.originalItemTextBox.TextChanged += new System.EventHandler(this.originalItemTextBox_TextChanged);
            // 
            // newItemLocationTextBox
            // 
            this.newItemLocationTextBox.Location = new System.Drawing.Point(15, 147);
            this.newItemLocationTextBox.Name = "newItemLocationTextBox";
            this.newItemLocationTextBox.Size = new System.Drawing.Size(307, 20);
            this.newItemLocationTextBox.TabIndex = 3;
            this.newItemLocationTextBox.TextChanged += new System.EventHandler(this.newItemLocationTextBox_TextChanged);
            // 
            // linkButton
            // 
            this.linkButton.Location = new System.Drawing.Point(15, 203);
            this.linkButton.Name = "linkButton";
            this.linkButton.Size = new System.Drawing.Size(75, 23);
            this.linkButton.TabIndex = 4;
            this.linkButton.Text = "Link!";
            this.linkButton.UseVisualStyleBackColor = true;
            this.linkButton.Click += new System.EventHandler(this.linkButton_Click);
            // 
            // originalItemFolderBrowserDialog
            // 
            this.originalItemFolderBrowserDialog.ShowNewFolderButton = false;
            // 
            // originalItemFolderBrowserButton
            // 
            this.originalItemFolderBrowserButton.Location = new System.Drawing.Point(328, 51);
            this.originalItemFolderBrowserButton.Name = "originalItemFolderBrowserButton";
            this.originalItemFolderBrowserButton.Size = new System.Drawing.Size(151, 23);
            this.originalItemFolderBrowserButton.TabIndex = 5;
            this.originalItemFolderBrowserButton.Text = "Choose a file or folder...";
            this.originalItemFolderBrowserButton.UseVisualStyleBackColor = true;
            this.originalItemFolderBrowserButton.Click += new System.EventHandler(this.OriginalItemFolderBrowserButton_Click);
            // 
            // newLocationFolderBrowserButton
            // 
            this.newLocationFolderBrowserButton.Location = new System.Drawing.Point(328, 144);
            this.newLocationFolderBrowserButton.Name = "newLocationFolderBrowserButton";
            this.newLocationFolderBrowserButton.Size = new System.Drawing.Size(151, 23);
            this.newLocationFolderBrowserButton.TabIndex = 6;
            this.newLocationFolderBrowserButton.Text = "Choose a new location...";
            this.newLocationFolderBrowserButton.UseVisualStyleBackColor = true;
            this.newLocationFolderBrowserButton.Click += new System.EventHandler(this.NewLocationFolderBrowserButton_Click);
            // 
            // resultText
            // 
            this.resultText.AutoSize = true;
            this.resultText.ForeColor = System.Drawing.Color.Red;
            this.resultText.Location = new System.Drawing.Point(112, 208);
            this.resultText.Name = "resultText";
            this.resultText.Size = new System.Drawing.Size(0, 13);
            this.resultText.TabIndex = 7;
            // 
            // LinkerTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 262);
            this.Controls.Add(this.resultText);
            this.Controls.Add(this.newLocationFolderBrowserButton);
            this.Controls.Add(this.originalItemFolderBrowserButton);
            this.Controls.Add(this.linkButton);
            this.Controls.Add(this.newItemLocationTextBox);
            this.Controls.Add(this.originalItemTextBox);
            this.Controls.Add(this.newItemLocationLabel);
            this.Controls.Add(this.originalItemLabel);
            this.Name = "LinkerTool";
            this.Text = "Linker Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label originalItemLabel;
        private System.Windows.Forms.Label newItemLocationLabel;
        private System.Windows.Forms.TextBox originalItemTextBox;
        private System.Windows.Forms.TextBox newItemLocationTextBox;
        private System.Windows.Forms.Button linkButton;
        private System.Windows.Forms.FolderBrowserDialog originalItemFolderBrowserDialog;
        private System.Windows.Forms.Button originalItemFolderBrowserButton;
        private System.Windows.Forms.Button newLocationFolderBrowserButton;
        private System.Windows.Forms.FolderBrowserDialog newLocationFolderBrowserDialog;
        private System.Windows.Forms.Label resultText;
    }
}

