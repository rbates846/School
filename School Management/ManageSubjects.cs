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
                ResetFormControls();
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

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-2791U4J\SQLEXPRESS;Initial Catalog=SchoolManagement;Integrated Security=True");

        public int SubId;
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

        private void button2_Click(object sender, EventArgs e)
        {
            ResetFormControls();
        }

        private void ResetFormControls()
        {
            textBox2.Clear();
            textBox4.Clear();
            comboBox1.ResetText();

            textBox2.Focus();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SubId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SubId > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_subjects set Subject_Code = @Subject_Code, Name = @Name, Teacher_Incharge = @Teacher_Incharge WHERE SubId = @SubId", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Subject_Code", textBox2.Text);
                cmd.Parameters.AddWithValue("@Name", textBox4.Text);
                cmd.Parameters.AddWithValue("@Teacher_Incharge", comboBox1.Text);
                cmd.Parameters.AddWithValue("@SubId", this.SubId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New student details successfully saved", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetSubjectRecord();
                ResetFormControls();

            }
            else
            {
                MessageBox.Show("Please select the Subject to update", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {

            if (SubId > 0)
            {
                if (MessageBox.Show("Are you sure want to delete?", "Delete Records", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    SqlCommand cmd = new SqlCommand("DELETE from tbl_subjects  WHERE SubId = @SubId", con);
                    cmd.CommandType = CommandType.Text;
                    
                    cmd.Parameters.AddWithValue("@SubId", this.SubId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    GetSubjectRecord();
                    ResetFormControls();
                }
            }
            else
            {
                MessageBox.Show("select Subject to the delete", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
