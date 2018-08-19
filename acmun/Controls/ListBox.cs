using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace leeyi45.acmun.Controls
{
    class ListBox : System.Windows.Forms.ListBox
    {
        public ListBox()
        {
            MouseUp += mouseUpHandler;
            MouseDown += mouseDownHandler;

            DragOver += dragOver;
            DragDrop += dragDrop;

            AllowDrop = true;

            MouseTimer = new Stopwatch();
            speakers = new List<string>();
        }

        Stopwatch MouseTimer;

        bool processClick = true;

        bool mouseUp = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(120)]
        public int clickDuration { get; set; } = 120;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(true)]
        public new bool AllowDrop
        {
            get => base.AllowDrop;
            private set => base.AllowDrop = value;
        }

        int prevIndex = -1;

        private void mouseUpHandler(object sender, MouseEventArgs e)
            => mouseUp = true;

        private void mouseDownHandler(object sender, MouseEventArgs e)
        {
            if (Items.Count == 0) return;
            if (!processClick) return;

            processClick = false;

            var thing = new Thread(new ThreadStart(() => { clickThread(e); }));
            thing.Start();
        }

        private void clickThread(MouseEventArgs e)
        {
            MouseTimer.Start();
            while (true)
            {
                if (mouseUp || MouseTimer.ElapsedMilliseconds >= clickDuration)
                {
                    MouseTimer.Stop();
                    break;
                }
            }
            
            var drag = MouseTimer.ElapsedMilliseconds >= clickDuration;
            MouseTimer.Reset();

            clickSelect(e, drag);

            mouseUp = false;
            processClick = true;
        }

        private void clickSelect(MouseEventArgs e, bool drag)
        {
            if (InvokeRequired)
            {
                var thing = new Action(() => { clickSelect(e, drag); });
                Invoke(thing);
            }
            else
            {
                if (drag)
                { //Drag Drop
                    if (SelectedItem == null) return;
                    DoDragDrop(SelectedItem, DragDropEffects.All);
                }
                else
                { //Single Click
                    var index = IndexFromPoint(PointToClient(e.Location));
                    if (index < 0) index = Items.Count - 1;

                    if (index > Items.Count) return;

                    if (index == SelectedIndex)
                    {
                        if (index == prevIndex) ClickSelect?.Invoke(this, index);
                        else prevIndex = SelectedIndex;
                    }
                }
            }
        }

        private void dragOver(object sender, DragEventArgs e)
            => e.Effect = DragDropEffects.Move;

        private void dragDrop(object sender, DragEventArgs e)
        {
            if (Items.Count <= 1) return;

            int dIndex = IndexFromPoint(PointToClient(new Point(e.X, e.Y)));
            if (dIndex < 0 || dIndex > Items.Count - 1) dIndex = Items.Count - 1;
            var name = (string)e.Data.GetData(typeof(string));

            var fIndex = Items.IndexOf(name);

            Items.RemoveAt(fIndex);
            Speakers.RemoveAt(fIndex);

            Items.Insert(dIndex, name);
            Speakers.Insert(dIndex, name);

            //DragDone?.Invoke(this, fIndex, dIndex);
        }

        public List<string> Speakers
        {
            get => speakers;
            set
            {
                speakers = value;
                Items.Clear();
                Items.AddRange(value.ToArray());
            }
        }

        private List<string> speakers;

        public void Clear()
        {
            Items.Clear();
            speakers.Clear();
        }

        public void AddSpeaker(Delegation del)
        {
            speakers.Add(del.Shortf);
            Items.Add(del.Name);
        }

        public void RemoveSpeaker(int index)
        {
            Items.RemoveAt(index);
            speakers.RemoveAt(index);
        }

        public void Deselect() => SelectedIndex = -1;

        public event ListBoxClickHandler ClickSelect;
    }

    public delegate void ListBoxClickHandler(object sender, int Index);
}
