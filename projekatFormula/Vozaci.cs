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
    public partial class Vozaci : Form
    {
        DataTable vozaciDt;
        public Vozaci()
        {
            InitializeComponent();
            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select osoba.id, osoba.ime + ' ' + osoba.prezime ime, osoba.poeni, osoba.imgPath from osoba " +
                "where osoba.pos = 0 " +
                "order by poeni desc", conn);
            vozaciDt = new DataTable();
            adapter.Fill(vozaciDt);
            listBox1.DataSource = vozaciDt;
            listBox1.DisplayMember = "ime";
            listBox1.ValueMember = "id";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            labelIme.Text = vozaciDt.Rows[listBox1.SelectedIndex]["ime"].ToString();
            labelGodina1.Text = vozaciDt.Rows[listBox1.SelectedIndex]["poeni"].ToString();
            pictureBox1.Image = Image.FromFile(sql.imgFromResource(vozaciDt.Rows[listBox1.SelectedIndex]["imgPath"].ToString()));
        }

        private void listBox1_DoubleClick(object sender, EventArgs e) {
            (new vozacDetail((int)vozaciDt.Rows[listBox1.SelectedIndex]["id"])).Show();
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
