using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace School_Management
{
    public partial class ManageStaff : Form
    {
        public ManageStaff()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void ManageStaff_Load(object sender, EventArgs e)
        {
            GetStaffRecord();
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-G2918RTQ\SQLEXPRESS;Initial Catalog=SchoolManagement;Integrated Security=True");

        private void GetStaffRecord()
        {
            SqlCommand cmd = new SqlCommand("Select * from tbl_staff", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_staff values (@Name, @Subject, @Class)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", textBox4.Text);
                cmd.Parameters.AddWithValue("@Subject", comboBox2.Text);
                cmd.Parameters.AddWithValue("@Class", comboBox1.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Staff Member Successfully Inserted", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStaffRecord();


            }
        }

        private bool IsValid()
        {
            if (textBox4.Text == string.Empty)
            {
                MessageBox.Show("Name is Required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
