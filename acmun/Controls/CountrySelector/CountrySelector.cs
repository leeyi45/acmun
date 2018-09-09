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
            comboButton1.AddButtonClick += AddButton_Click;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (comboButton1?.SelectedItem == null) return;
            AddSpeaker(Present[comboButton1.SelectedIndex]);

            comboButton1.SelectedIndex = -1;
        }

        #region ComboBox
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int ComboBoxSelectedIndex
        {
            get => comboButton1.SelectedIndex;
            set => comboButton1.SelectedIndex = value;
        }

        public void ComboBoxResetItems(string[] items) => comboButton1.ResetItems(items);

        [Category("ComboBox")]
        [Description("ComboBoxItems")]
        public ComboBox.ObjectCollection ComboBoxItems => comboButton1.Items;

        [Category("ComboBox")]
        [Description("Font settings for the ComboBox")]
        public Font ComboBoxFont
        {
            get => comboButton1.Font;
            set => comboButton1.Font = value;
        }

        [Category("ComboBox")]
        [Description("Size of the ComboBox")]
        public Size ComboBoxSize
        {
            get => comboButton1.Size;
            set => comboButton1.Size = value;
        }
        #endregion

        #region listBox
        [Category("ListBox")]
        [Description("List of all the speakers currently on the list")]
        public List<string> Speakers
        {
            get => listBox.Speakers;
            set => listBox.Speakers = value;

        }

        [Category("ListBox")]
        [Description("Font settings for the ListBox")]
        public Font ListBoxFont
        {
            get => listBox.Font;
            set => listBox.Font = value;
        }

        public void AddSpeaker(Delegation del) => listBox.AddSpeaker(del);

        public void RemoveSpeaker(int index) => listBox.RemoveSpeaker(index);

        public void ClearSpeakers()
        {
            listBox.ClearSpeakers();
            comboButton1.Text = "";
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

        [Category("ListBox")]
        public event ListBoxClickHandler ClickSelect;
        #endregion

        [Category("Appearance")]
        [Description("Controls the text that the label displays")]
        public string LabelText
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new Font Font => base.Font;

        [Category("Behavior")]
        [Description("Value indicating if delegations can be selected straight from the combobox")]
        [DefaultValue(true)]
        public bool AllowComboSelect { get; set; }

        public string NextSpeaker(int index = 0)
        {
            if (listBox.Speakers.Count > 0)
            {
                var output = listBox.Speakers[index];
                listBox.RemoveSpeaker(index);
                return output;
            }
            else if (comboButton1.SelectedItem != null && index != 0 && AllowComboSelect)
            {
                var output = (string)comboButton1.SelectedItem;
                comboButton1.SelectedIndex = -1;
                return output;
            }
            else return null;
        }
    }
}
