namespace leeyi45.acmun.AboutBox
{
    partial class AboutBox
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabs = new System.Windows.Forms.TabControl();
            this.versionTab = new System.Windows.Forms.TabPage();
            this.websiteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.websiteText = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.closeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabs.SuspendLayout();
            this.versionTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(441, 145);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.versionTab);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Location = new System.Drawing.Point(12, 163);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(442, 141);
            this.tabs.TabIndex = 1;
            // 
            // versionTab
            // 
            this.versionTab.Controls.Add(this.label2);
            this.versionTab.Controls.Add(this.label1);
            this.versionTab.Controls.Add(this.websiteLinkLabel);
            this.versionTab.Controls.Add(this.websiteText);
            this.versionTab.Controls.Add(this.versionLabel);
            this.versionTab.Controls.Add(this.nameLabel);
            this.versionTab.Location = new System.Drawing.Point(4, 22);
            this.versionTab.Name = "versionTab";
            this.versionTab.Padding = new System.Windows.Forms.Padding(3);
            this.versionTab.Size = new System.Drawing.Size(434, 115);
            this.versionTab.TabIndex = 0;
            this.versionTab.Text = "Version";
            this.versionTab.UseVisualStyleBackColor = true;
            // 
            // websiteLinkLabel
            // 
            this.websiteLinkLabel.AutoSize = true;
            this.websiteLinkLabel.Location = new System.Drawing.Point(58, 87);
            this.websiteLinkLabel.Name = "websiteLinkLabel";
            this.websiteLinkLabel.Size = new System.Drawing.Size(170, 13);
            this.websiteLinkLabel.TabIndex = 3;
            this.websiteLinkLabel.TabStop = true;
            this.websiteLinkLabel.Text = "https://github.com/leeyi45/acmun";
            this.websiteLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // websiteText
            // 
            this.websiteText.AutoSize = true;
            this.websiteText.Location = new System.Drawing.Point(6, 87);
            this.websiteText.Name = "websiteText";
            this.websiteText.Size = new System.Drawing.Size(49, 13);
            this.websiteText.TabIndex = 2;
            this.websiteText.Text = "Website:";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(58, 57);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(28, 13);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "0.40";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(58, 29);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(39, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "acmun";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(434, 115);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(12, 306);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Version:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 337);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AboutBox";
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabs.ResumeLayout(false);
            this.versionTab.ResumeLayout(false);
            this.versionTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage versionTab;
        private System.Windows.Forms.Label websiteText;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.LinkLabel websiteLinkLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}