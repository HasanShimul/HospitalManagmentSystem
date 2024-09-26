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

namespace HospitalManagmentSystem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox3.PasswordChar = '*';
            textBox4.PasswordChar = '*';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name, username, password, confirmPassword, doctorType;

            name = textBox1.Text;
            username = textBox2.Text;
            password = textBox3.Text;
            confirmPassword = textBox4.Text;
            doctorType = textBox5.Text;


            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(doctorType)) {

                MessageBox.Show("Please fill all section.");
                return;
            }

            if (password != confirmPassword) {
                MessageBox.Show("Enter Same Password.");
                return;
            }


            string connectingString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ssshi\OneDrive\Documents\HMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectingString);
            con.Open();

            SqlCommand querry = new SqlCommand("INSERT INTO DoctorRegistration(Username,Name,Password,Confirm_password,Doctor_Type) VALUES(@uname,@name,@password,@confPassword,@dType)", con);
            querry.Parameters.AddWithValue("@name" , name);
            querry.Parameters.AddWithValue("@uname" , username);
            querry.Parameters.AddWithValue("@password",password);
            querry.Parameters.AddWithValue("@confPassword", confirmPassword);
            querry.Parameters.AddWithValue("@dType", doctorType);

            querry.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Registration Completed.");
            
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            new Form1().Show();
            this.Hide();


        }

        private void button2_Click(object sender, EventArgs e)
        { 
            new Form1().Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Form5().Show();
           // patient.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.PasswordChar = '\0';
                textBox4.PasswordChar = '\0';
            }
            else {
                textBox3.PasswordChar = '*';
                textBox4.PasswordChar = '*';
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
