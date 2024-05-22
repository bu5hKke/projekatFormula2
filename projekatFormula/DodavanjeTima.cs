using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formula
{
    public partial class DodavanjeTima : Form
    {
        string ime, opis, imgPath;
        int godina;
        Image slika;

        public DodavanjeTima()
        {
            InitializeComponent();
        }

        private void DodavanjeTima_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
            
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Red;
            ((Button)sender).ForeColor = Color.Black;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Black;
            ((Button)sender).ForeColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DodavanjeTima_DragDrop(object sender, DragEventArgs e) {
            string imgPath = ((String[])e.Data.GetData(DataFormats.FileDrop))[0];
            try {
                slika = Image.FromFile(imgPath);
            } catch {
                MessageBox.Show("Doslo je do greske pri citanju slike", "Greska");
            }
            
            pictureBox1.Image = slika;
            
        }

        private void button1_Click(object sender, EventArgs e) {
            SqlConnection conn = sql.vrati_vezu();
            ime = textBox1.Text;
            godina = int.Parse(textBox3.Text);
            opis = textBox2.Text;
            imgPath = ime.ToLower();

            slika.Save(sql.imgFromResource(imgPath));

            string comm = "insert into tim values ( '" + ime + "', '"
                + imgPath + "', " + godina + ", '" + opis + "')";
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = comm;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Tim uspesno dodat.", "Dodaj tim");
            this.Close();
        }
    }
}
