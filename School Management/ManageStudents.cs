using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace School_Management
{
    public partial class ManageStudents : Form
    {
        public ManageStudents()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void ManageStudents_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-G2918RTQ\SQLEXPRESS;Initial Catalog=SchoolManagement;Integrated Security=True");

        public int StudentId;

        private void GetStudentsRecord()
        {
            SqlCommand cmd = new SqlCommand("Select * from tbl_student", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("Insert into tbl_student values (@StudentNo, @Name, @Age)", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@StudentNo", textBox2.Text);
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Age", numericUpDown1.Value);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Student Successfully Inserted", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();

                ResetFormControls();


            }
        }

        private bool IsValid()
        {
            if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Acedemic Year And Semester is Required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetFormControls();

        }

        private void ResetFormControls()
        {
            textBox2.Clear();
            textBox1.Clear();
            numericUpDown1.Value = 0;

            textBox2.Focus();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            StudentId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            numericUpDown1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StudentId > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_student SET StudentNo = @StudentNo, Name = @Name, Age = @Age  WHERE StudentId = @StudentId", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@StudentNo", textBox2.Text);
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Age", numericUpDown1.Value);
                
                cmd.Parameters.AddWithValue("@StudentId", this.StudentId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();

                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Select Student to Update", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (StudentId > 0)
            {
                if (MessageBox.Show("Are you sure to delete?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROm tbl_student WHERE StudentId = @StudentId", con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@StudentId", this.StudentId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                    GetStudentsRecord();

                    ResetFormControls();
                }
            }
            else
            {
                MessageBox.Show("Select Student to Delete", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            ///Get the Value from Text Box

            string keyword = textBox4.Text;
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_student WHERE StudentNo LIKE '%" + keyword + "%' OR Name LIKE '%" + keyword + "%' OR Age LIKE '%" + keyword + "%'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
