using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace School_Management
{
    public partial class ManageClass : Form
    {
        public ManageClass()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_classRoom values (@RoomNo, @Capacity, @Building, @SectionNo)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@RoomNo", textBox1.Text);
                cmd.Parameters.AddWithValue("@Capacity", numericUpDown1.Value);
                cmd.Parameters.AddWithValue("@Building", textBox3.Text);
                cmd.Parameters.AddWithValue("@SectionNo", textBox4.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Class Room Successfully Inserted", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetClassRecord();
            } 
            
            
        }

        private bool IsValid()
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Room Number is Required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ManageClass_Load(object sender, EventArgs e)
        {
            GetClassRecord();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-G2918RTQ\SQLEXPRESS;Initial Catalog=SchoolManagement;Integrated Security=True");

        private void GetClassRecord()
        {
            SqlCommand cmd = new SqlCommand("Select * from tbl_classRoom", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            ///Get the Value from Text Box

            string keyword = textBox5.Text;
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_classRoom WHERE RoomNo LIKE '%" + keyword + "%' OR Capacity LIKE '%" + keyword + "%' OR Building LIKE '%" + keyword + "%' OR SectionNo LIKE '%" + keyword + "%'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
