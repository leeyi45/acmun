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
            this.comboBox = new leeyi45.acmun.Controls.ComboBox();
            this.listBox = new leeyi45.acmun.Controls.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox
            // 
            this.comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(39, 18);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(414, 21);
            this.comboBox.TabIndex = 26;
            // 
            // listBox
            // 
            this.listBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 29;
            this.listBox.Location = new System.Drawing.Point(0, 58);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(457, 410);
            this.listBox.Speakers = ((System.Collections.Generic.List<string>)(resources.GetObject("listBox.Speakers")));
            this.listBox.TabIndex = 25;
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.Location = new System.Drawing.Point(3, 17);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(29, 23);
            this.addButton.TabIndex = 24;
            this.addButton.Text = "+";
            this.addButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
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
            // CountrySelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.addButton);
            this.Name = "CountrySelector";
            this.Size = new System.Drawing.Size(457, 468);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox comboBox;
        private ListBox listBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label1;
    }
}
