namespace leeyi45.acmun.MotionCreator
{
    partial class MotionCreator
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
            this.label1 = new System.Windows.Forms.Label();
            this.topicTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.countryComboBox = new leeyi45.acmun.Controls.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.typeComboBox = new leeyi45.acmun.Controls.ComboBox();
            this.totalTimeSelector = new leeyi45.acmun.Controls.TimeSelector();
            this.speakTimeSelector = new leeyi45.acmun.Controls.TimeSelector();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Motion Type:";
            // 
            // topicTextBox
            // 
            this.topicTextBox.Location = new System.Drawing.Point(12, 123);
            this.topicTextBox.Name = "topicTextBox";
            this.topicTextBox.Size = new System.Drawing.Size(231, 20);
            this.topicTextBox.TabIndex = 25;
            this.topicTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.topicTextBox_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Topic:";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(44, 207);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 27;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(132, 207);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 28;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // countryComboBox
            // 
            this.countryComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.countryComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.Location = new System.Drawing.Point(12, 180);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(231, 21);
            this.countryComboBox.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Proposer";
            // 
            // typeComboBox
            // 
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(12, 28);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(231, 21);
            this.typeComboBox.TabIndex = 0;
            // 
            // totalTimeSelector
            // 
            this.totalTimeSelector.Location = new System.Drawing.Point(11, 53);
            this.totalTimeSelector.Name = "totalTimeSelector";
            this.totalTimeSelector.Size = new System.Drawing.Size(108, 49);
            this.totalTimeSelector.TabIndex = 31;
            this.totalTimeSelector.Text = "Total Duration";
            this.totalTimeSelector.TextBackColor = System.Drawing.SystemColors.Control;
            this.totalTimeSelector.Value = System.TimeSpan.Parse("00:00:01");
            this.totalTimeSelector.Width = 108;
            // 
            // speakTimeSelector
            // 
            this.speakTimeSelector.Location = new System.Drawing.Point(125, 53);
            this.speakTimeSelector.Name = "speakTimeSelector";
            this.speakTimeSelector.Size = new System.Drawing.Size(108, 49);
            this.speakTimeSelector.TabIndex = 32;
            this.speakTimeSelector.Text = "Speaking Time";
            this.speakTimeSelector.TextBackColor = System.Drawing.SystemColors.Control;
            this.speakTimeSelector.Value = System.TimeSpan.Parse("00:00:01");
            this.speakTimeSelector.Width = 108;
            // 
            // MotionCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 242);
            this.Controls.Add(this.speakTimeSelector);
            this.Controls.Add(this.totalTimeSelector);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.countryComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.topicTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.typeComboBox);
            this.Name = "MotionCreator";
            this.Text = "Create Motion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ComboBox typeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox topicTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        private Controls.ComboBox countryComboBox;
        private System.Windows.Forms.Label label5;
        private Controls.TimeSelector totalTimeSelector;
        private Controls.TimeSelector speakTimeSelector;
    }
}