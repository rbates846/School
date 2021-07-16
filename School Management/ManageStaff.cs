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
            FillCombo();
            FillCombo2();
        }

        void FillCombo()
        {
            string sql = "Select * from tbl_subjects";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string Name = myreader.GetString(2);
                    comboBox2.Items.Add(Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        void FillCombo2()
        {
            string sql = "Select * from tbl_classRoom";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string RoomNo = myreader.GetString(1);
                    comboBox1.Items.Add(RoomNo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
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

        public int TeacherId;
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

                ResetFormControls();


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
            ResetFormControls();
        }

        private void ResetFormControls()
        {
            textBox4.Clear();
            comboBox2.Text = string.Empty;
            comboBox1.Text = string.Empty;

            textBox4.Focus();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TeacherId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox4.Text  = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (TeacherId > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_staff SET Name = @Name, Subject = @Subject, Class = @Class  WHERE TeacherId = @TeacherId", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", textBox4.Text);
                cmd.Parameters.AddWithValue("@Subject", comboBox2.Text);
                cmd.Parameters.AddWithValue("@Class", comboBox1.Text);

                cmd.Parameters.AddWithValue("@TeacherId", this.TeacherId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Satff Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStaffRecord();

                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Select Staff to Update", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (TeacherId > 0)
            {
                if (MessageBox.Show("Are you sure to delete?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROm tbl_staff WHERE TeacherId = @TeacherId", con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@TeacherId", this.TeacherId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                    GetStaffRecord();

                    ResetFormControls();
                }
            }
            else
            {
                MessageBox.Show("Select Staff to Delete", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ///Get the Value from Text Box

            string keyword = textBox1.Text;
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_staff WHERE Name LIKE '%" + keyword + "%' OR Subject LIKE '%" + keyword + "%' OR Class LIKE '%" + keyword + "%'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
