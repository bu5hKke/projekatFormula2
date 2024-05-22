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
    public partial class IzmenaOsobe : Form
    {

        string ime, prezime, opis, imgPath;
        int pos;
        Image slika;
        DataTable dt, dtTim;

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

        private void button1_Click(object sender, EventArgs e) {
            SqlConnection conn = sql.vrati_vezu();
            ime = textBox1.Text;
            prezime = textBox3.Text;
            opis = textBox2.Text;
            imgPath = ime.ToLower();
            pos = -1;
            pos = radioButton1.Checked && !radioButton2.Checked ? 0 : 1;
            File.Delete(imgPath + ".png");
            slika.Save(sql.imgFromResource(imgPath));
            string comm = "update osoba set ime = '" + ime + "', imgPath = '"
                + imgPath + "', prezime = " + prezime + ", opis = '" + opis + "' " +
                ", pos = " + pos + "where id = " + comboBox1.SelectedValue;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = comm;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Osoba je uspesno izmenjena.", "Izmena osobe");
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = (int)comboBox1.SelectedIndex;
            textBox1.Text = dt.Rows[id]["ime"].ToString();
            textBox2.Text = dt.Rows[id]["opis"].ToString();
            textBox3.Text = dt.Rows[id]["prezime"].ToString();
            imgPath = dt.Rows[id]["imgPath"].ToString();

            imgPath = sql.imgFromResource(imgPath);
            try
            {
                slika = Image.FromFile(imgPath);
                pictureBox1.Image = slika;
            }
            catch { }
            comboBox2.SelectedValue = dt.Rows[(int)comboBox1.SelectedIndex]["tim"];
            radioButton1.Checked = (int)dt.Rows[id]["pos"] == 0;
            radioButton2.Checked = (int)dt.Rows[id]["pos"] == 1;
        }

        public IzmenaOsobe()
        {
            InitializeComponent();
        }

        private void IzmenaOsobe_Load(object sender, EventArgs e) {
            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select *, ime + ' ' + prezime punoime from osoba", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "punoime";
            comboBox1.ValueMember = "id";

            adapter.SelectCommand = new SqlCommand("select * from tim", conn);
            dtTim = new DataTable();
            adapter.Fill(dtTim);

            comboBox2.DataSource = dtTim;
            comboBox2.DisplayMember = "ime";
            comboBox2.ValueMember = "id";
            comboBox2.SelectedValue = dt.Rows[(int)comboBox1.SelectedIndex]["tim"];
        }
    }
}
