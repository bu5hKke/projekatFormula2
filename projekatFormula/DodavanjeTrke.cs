using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formula
{
    public partial class DodavanjeTrke : Form
    {

        DataTable dtSofer, dtStaza;
        int brojac, maxBroj, id;
        bool dodata;
        int[] poeni = { 25, 18, 15, 12, 10, 8, 6, 4, 2, 1 };

        public DodavanjeTrke()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select imgPath from staza where id=" + comboBox1.SelectedValue, conn);
            dtStaza = new DataTable();
            adapter.Fill(dtStaza);

            pictureBox1.Image = Image.FromFile(sql.imgFromResource(dtStaza.Rows[0]["imgPath"].ToString()));
        }

        private void DodavanjeTrke_Load(object sender, EventArgs e)
        {
            SqlConnection conn = sql.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("select id, ime + ' ' + prezime Ime from osoba", conn);
            dtSofer = new DataTable();
            adapter.Fill(dtSofer);
            brojac = 1;
            textBox1.Text = brojac.ToString();
            textBox1.Enabled = false;
            comboBox2.DataSource = dtSofer;
            comboBox2.DisplayMember = "Ime";
            comboBox2.ValueMember = "id";
            maxBroj = dtSofer.Columns.Count;
            adapter = new SqlDataAdapter("select * from staza", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "ime";
            comboBox1.ValueMember = "id";
            dodata = false;
        }

        private void button1_Click(object sender, EventArgs e) {
            SqlConnection conn = sql.vrati_vezu();
            SqlCommand cmd = conn.CreateCommand();
            if ( !dodata ) {
                
                cmd.CommandText = "insert into trka values ('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', " + comboBox1.SelectedValue + ")";
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            SqlDataAdapter adapter = new SqlDataAdapter("select id from trka", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            id = (int)dt.Rows[dt.Rows.Count - 1]["id"];

            cmd.CommandText = "insert into trka_vozac values (" + id + ", " + comboBox2.SelectedValue.ToString() + ", " + (brojac <= 10 ? poeni[brojac - 1] : 0) + ", " + brojac + ")";

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            brojac++;
            int i = 0;
            while (dtSofer.Rows[i]["id"].ToString() != comboBox2.SelectedValue.ToString()) { i++; } 
            dtSofer.Rows.RemoveAt(i);

            comboBox2.DataSource = dtSofer;
            comboBox2.DisplayMember = "Ime";
            comboBox2.ValueMember = "id";

            textBox1.Text = brojac.ToString();
            if (dtSofer.Rows.Count == 0) {
                MessageBox.Show("Unos trke gotov.", "Nova trka");
                this.Close();
            }
            dodata = true;

        }
    }
}
