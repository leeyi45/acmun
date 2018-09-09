namespace leeyi45.acmun.Controls
{
    partial class CountrySelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CountrySelector));
            this.listBox = new leeyi45.acmun.Controls.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboButton1 = new leeyi45.acmun.Controls.ComboButton();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 29;
            this.listBox.Location = new System.Drawing.Point(0, 58);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(457, 410);
            this.listBox.Speakers = ((System.Collections.Generic.List<string>)(resources.GetObject("listBox.Speakers")));
            this.listBox.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Countries:";
            // 
            // comboButton1
            // 
            this.comboButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboButton1.ItemHeight = 13;
            this.comboButton1.Location = new System.Drawing.Point(0, 17);
            this.comboButton1.MinimumSize = new System.Drawing.Size(34, 23);
            this.comboButton1.Name = "comboButton1";
            this.comboButton1.SelectedIndex = -1;
            this.comboButton1.SelectedItem = null;
            this.comboButton1.Size = new System.Drawing.Size(451, 23);
            this.comboButton1.TabIndex = 28;
            // 
            // CountrySelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox);
            this.Name = "CountrySelector";
            this.Size = new System.Drawing.Size(457, 468);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ListBox listBox;
        private System.Windows.Forms.Label label1;
        private ComboButton comboButton1;
    }
}
