using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formula
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();

            Username.Enter += RemoveText;
            Username.Leave += AddText;

            Password.Enter += RemoveText;
            Password.Leave += AddText;

            AddText(Username, new EventArgs());
            AddText(Password, new EventArgs());


        }

        private void login_Load(object sender, EventArgs e) {

        }

        public void RemoveText(object sender, EventArgs e) {
            TextBox myTxtbx = (TextBox)sender;
            if (myTxtbx.Text == myTxtbx.Name) {
                myTxtbx.Text = "";
                myTxtbx.ForeColor = Color.Black;
                if (myTxtbx.Name == "Password")
                    myTxtbx.UseSystemPasswordChar = true;
            }
        }

        public void AddText(object sender, EventArgs e) {
            TextBox myTxtbx = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(myTxtbx.Text)) { 
                myTxtbx.Text = myTxtbx.Name;
                myTxtbx.ForeColor = Color.Gray;
                if (myTxtbx.Name == "Password") 
                    myTxtbx.UseSystemPasswordChar = false;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            if (Username.Text == "username" && Password.Text == "password") {
                Program.log = true;
                ((Main)Application.OpenForms.OfType<Main>().First()).Focus();
                ((Main)Application.OpenForms.OfType<Main>().First()).showContext();
            }
            this.Close();
        }
    }
}
