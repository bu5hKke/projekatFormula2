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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Formula
{
    public partial class BrisanjeOsobe : Form
    {

        int id;
        public BrisanjeOsobe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            id = (int)comboBox1.SelectedValue;

            SqlConnection conn = sql.vrati_vezu();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.Connection = conn;

            conn.Open();
            comm.CommandText = "delete from trka_vozac where vozac = " + id;
            comm.ExecuteNonQuery();
            comm.CommandText = "delete from osoba where id = " + id;
            comm.ExecuteNonQuery();
            conn.Close();
        }

        private void BrisanjeOsobe_Load(object sender, EventArgs e)
        {
            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select osoba.id, osoba.ime + ' ' + prezime + ', ' + tim.ime punoime from osoba " +
                "join tim on tim.id = osoba.tim", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "punoime";
            comboBox1.ValueMember = "id";
            id = 0;
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
