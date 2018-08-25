namespace leeyi45.acmun.Controls
{
    partial class WarningBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.leftButton = new System.Windows.Forms.Button();
            this.middleButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.TextBox = new leeyi45.acmun.Controls.LabelBox();
            this.SuspendLayout();
            // 
            // leftButton
            // 
            this.leftButton.Location = new System.Drawing.Point(4, 81);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(75, 23);
            this.leftButton.TabIndex = 0;
            this.leftButton.Text = "button1";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.button1_click);
            // 
            // middleButton
            // 
            this.middleButton.Location = new System.Drawing.Point(89, 81);
            this.middleButton.Name = "middleButton";
            this.middleButton.Size = new System.Drawing.Size(75, 23);
            this.middleButton.TabIndex = 1;
            this.middleButton.Text = "button1";
            this.middleButton.UseVisualStyleBackColor = true;
            this.middleButton.Click += new System.EventHandler(this.button2_click);
            // 
            // rightButton
            // 
            this.rightButton.Location = new System.Drawing.Point(173, 81);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(75, 23);
            this.rightButton.TabIndex = 2;
            this.rightButton.Text = "button1";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.button3_click);
            // 
            // TextBox
            // 
            this.TextBox.BackColor = System.Drawing.SystemColors.Window;
            this.TextBox.Location = new System.Drawing.Point(4, 4);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(244, 71);
            this.TextBox.TabIndex = 3;
            // 
            // WarningBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.middleButton);
            this.Controls.Add(this.leftButton);
            this.Name = "WarningBox";
            this.Size = new System.Drawing.Size(251, 107);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.Button middleButton;
        private System.Windows.Forms.Button rightButton;
        private LabelBox TextBox;
    }
}
