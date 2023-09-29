using Lab04.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04
{
    public partial class frmTimKiemSinhVien : Form
    {
        List<Student> listStudent;
        List<Faculty> listFaculties;
        StudentContextDB context;

        public frmTimKiemSinhVien()
        {
            InitializeComponent();
        }

        private void frmTimKiemSinhVien_Load(object sender, EventArgs e)
        {
            try
            {
                context = new StudentContextDB();
                listFaculties = context.Faculties.ToList(); // Lấy các khoa
                listStudent = context.Students.ToList(); // Lấy sinh viên
                FillFalcultyCombobox(listFaculties);
                BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillFalcultyCombobox(List<Faculty> listFaculties)
        {
            cmbFaculty.DataSource = listFaculties;
            cmbFaculty.DisplayMember = "FacultyName";
            cmbFaculty.ValueMember = "FacultyID";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string studentID = txtStudentID.Text;
                string studentName = txtName.Text;
                int facultyID = -1; // Giá trị mặc định

                if (cmbFaculty.SelectedValue != null)
                {
                    facultyID = int.Parse(cmbFaculty.SelectedValue.ToString());
                }

                // Xây dựng truy vấn dựa trên các điều kiện tìm kiếm
                var query = context.Students.AsQueryable(); // Bắt đầu với tất cả sinh viên

                if (!string.IsNullOrWhiteSpace(studentID))
                {
                    query = query.Where(x => x.StudentID == studentID);
                }

                if (!string.IsNullOrWhiteSpace(studentName))
                {
                    query = query.Where(x => x.FullName == studentName);
                }

                if (facultyID != -1)
                {
                    query = query.Where(x => x.FacultyID == facultyID);
                }

                // Thực hiện truy vấn và hiển thị kết quả
                var students = query.ToList();
                BindGrid(students);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BindGrid(List<Student> students = null)
        {
            if (students == null)
                listStudent = context.Students.ToList(); // Lấy sinh viên
            else
                listStudent = students;
            dgvStudent.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;
                dgvStudent.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtName.Text = txtResult.Text = txtStudentID.Text = string.Empty;
        }
    }
}
