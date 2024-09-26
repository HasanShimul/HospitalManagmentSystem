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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HospitalManagmentSystem
{
    public partial class Form7 : Form
    {

        private string patientUserName;
        private string patientName;
        private string patientGender;
        private int patientAge;


        public Form7(string pName, string patientUserName, string gender, int age)
        {
            InitializeComponent();
            this.patientUserName = patientUserName;
            this.patientName = pName;
            this.patientGender = gender;
            this.patientAge = age;

            this.label4.Text = this.patientName;


        }

        private void Form7_Load(object sender, EventArgs e)
        {

           /* string connectingString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ssshi\OneDrive\Documents\HMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectingString);

            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error " + ex.Message);
                return;
            }

            string query = "SELECT Name , Doctor_type FROM DoctorRegistration";

            SqlCommand cmd = new SqlCommand(query, con);

            DataTable dt = new DataTable();

            SqlDataReader sdr = cmd.ExecuteReader();

            dt.Load(sdr);
            dataGridView1.DataSource = dt;

            con.Close();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;
            string connectingString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ssshi\OneDrive\Documents\HMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectingString);

            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error " + ex.Message);
                return;
            }

            string query = "SELECT Username, Name , Doctor_type FROM DoctorRegistration WHERE Name LIKE @name";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name","%"+Name+"%");

            SqlDataReader reader = cmd.ExecuteReader();


             DataTable table = new DataTable();
            table.Load(reader);
            //dataGridView1 dgv1 = new dataGridView1();

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            //add column
            dataGridView1.Columns.Add("DoctorName", "DoctorName");
            dataGridView1.Columns.Add("DoctorType", "DoctorType");
            dataGridView1.Columns.Add("doctorUserName", "doctorUserName");
            dataGridView1.Columns["doctorUserName"].Visible = false;

            // Create a new checkbox column
            // Check if the checkbox column already exists to avoid adding it multiple times

            if (!dataGridView1.Columns.Contains("BookChbox"))

            {

                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

                checkBoxColumn.HeaderText = "Book";

                checkBoxColumn.Name = "BookChbox";

                checkBoxColumn.Width = 50; 


                dataGridView1.Columns.Add(checkBoxColumn);

            }

            foreach (DataRow row in table.Rows)
            {
                string doctorUserName = row["Username"].ToString(); 
                string  doctorName = row["Name"].ToString();
                string doctorType = row["Doctor_type"].ToString();
                // CheckBox booked = new CheckBox();


                dataGridView1.Rows.Add(doctorName, doctorType, doctorUserName, false);

            }

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.ColumnIndex == dataGridView1.Columns["BookChbox"].Index && e.RowIndex >= 0){

                bool isChecked = (bool)dataGridView1.Rows[e.RowIndex].Cells["BookChbox"].EditedFormattedValue;
                string doctorUserName = dataGridView1.Rows[e.RowIndex].Cells["doctorUserName"].Value.ToString();
                string doctorName = dataGridView1.Rows[e.RowIndex].Cells["DoctorName"].Value.ToString();

                if (isChecked){

                    InsertDoctorUserNameToDatabase(doctorUserName, doctorName);
                }

            }
            

        }

        private void InsertDoctorUserNameToDatabase(string doctorUserName, string doctorName)
        {
            string connectingString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ssshi\OneDrive\Documents\HMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectingString);

            try
            {

                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error " + ex.Message);
            }


            SqlCommand query = new SqlCommand("INSERT INTO booked (patientName, age, gender, doctorUserName, doctorName, patientUserName) VALUES (@pname, @page, @pgender, @duser, @dname, @puser)", con);
            query.Parameters.AddWithValue("@pname", this.patientName);
            query.Parameters.AddWithValue("@page", this.patientAge);
            query.Parameters.AddWithValue("@pgender", this.patientGender);
            query.Parameters.AddWithValue("@puser", this.patientUserName);
            query.Parameters.AddWithValue("@duser", doctorUserName);
            query.Parameters.AddWithValue("@dname", doctorName);


            query.ExecuteNonQuery();

            MessageBox.Show(doctorName + " has been book by " + this.patientName);

            con.Close();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string connectingString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ssshi\OneDrive\Documents\HMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectingString);
            con.Open();

            string query = "";

            //take appointment and then doctor will isntruct then you can see your history

            con.Close();
        }

       
    }
}