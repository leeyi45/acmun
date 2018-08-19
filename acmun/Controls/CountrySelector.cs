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
            List = new List<Delegation>();
        }

        private Button clearButton;
        public Button ClearButton
        {
            get => clearButton;
            set
            {
                clearButton = value;
                clearButton.Click += ClearButton_Click;
            }
        }

        private Button removeButton;
        public Button RemoveButton
        {
            get => removeButton;
            set
            {
                removeButton = value;
                removeButton.Click += RemoveButton_Click;
            }
        }

        public int SelectedIndex
        {
            get => listBox.SelectedIndex;
            set => listBox.SelectedIndex = value;
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (listBox.Items.Count == 0) return;

            if (listBox.SelectedItem == null) return;

            var selectedIndex = listBox.SelectedIndex;
            listBox.Items.RemoveAt(listBox.SelectedIndex);
            List.RemoveAt(listBox.SelectedIndex);

            if (listBox.Items.Count == 0) return;

            listBox.SelectedIndex = Math.Max(0, selectedIndex - 1);
        }

        private void ClearButton_Click(object sender, EventArgs e)
            => listBox.Items.Clear();

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (comboBox?.SelectedItem == null) return;

            var country = Present[comboBox.SelectedIndex];
            listBox.Items.Add(country.Name);
            List.Insert(List.Count, country);

            comboBox.SelectedIndex = -1;
        }

        public int ComboBoxSelectedIndex
        {
            get => comboBox.SelectedIndex;
            set => comboBox.SelectedIndex = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private List<Delegation> List { get; set; }

        public new int Height
        {
            get => base.Height;
            set
            {
                base.Height = value;
                listBox.Height = value - 2;
            }
        }
    }
}
