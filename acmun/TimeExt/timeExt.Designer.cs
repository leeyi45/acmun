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
            this.minSelector = new System.Windows.Forms.NumericUpDown();
            this.secSelector = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timeLeftLabel = new System.Windows.Forms.Label();
            this.purposeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.minSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secSelector)).BeginInit();
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
            // minSelector
            // 
            this.minSelector.Location = new System.Drawing.Point(71, 104);
            this.minSelector.Name = "minSelector";
            this.minSelector.Size = new System.Drawing.Size(36, 20);
            this.minSelector.TabIndex = 2;
            // 
            // secSelector
            // 
            this.secSelector.Location = new System.Drawing.Point(132, 104);
            this.secSelector.Name = "secSelector";
            this.secSelector.Size = new System.Drawing.Size(35, 20);
            this.secSelector.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(113, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = ":";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Enter Time:";
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
            // timeExt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 168);
            this.Controls.Add(this.purposeLabel);
            this.Controls.Add(this.timeLeftLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.secSelector);
            this.Controls.Add(this.minSelector);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.extendButton);
            this.Name = "timeExt";
            this.Text = "Time Extension";
            ((System.ComponentModel.ISupportInitialize)(this.minSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button extendButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown minSelector;
        private System.Windows.Forms.NumericUpDown secSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label timeLeftLabel;
        private System.Windows.Forms.Label purposeLabel;
    }
}