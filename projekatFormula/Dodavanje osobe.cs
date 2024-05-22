using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formula
{
    public partial class Dodavanje_osobe : Form
    {

        string imgPath;
        int pos;
        Image slika;

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = sql.vrati_vezu();
            SqlCommand cmd = conn.CreateCommand();

            pos = -1;
            pos = radioButton1.Checked && !radioButton2.Checked ? 0 : 1;

            imgPath = (textBox1.Text + textBox3.Text).ToLower();
            slika.Save(sql.imgFromResource(imgPath));

            cmd.CommandText = "insert into osoba values (" +
                "'" + textBox1.Text + "', '" + textBox3.Text + "', '" + imgPath + "', " + comboBox1.SelectedValue + ", '" +
                textBox2.Text + "', " + pos + ", " + 0 + ")";

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public Dodavanje_osobe()
        {
            InitializeComponent();
        }

        private void Dodavanje_osobe_Load(object sender, EventArgs e) {
            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from tim", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "ime";
            comboBox1.ValueMember = "id";


        }

        private void Dodavanje_osobe_DragDrop(object sender, DragEventArgs e)
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

        private void Dodavanje_osobe_DragEnter(object sender, DragEventArgs e)
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
    }
}
