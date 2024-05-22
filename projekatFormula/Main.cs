using Formula.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formula
{
    public partial class Main : Form {

        public Main() {
            InitializeComponent();
            panel1.Parent = pictureBox1;
            label1.Parent = panel1;
            label1.Visible = false;

            panel2.Parent = pictureBox2;
            label2.Parent = panel2;
            label2.Visible = false;

            panel3.Parent = pictureBox3;
            label3.Parent = panel3;
            label3.Visible = false;

        }

        private void panel1_MouseEnter(object sender, EventArgs e) {
            ((Panel)sender).BackColor = Color.FromArgb(127, 0, 0, 0);
            ((Panel)sender).Controls[0].Visible = true;
        }

        private void panel1_MouseLeave(object sender, EventArgs e) {
            ((Panel)sender).BackColor = Color.FromArgb(0, 0, 0, 0);
            ((Panel)sender).Controls[0].Visible = false;
        }

        private void button1_MouseEnter(object sender, EventArgs e) {
            ((Button)sender).BackColor = Color.Red;
            ((Button)sender).ForeColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e) {
            ((Button)sender).BackColor = Color.Black;
            ((Button)sender).ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e) { 
            this.Close();
        }

        private void panel1_Click(object sender, EventArgs e) {
            bool i = false;
            foreach (timovi t in Application.OpenForms.OfType<timovi>())
            {
                t.Focus();
                i = true;
            }
            if (!i) (new timovi()).Show();
        }

        private void panel3_Click(object sender, EventArgs e) {
            bool i = false;
            foreach (Vozaci t in Application.OpenForms.OfType<Vozaci>())
            {
                t.Focus();
                i = true;
            }
            if (!Program.log && !i)
                (new login()).Show();
            else showContext();
        }

        private void panel2_Click(object sender, EventArgs e) {
            bool i = false;
            foreach (Vozaci t in Application.OpenForms.OfType<Vozaci>())
            {
                t.Focus();
                i = true;
            }
            if (!i) (new Vozaci()).Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e) {
            Program.log = false;
            this.contextMenuStrip1.Close();
        }

        public void showContext() {
            Panel btnSender = this.panel3;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            contextMenuStrip1.Show(ptLowerLeft);
        }

        private void timToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool i = false;
            foreach (DodavanjeTima t in Application.OpenForms.OfType<DodavanjeTima>())
            {
                t.Focus();
                i = true;
            }
            if (!i) (new DodavanjeTima()).Show();
        }

        private void osobuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool i = false;
            foreach (Dodavanje_osobe t in Application.OpenForms.OfType<Dodavanje_osobe>())
            {
                t.Focus();
                i = true;
            }
            if (!i) (new Dodavanje_osobe()).Show();
        }

        private void trkuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool i = false;
            foreach (DodavanjeTrke t in Application.OpenForms.OfType<DodavanjeTrke>())
            {
                t.Focus();
                i = true;
            }
            if (!i) (new DodavanjeTrke()).Show();
        }

        private void timToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool i = false;
            foreach (Form1 t in Application.OpenForms.OfType<Form1>())
            {
                t.Focus();
                i = true;
            }
            if (!i) (new Form1()).Show();
        }

        private void osobuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool i = false;
            foreach (IzmenaOsobe t in Application.OpenForms.OfType<IzmenaOsobe>())
            {
                t.Focus();
                i = true;
            }
            if (!i) (new IzmenaOsobe()).Show();
        }

        private void timToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool i = false;
            foreach (BrisanjeTima t in Application.OpenForms.OfType<BrisanjeTima>())
            {
                t.Focus();
                i = true;
            }
            if (!i) (new BrisanjeTima()).Show();
        }

        private void osobuToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool i = false;
            foreach (BrisanjeOsobe t in Application.OpenForms.OfType<BrisanjeOsobe>())
            {
                t.Focus();
                i = true;
            }
            if (!i) (new BrisanjeOsobe()).Show();
        }
    }
}
