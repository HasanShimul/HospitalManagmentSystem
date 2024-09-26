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
    public partial class Form5 : Form
    {
        string isGender;
        public Form5()
        {
            InitializeComponent();
            textBox8.PasswordChar = '*';
            textBox9.PasswordChar = '*';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 home = new Form1();
            home.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
           new  Form1().Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox8.PasswordChar = '\0';
                textBox9.PasswordChar = '\0';
            }
            else {
                textBox8.PasswordChar = '*';
                textBox9.PasswordChar = '*';
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {


            string name, username, password, confirm_pass, phone, age, gender;
            name = textBox6.Text;
            username = textBox7.Text;
            password  = textBox8.Text;
            confirm_pass = textBox9.Text;
            phone = textBox10.Text;
            age = textBox11.Text;


            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(confirm_pass) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(age))
            {

                MessageBox.Show("Please fill all section");
                return;
            }
          


            if (password != confirm_pass) {
                MessageBox.Show("Enter same password");
                return;
             }
                
                
            if (radioButton1.Checked) {
                 gender = isGender;
             }

             else if (radioButton2.Checked)
              {

                 gender = isGender;
              }
             else
             {
                  MessageBox.Show("Please Select gender.");
                  return;
             }

            

            string connectingString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ssshi\OneDrive\Documents\HMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectingString);
            con.Open();

            string query = "INSERT INTO PatientRegistration(Username,Name,Password,Confirm_password,Phone,Age,Gender) VALUES(@uname,@nam,@pass,@cpass,@phn,@age,@gender)";
            SqlCommand cmd = new SqlCommand(query,con);

            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@nam", name);
            cmd.Parameters.AddWithValue("@pass", password);
            cmd.Parameters.AddWithValue("@cpass", confirm_pass);
            cmd.Parameters.AddWithValue("@phn", phone);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@gender", gender);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Registration Completed.");

            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
           

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            

            if (radioButton1.Checked)
            {
                isGender = "Male";
            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {

            if (radioButton2.Checked) {
                isGender = "Female";
            }
        }


    }
}
