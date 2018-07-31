namespace leeyi45.acmun
{
    partial class DurationPicker
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
            this.timeSelector1 = new leeyi45.acmun.Controls.TimeSelector();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timeSelector1
            // 
            this.timeSelector1.Location = new System.Drawing.Point(51, 12);
            this.timeSelector1.Name = "timeSelector1";
            this.timeSelector1.Size = new System.Drawing.Size(106, 61);
            this.timeSelector1.TabIndex = 0;
            this.timeSelector1.Text = "Select New Time";
            this.timeSelector1.TextBackColor = System.Drawing.SystemColors.Control;
            this.timeSelector1.Value = System.TimeSpan.Parse("00:00:01");
            this.timeSelector1.Width = 106;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(133, 87);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // DurationPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 122);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.timeSelector1);
            this.Name = "DurationPicker";
            this.Text = "DurationPicker";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TimeSelector timeSelector1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}