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
    public partial class timDetail : Form
    {
        public timDetail( int timId )
        {
            InitializeComponent();
            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from tim", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "ime";
            comboBox1.ValueMember = "id";

            comboBox1.SelectedValue = timId;
            comboBox1_SelectedValueChanged(comboBox1, new EventArgs());
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e) {
            SqlConnection conn = sql.vrati_vezu();
            String comm = "";
            SqlDataAdapter adapter = new SqlDataAdapter(comm, conn);

            comm = "select * from tim where id = " + comboBox1.SelectedValue;
            DataTable timDt = new DataTable();
            adapter = new SqlDataAdapter(comm, conn);
            adapter.Fill(timDt);

            pictureBox1.Image = Image.FromFile(sql.imgFromResource(timDt.Rows[0]["imgPath"].ToString()));
            label1.Text = timDt.Rows[0]["ime"].ToString();
            label5.Text = timDt.Rows[0]["prvaGodina"].ToString();

            comm = "select *, ime + ' ' + prezime 'punoime' from osoba where tim = " + comboBox1.SelectedValue;
            DataTable osobaDt = new DataTable();
            adapter = new SqlDataAdapter(comm, conn);
            adapter.Fill(osobaDt);


            comboBox3.DataSource = osobaDt;
            comboBox3.DisplayMember = "punoime";
            comboBox3.ValueMember = "id";
            comboBox3_SelectionChangeCommitted(comboBox3, new EventArgs());


        }


        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e) {
            SqlConnection conn = sql.vrati_vezu();
            String comm = "select * from osoba where id = " + comboBox3.SelectedValue.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter(comm, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            pictureBox2.ImageLocation = sql.imgFromResource(dt.Rows[0]["imgPath"].ToString());
            pictureBox2.Load();
            label4.Text = dt.Rows[0]["pos"].ToString() == "1" ? "direktor" : "sofer";
            label8.Text = dt.Rows[0]["poeni"].ToString();
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
