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
    public partial class vozacDetail : Form
    {
        public vozacDetail( int vozacId )
        {
            InitializeComponent();
            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select *, ime + ' ' + prezime punoime from osoba where pos = 0", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "punoime";
            comboBox1.ValueMember = "id";

            comboBox1.SelectedValue = vozacId;
            comboBox1_SelectedIndexChanged(comboBox1, new EventArgs());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = sql.vrati_vezu();
            String comm = "";
            SqlDataAdapter adapter = new SqlDataAdapter(comm, conn);

            comm = "select *, ime + ' ' + prezime punoime from osoba where id = " + comboBox1.SelectedValue;
            DataTable osobaDt = new DataTable();
            adapter = new SqlDataAdapter(comm, conn);
            adapter.Fill(osobaDt);

            pictureBox1.Image = Image.FromFile(sql.imgFromResource(osobaDt.Rows[0]["imgPath"].ToString()));
            label1.Text = osobaDt.Rows[0]["punoime"].ToString();
            label5.Text = osobaDt.Rows[0]["poeni"].ToString() + " ";
            label5.Text += label5.Text.EndsWith("1 ") ? "poen" : "poena";

            comm = "select trka_vozac.mesto Mesto, staza.ime + ', ' + staza.drzava Staza, trka.datum Datum from trka_vozac " +
                "join trka on trka_vozac.trka = trka.id " +
                "join staza on trka.stazaId = staza.id " +
                "where trka_vozac.vozac = " + comboBox1.SelectedValue;
            DataTable trkaDt = new DataTable();
            adapter = new SqlDataAdapter(comm, conn);
            adapter.Fill(trkaDt);

            dataGridView1.DataSource = trkaDt;

            
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
