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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }
 
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username, password;

            username = textBox1.Text;
            password = textBox2.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill up");
                return;
            }

            string connectingString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ssshi\OneDrive\Documents\HMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectingString);
            con.Open();

            string query1 = "SELECT Name, Username , Password, Age, Gender FROM PatientRegistration WHERE username = @uname AND Password = @pass";

            try
            {
                SqlCommand cmd1 = new SqlCommand(query1, con);
                cmd1.Parameters.AddWithValue("@uname", username);
                cmd1.Parameters.AddWithValue("@pass", password);

                SqlDataReader readerPatient = cmd1.ExecuteReader();

                if (readerPatient.Read())
                {
                    string patientName = readerPatient["Name"].ToString();
                    string patientUserName = readerPatient["Username"].ToString();
                    string patientGender = readerPatient["Gender"].ToString();
                    int age  = int.Parse(readerPatient["Age"].ToString());
                    MessageBox.Show("Login Successful");
                    // new Form7(patientName).Show();
                    Form7 patientForm = new Form7(patientName, patientUserName, patientGender, age);
                    patientForm.Show();
                    this.Hide();
                }
                else
                {
                    readerPatient.Close();
                    string query2 = "SELECT Name,Username , Password FROM DoctorRegistration WHERE username = @uname AND Password = @pass";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@uname", username);
                    cmd2.Parameters.AddWithValue("@pass", password);

                    SqlDataReader readerDoctor = cmd2.ExecuteReader();

                    if (readerDoctor.Read())
                    {
                        string doctorName = readerDoctor["Name"].ToString();
                        string docotrusername = readerDoctor["Username"].ToString();
                        MessageBox.Show("Login Successful");
                        //new Form6(doctorName).Show();
                        Form6 doctorForm = new Form6(doctorName, docotrusername);
                        doctorForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials.");
                    }

                    readerDoctor.Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 doctorPage = new Form2();
            doctorPage.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 doctorPage = new Form2();
            doctorPage.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else {
                textBox2.PasswordChar = '*';
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
