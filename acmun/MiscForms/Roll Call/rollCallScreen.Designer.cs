namespace leeyi45.acmun.Roll_Call
{
    partial class rollCallScreen
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
            this.rollCallBox = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.quorumLabel = new System.Windows.Forms.Label();
            this.allPresentCheckBox = new System.Windows.Forms.CheckBox();
            this.doneButton = new System.Windows.Forms.Button();
            this.observerCheckBox = new System.Windows.Forms.CheckBox();
            this.councilNameLabelBox = new leeyi45.acmun.Controls.LabelBox();
            this.SuspendLayout();
            // 
            // rollCallBox
            // 
            this.rollCallBox.FormattingEnabled = true;
            this.rollCallBox.Location = new System.Drawing.Point(12, 50);
            this.rollCallBox.Name = "rollCallBox";
            this.rollCallBox.Size = new System.Drawing.Size(383, 259);
            this.rollCallBox.TabIndex = 1;
            this.rollCallBox.ThreeDCheckBoxes = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Quorum:";
            // 
            // quorumLabel
            // 
            this.quorumLabel.AutoSize = true;
            this.quorumLabel.Location = new System.Drawing.Point(67, 31);
            this.quorumLabel.Name = "quorumLabel";
            this.quorumLabel.Size = new System.Drawing.Size(35, 13);
            this.quorumLabel.TabIndex = 3;
            this.quorumLabel.Text = "label2";
            // 
            // allPresentCheckBox
            // 
            this.allPresentCheckBox.AutoSize = true;
            this.allPresentCheckBox.Location = new System.Drawing.Point(324, 315);
            this.allPresentCheckBox.Name = "allPresentCheckBox";
            this.allPresentCheckBox.Size = new System.Drawing.Size(76, 17);
            this.allPresentCheckBox.TabIndex = 4;
            this.allPresentCheckBox.Text = "All Present";
            this.allPresentCheckBox.UseVisualStyleBackColor = true;
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(160, 335);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(75, 23);
            this.doneButton.TabIndex = 5;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // observerCheckBox
            // 
            this.observerCheckBox.AutoSize = true;
            this.observerCheckBox.Enabled = false;
            this.observerCheckBox.Location = new System.Drawing.Point(12, 315);
            this.observerCheckBox.Name = "observerCheckBox";
            this.observerCheckBox.Size = new System.Drawing.Size(69, 17);
            this.observerCheckBox.TabIndex = 7;
            this.observerCheckBox.Text = "Observer";
            this.observerCheckBox.UseVisualStyleBackColor = true;
            // 
            // councilNameLabelBox
            // 
            this.councilNameLabelBox.Location = new System.Drawing.Point(16, 9);
            this.councilNameLabelBox.Name = "councilNameLabelBox";
            this.councilNameLabelBox.Size = new System.Drawing.Size(379, 13);
            this.councilNameLabelBox.TabIndex = 6;
            this.councilNameLabelBox.Text = "Roll Call for: ";
            this.councilNameLabelBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // rollCallScreen
            // 
            this.AcceptButton = this.doneButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 363);
            this.Controls.Add(this.observerCheckBox);
            this.Controls.Add(this.councilNameLabelBox);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.allPresentCheckBox);
            this.Controls.Add(this.quorumLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rollCallBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "rollCallScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Roll Call";
            this.Load += new System.EventHandler(this.rollCallScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox rollCallBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label quorumLabel;
        private System.Windows.Forms.CheckBox allPresentCheckBox;
        private System.Windows.Forms.Button doneButton;
        private Controls.LabelBox councilNameLabelBox;
        private System.Windows.Forms.CheckBox observerCheckBox;
    }
}