using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Formula
{
    public partial class timovi : Form
    {
        DataTable timoviDt;
        public timovi() {
            InitializeComponent();

            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select distinct tim.id, tim.ime, tim.imgPath, sum(poeni) poeni from tim" +
                " join osoba on tim.id = osoba.tim group by tim.ime, tim.id, tim.imgPath order by poeni desc", conn);
            timoviDt = new DataTable();
            adapter.Fill(timoviDt);
            listBox1.DataSource = timoviDt;
            listBox1.DisplayMember = "ime";
            listBox1.ValueMember = "id";

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            labelIme.Text = timoviDt.Rows[listBox1.SelectedIndex]["ime"].ToString();
            labelGodina1.Text = timoviDt.Rows[listBox1.SelectedIndex]["poeni"].ToString();
            pictureBox1.Image = Image.FromFile(sql.imgFromResource(timoviDt.Rows[listBox1.SelectedIndex]["imgPath"].ToString()));
        }

        private void listBox1_DoubleClick(object sender, EventArgs e) {
            (new timDetail((int)timoviDt.Rows[listBox1.SelectedIndex]["id"])).Show();
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
