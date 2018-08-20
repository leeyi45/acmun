using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static leeyi45.acmun.Council;

namespace leeyi45.acmun.Controls
{
    public partial class CountrySelector : UserControl
    {
        public CountrySelector()
        {
            InitializeComponent();

            listBox.ClickSelect += ListBox_ClickSelect;
            addButton.Click += AddButton_Click;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (comboBox?.SelectedItem == null) return;
            AddSpeaker(Present[comboBox.SelectedIndex]);

            comboBox.SelectedIndex = -1;
        }

        #region Combobox
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int ComboBoxSelectedIndex
        {
            get => comboBox.SelectedIndex;
            set => comboBox.SelectedIndex = value;
        }

        public void ComboBoxResetItems(string[] items) => comboBox.ResetItems(items);

        public ComboBox.ObjectCollection ComboBoxItems => comboBox.Items;
        #endregion

        #region listBox
        public List<string> Speakers
        {
            get => listBox.Speakers;
            set => listBox.Speakers = value;

        }

        public void AddSpeaker(Delegation del) => listBox.AddSpeaker(del);

        public void RemoveSpeaker(int index) => listBox.RemoveSpeaker(index);

        public void ClearSpeakers()
        {
            listBox.Clear();
            comboBox.Text = "";
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int ListBoxSelectedIndex
        {
            get => listBox.SelectedIndex;
            set => listBox.SelectedIndex = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public object ListBoxSelectedItem
        {
            get => listBox.SelectedItem;
            set => listBox.SelectedItem = value;
        }

        private void ListBox_ClickSelect(object sender, int index)
            => ClickSelect?.Invoke(this, index);

        public event ListBoxClickHandler ClickSelect;
        #endregion

        public string LabelText
        {
            get => label1.Text;
            set => label1.Text = value;
        }
    }
}
