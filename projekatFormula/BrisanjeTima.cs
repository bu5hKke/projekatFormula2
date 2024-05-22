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
    public partial class BrisanjeTima : Form
    {
        int tim;
        public BrisanjeTima()
        {
            InitializeComponent();
        }

        private void BrisanjeTima_Load(object sender, EventArgs e){
            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from tim", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "ime";
            comboBox1.ValueMember = "id";
            tim = 0;
        }

        private void button1_Click(object sender, EventArgs e) {
            tim = (int)comboBox1.SelectedValue;

            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from osoba where tim = " + tim, conn);
            DataTable dt = new DataTable();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.Connection = conn;
            adapter.Fill(dt);

            conn.Open();
            foreach (DataRow dr in dt.Rows) {
                string id = dr["id"].ToString();
                comm.CommandText = "delete from trka_vozac where vozac = " + id;
                comm.ExecuteNonQuery();
                comm.CommandText = "delete from osoba where id = " + id;
                comm.ExecuteNonQuery();
            }
            comm.CommandText = "delete from tim where id = " + tim;
            comm.ExecuteNonQuery();
            conn.Close();
            
            MessageBox.Show("Tim je uspesno izbrisan.", "Brisanje tima");
            this.Close();
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
