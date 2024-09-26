using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HospitalManagmentSystem
{
    public partial class Form6 : Form
    {
       private string doctorName;
        private string doctorUserName;

        public Form6(string doctorName, string doctorUserName)
        {

            InitializeComponent();
            label3.Text = doctorName;

            this.doctorName = doctorName;
            this.doctorUserName = doctorUserName;

            loadBookedDocotor();


        }

        private void loadBookedDocotor()
        {

            string connectingString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ssshi\OneDrive\Documents\HMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectingString);
            con.Open();

            string query = "SELECT id, patientName , gender, age FROM booked WHERE doctorUserName = @uname";

            SqlCommand cmd2 = new SqlCommand(query, con);
            cmd2.Parameters.AddWithValue("@uname", this.doctorUserName);

            SqlDataReader readerDoctor = cmd2.ExecuteReader();


            DataTable table = new DataTable();
            table.Load(readerDoctor);

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("patientName", "Patient Name");
            dataGridView1.Columns.Add("gender", "Gender");
            dataGridView1.Columns.Add("age", "Age");

            DataGridViewButtonColumn removeColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Remove",  
                Name = "remove",         
                Text = "Remove",         
                UseColumnTextForButtonValue = true 
            };

            dataGridView1.Columns.Add(removeColumn);

            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns["id"].Visible = false;

            foreach (DataRow row in table.Rows)
            {
                string patientName = row["patientName"].ToString();
                string gender = row["gender"].ToString();
                int age = int.Parse(row["age"].ToString());
                int id = int.Parse(row["id"].ToString());
                // CheckBox booked = new CheckBox();


                dataGridView1.Rows.Add(patientName, gender, age, "x", id);

            }


            con.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // retrive patient name and show

            string username, password;
           
            string connectingString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ssshi\OneDrive\Documents\HMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            SqlConnection con = new SqlConnection(connectingString);
            con.Open();

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["remove"].Index && e.RowIndex >= 0)
            {

                int id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());

                removePatientFromDatabase(id);

            }

        }
        private void removePatientFromDatabase(int id) {


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

            SqlCommand query = new SqlCommand("DELETE FROM booked where id = @id", con);
            query.Parameters.AddWithValue("@id", id);

            query.ExecuteNonQuery();
              
;

            con.Close();

            MessageBox.Show("Appointed deleted !");

            this.loadBookedDocotor(); 

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
