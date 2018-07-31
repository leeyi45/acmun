namespace leeyi45.acmun.TimeExt
{
    partial class timeExt
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
            this.extendButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.timeLeftLabel = new System.Windows.Forms.Label();
            this.purposeLabel = new System.Windows.Forms.Label();
            this.timeSelector1 = new leeyi45.acmun.Controls.TimeSelector();
            this.SuspendLayout();
            // 
            // extendButton
            // 
            this.extendButton.Location = new System.Drawing.Point(61, 130);
            this.extendButton.Name = "extendButton";
            this.extendButton.Size = new System.Drawing.Size(56, 23);
            this.extendButton.TabIndex = 0;
            this.extendButton.Text = "Extend";
            this.extendButton.UseVisualStyleBackColor = true;
            this.extendButton.Click += new System.EventHandler(this.extendButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(123, 130);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(53, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // timeLeftLabel
            // 
            this.timeLeftLabel.AutoSize = true;
            this.timeLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLeftLabel.Location = new System.Drawing.Point(83, 49);
            this.timeLeftLabel.Name = "timeLeftLabel";
            this.timeLeftLabel.Size = new System.Drawing.Size(60, 24);
            this.timeLeftLabel.TabIndex = 6;
            this.timeLeftLabel.Text = "label3";
            // 
            // purposeLabel
            // 
            this.purposeLabel.AutoSize = true;
            this.purposeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purposeLabel.Location = new System.Drawing.Point(12, 26);
            this.purposeLabel.Name = "purposeLabel";
            this.purposeLabel.Size = new System.Drawing.Size(216, 16);
            this.purposeLabel.TabIndex = 7;
            this.purposeLabel.Text = "Extend Time for moderated caucus";
            // 
            // timeSelector1
            // 
            this.timeSelector1.Location = new System.Drawing.Point(68, 76);
            this.timeSelector1.Name = "timeSelector1";
            this.timeSelector1.Size = new System.Drawing.Size(108, 49);
            this.timeSelector1.TabIndex = 8;
            this.timeSelector1.Text = "Select Time";
            this.timeSelector1.TextBackColor = System.Drawing.SystemColors.Control;
            this.timeSelector1.Value = System.TimeSpan.Parse("00:00:00");
            this.timeSelector1.Width = 108;
            // 
            // timeExt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 168);
            this.Controls.Add(this.timeSelector1);
            this.Controls.Add(this.purposeLabel);
            this.Controls.Add(this.timeLeftLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.extendButton);
            this.Name = "timeExt";
            this.Text = "Time Extension";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button extendButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label timeLeftLabel;
        private System.Windows.Forms.Label purposeLabel;
        private Controls.TimeSelector timeSelector1;
    }
}