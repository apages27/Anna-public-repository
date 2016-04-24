namespace CineViewEmulatorForTCPTesting
{
    partial class Emulator
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
            this.resultsTextBox = new System.Windows.Forms.TextBox();
            this.sendTestMsgButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resultsTextBox
            // 
            this.resultsTextBox.Location = new System.Drawing.Point(12, 91);
            this.resultsTextBox.Multiline = true;
            this.resultsTextBox.Name = "resultsTextBox";
            this.resultsTextBox.Size = new System.Drawing.Size(498, 450);
            this.resultsTextBox.TabIndex = 3;
            // 
            // sendTestMsgButton
            // 
            this.sendTestMsgButton.Location = new System.Drawing.Point(190, 37);
            this.sendTestMsgButton.Name = "sendTestMsgButton";
            this.sendTestMsgButton.Size = new System.Drawing.Size(126, 23);
            this.sendTestMsgButton.TabIndex = 4;
            this.sendTestMsgButton.Text = "Send Test Message";
            this.sendTestMsgButton.UseVisualStyleBackColor = true;
            this.sendTestMsgButton.Click += new System.EventHandler(this.sendTestMsgButton_Click);
            // 
            // Emulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 553);
            this.Controls.Add(this.sendTestMsgButton);
            this.Controls.Add(this.resultsTextBox);
            this.Name = "Emulator";
            this.Text = "TCP Emulator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox resultsTextBox;
        private System.Windows.Forms.Button sendTestMsgButton;
    }
}

