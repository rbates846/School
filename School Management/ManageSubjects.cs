using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace School_Management
{
    public partial class ManageSubjects : Form
    {
        public ManageSubjects()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_subjects values (@Subject_Code, @Name, @Teacher_Incharge)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Subject_Code", textBox2.Text);
                cmd.Parameters.AddWithValue("@Name", textBox4.Text);
                cmd.Parameters.AddWithValue("@Teacher_Incharge", comboBox1.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Subjects is successfully Added", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetSubjectRecord();
            }

            

        }

        private bool IsValid()
        {
            if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Name is Reuired", "failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void ManageSubjects_Load(object sender, EventArgs e)
        {
            GetSubjectRecord();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UDVU5DR\SQLEXPRESS01;Initial Catalog=SchoolManagement;Integrated Security=True");

        private void GetSubjectRecord()
        {
            SqlCommand cmd = new SqlCommand("Select* from tbl_subjects", con);//connection
            DataTable dt = new DataTable();
           
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;

            

           
        }
    }
}
