namespace leeyi45.acmun.Controls
{
    partial class TimeSelector
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
            this.minSelector = new System.Windows.Forms.NumericUpDown();
            this.secSelector = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.mainLabel = new leeyi45.acmun.Controls.LabelBox();
            ((System.ComponentModel.ISupportInitialize)(this.minSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // minSelector
            // 
            this.minSelector.Location = new System.Drawing.Point(4, 23);
            this.minSelector.Name = "minSelector";
            this.minSelector.Size = new System.Drawing.Size(39, 20);
            this.minSelector.TabIndex = 1;
            // 
            // secSelector
            // 
            this.secSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.secSelector.Location = new System.Drawing.Point(66, 23);
            this.secSelector.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.secSelector.Name = "secSelector";
            this.secSelector.Size = new System.Drawing.Size(39, 20);
            this.secSelector.TabIndex = 2;
            this.secSelector.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(49, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = ":";
            // 
            // mainLabel
            // 
            this.mainLabel.BackColor = System.Drawing.SystemColors.Window;
            this.mainLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLabel.Location = new System.Drawing.Point(0, 0);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(108, 13);
            this.mainLabel.TabIndex = 4;
            // 
            // TimeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.secSelector);
            this.Controls.Add(this.minSelector);
            this.Name = "TimeSelector";
            this.Size = new System.Drawing.Size(108, 49);
            ((System.ComponentModel.ISupportInitialize)(this.minSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown minSelector;
        private System.Windows.Forms.NumericUpDown secSelector;
        private System.Windows.Forms.Label label2;
        private leeyi45.acmun.Controls.LabelBox mainLabel;
    }
}
