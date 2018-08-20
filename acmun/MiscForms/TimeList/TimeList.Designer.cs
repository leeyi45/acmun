namespace leeyi45.acmun.TimeList
{
    partial class TimeList
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DelColumn,
            this.TimeColumn,
            this.CountColumn});
            this.dataGridView1.Location = new System.Drawing.Point(28, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(430, 386);
            this.dataGridView1.TabIndex = 0;
            // 
            // DelColumn
            // 
            this.DelColumn.HeaderText = "Delegation";
            this.DelColumn.Name = "DelColumn";
            this.DelColumn.ReadOnly = true;
            // 
            // TimeColumn
            // 
            this.TimeColumn.HeaderText = "Speaking Time";
            this.TimeColumn.Name = "TimeColumn";
            this.TimeColumn.Width = 120;
            // 
            // CountColumn
            // 
            this.CountColumn.HeaderText = "Speech Count";
            this.CountColumn.Name = "CountColumn";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(28, 433);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "button1";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // TimeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 468);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TimeList";
            this.Text = "TimeList";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DelColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountColumn;
        private System.Windows.Forms.Button closeButton;
    }
}