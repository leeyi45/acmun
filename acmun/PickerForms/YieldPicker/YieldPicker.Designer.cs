namespace leeyi45.acmun.YieldPicker
{
    partial class YieldPicker
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
            this.countryBox = new leeyi45.acmun.Controls.ListBox();
            this.yieldButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // countryBox
            // 
            this.countryBox.clickDuration = 70;
            this.countryBox.FormattingEnabled = true;
            this.countryBox.Location = new System.Drawing.Point(12, 37);
            this.countryBox.Name = "countryBox";
            this.countryBox.Size = new System.Drawing.Size(394, 238);
            this.countryBox.TabIndex = 0;
            // 
            // yieldButton
            // 
            this.yieldButton.Location = new System.Drawing.Point(12, 282);
            this.yieldButton.Name = "yieldButton";
            this.yieldButton.Size = new System.Drawing.Size(75, 23);
            this.yieldButton.TabIndex = 1;
            this.yieldButton.Text = "Yield";
            this.yieldButton.UseVisualStyleBackColor = true;
            this.yieldButton.Click += new System.EventHandler(this.yieldButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(331, 281);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select country to yield to:";
            // 
            // YieldPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 313);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.yieldButton);
            this.Controls.Add(this.countryBox);
            this.Name = "YieldPicker";
            this.Text = "Yield";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ListBox countryBox;
        private System.Windows.Forms.Button yieldButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
    }
}