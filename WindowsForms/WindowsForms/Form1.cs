using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=FAIZAN;Initial Catalog=Students;Integrated Security=True");
        public int StudentID;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsData();
        }

        private void GetStudentsData()
        {
          
            SqlCommand cmd = new SqlCommand("Select * from studentinfo", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            StudentsRecordGridView.DataSource = dt;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if(IsValid())
            {
                SqlCommand cmd = new SqlCommand("Insert into studentinfo values(@Name,@FatherName,@RolNumber,@Degree)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", textStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", textFatherName.Text); 
                cmd.Parameters.AddWithValue("@RolNumber", textRollNumber.Text);
                cmd.Parameters.AddWithValue("@Degree", textDegree.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Student is Successfully Saved in database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsData();
                ResetFormControls();
            }
                 


        }

        private bool IsValid()
        {
            if(textStudentName.Text == string.Empty)
            {
                MessageBox.Show("Student name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormControls();
        }

        private void ResetFormControls()
        {
            StudentID = 0;
            textStudentName.Clear();
            textRollNumber.Clear();
            textFatherName.Clear();
            textDegree.Clear();

            textStudentName.Focus();

        }

        private void StudentsRecordGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32( StudentsRecordGridView.SelectedRows[0].Cells[0].Value);
            textStudentName.Text = StudentsRecordGridView.SelectedRows[0].Cells[1].Value.ToString();
            textFatherName.Text = StudentsRecordGridView.SelectedRows[0].Cells[2].Value.ToString();
            textRollNumber.Text = StudentsRecordGridView.SelectedRows[0].Cells[3].Value.ToString();
            textDegree.Text = StudentsRecordGridView.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(StudentID>0)
            {

                SqlCommand cmd = new SqlCommand("update studentinfo set StudentName=@Name,FatherName=@FatherName,RollNUmber=@RolNumber,Degree=@Degree where StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", textStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", textFatherName.Text);
                cmd.Parameters.AddWithValue("@RolNumber", textRollNumber.Text);
                cmd.Parameters.AddWithValue("@Degree", textDegree.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information is Updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsData();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please select a student to update his information", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(StudentID>0)
            {
                SqlCommand cmd = new SqlCommand("Delete from studentinfo where StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information is deleted successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsData();
                ResetFormControls();

            }
            else
            {
                MessageBox.Show("Please select a student to delete his information", "delete?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
