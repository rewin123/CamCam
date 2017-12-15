using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;

namespace Synetic_visual
{
    public partial class Form1 : Form
    {
        FilterInfoCollection col;
        public Form1()
        {
            InitializeComponent();

            col = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            for (int i = 0; i < col.Count; i++)
                listBox1.Items.Add(col[i].Name);

            listBox1.MouseDoubleClick += ListBox1_MouseDoubleClick;
        }

        private void ListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Experiment exp = new Experiment(new VideoCaptureDevice(col[listBox1.SelectedIndex].MonikerString));
            exp.Show();
            Hide();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
