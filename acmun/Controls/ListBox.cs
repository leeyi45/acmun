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

            MouseTimer = new Stopwatch();
        }

        Stopwatch MouseTimer;

        bool processClick = true;

        bool mouseUp = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(90)]
        public int clickDuration { get; set; }

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
                    DoDragDrop(SelectedItem, DragDropEffects.Move);
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
            Items.Insert(dIndex, name);

            DragDone?.Invoke(this, fIndex, dIndex);
        }

        public void Deselect()
        {
            SelectedIndex = -1;
        }

        public event ListBoxClickHandler ClickSelect;

        public event ListBoxDragHandler DragDone;
    }

    public delegate void ListBoxClickHandler(object sender, int Index);

    public delegate void ListBoxDragHandler(object sender, int oldIndex, int newIndex);
}
