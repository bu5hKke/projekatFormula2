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
    public partial class Form1 : Form
    {

        string ime, opis, imgPath;
        int godina;
        Image slika;
        DataTable dt;

        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e) {
            int tim = (int)comboBox1.SelectedIndex;
            textBox1.Text = dt.Rows[tim]["ime"].ToString();
            textBox2.Text = dt.Rows[tim]["opis"].ToString();
            textBox3.Text = dt.Rows[tim]["prvaGodina"].ToString();
            imgPath = dt.Rows[tim]["imgPath"].ToString();
            imgPath = sql.imgFromResource(imgPath);
            try {
                slika = Image.FromFile(imgPath);
                pictureBox1.Image = slika;
            } catch { }
            
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string imgPath = ((String[])e.Data.GetData(DataFormats.FileDrop))[0];
            try
            {
                slika = Image.FromFile(imgPath);
            }
            catch
            {
                MessageBox.Show("Doslo je do greske pri citanju slike", "Greska");
            }

            pictureBox1.Image = slika;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from tim", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "ime";
            comboBox1.ValueMember = "id";

            comboBox1_SelectedValueChanged(comboBox1, new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e) {
            SqlConnection conn = sql.vrati_vezu();
            ime = textBox1.Text;
            godina = int.Parse(textBox3.Text);
            opis = textBox2.Text;
            imgPath = ime.ToLower();
            File.Delete(imgPath + ".png");
            slika.Save(sql.imgFromResource(imgPath));
            string comm = "update tim set ime = '" + ime + "', imgPath = '"
                + imgPath + "', prvaGodina = " + godina + ", opis = '" + opis + "' " +
                "where id = " + comboBox1.SelectedValue;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = comm;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Tim uspesno izmenjen.", "Izmena tima");
            this.Close();
        }
    }
}
