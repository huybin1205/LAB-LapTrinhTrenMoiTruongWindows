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
    public partial class frmQuanLySinhVien : Form
    {
        List<Student> listStudent;
        List<Faculty> listFaculties;
        StudentContextDB context;

        public frmQuanLySinhVien()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLab04_Load(object sender, EventArgs e)
        {
            try
            {
                context = new StudentContextDB();
                listStudent = context.Students.ToList(); // Lấy sinh viên
                listFaculties = context.Faculties.ToList(); // Lấy các khoa
                FillFalcultyCombobox(listFaculties);
                BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Hàm binding gridView từ list sinh viên
        /// </summary>
        /// <param name="listStudent"></param>
        private void BindGrid()
        {
            listStudent = context.Students.ToList(); // Lấy sinh viên
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

        /// <summary>
        /// Hàm binding list có tên hiện thị là tên khoa, giá trị là Mã khoa
        /// </summary>
        /// <param name="listFalcultys"></param>
        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            cmbFaculty.DataSource = listFalcultys;
            cmbFaculty.DisplayMember = "FacultyName";
            cmbFaculty.ValueMember = "FacultyID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                context = new StudentContextDB();
                // Kiểm tra nhập thiếu thông tin
                if (string.IsNullOrEmpty(txtStudentID.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAverageScore.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                // Kiểm tra mã số sinh viên
                if(txtStudentID.Text.Length != 10)
                {
                    MessageBox.Show("Mã số sinh viên phải có 10 chữ số");
                    return;
                }

                // Tạo mới sinh viên
                Student student = new Student();
                student.StudentID = txtStudentID.Text;
                student.FullName = txtName.Text;
                student.FacultyID = int.Parse(cmbFaculty.SelectedValue.ToString());
                student.AverageScore = float.Parse(txtAverageScore.Text);

                // Thêm xuống vào table
                context.Students.Add(student);
                // Lưu xuống database
                context.SaveChanges();

                // Hiển thị lại trên giao diện danh sách
                BindGrid();

                // Hiển thị rỗng các field
                EmptyTexbox();

                // Hiển thị thông báo
                MessageBox.Show("Thêm mới thành công", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                context = new StudentContextDB();
                // Kiểm tra nhập thiếu thông tin
                if (string.IsNullOrEmpty(txtStudentID.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAverageScore.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                // Tìm sinh viên
                Student student = context.Students.FirstOrDefault(x => x.StudentID == txtStudentID.Text);
                if (student == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên!", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Cập nhật thông tin sinh viên
                student.StudentID = txtStudentID.Text;
                student.FullName = txtName.Text;
                student.FacultyID = int.Parse(cmbFaculty.SelectedValue.ToString());
                student.AverageScore = float.Parse(txtAverageScore.Text);
                
                // Lưu xuống database
                context.SaveChanges();

                // Hiển thị lại trên giao diện danh sách sinh viên
                BindGrid();

                // Hiển thị rỗng các field
                EmptyTexbox();

                // Hiển thị thông báo
                MessageBox.Show("Cập nhật sinh viên thành công", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nhập thiếu thong tin
                if (string.IsNullOrEmpty(txtStudentID.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã số sinh viên!", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tìm sinh viên
                Student student = context.Students.FirstOrDefault(x => x.StudentID == txtStudentID.Text);
                if(student == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên!", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                var result = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này không?", "Thông tin từ hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    // Xóa sinh viên
                    context.Students.Remove(student);
                    // Lưu xuống database
                    context.SaveChanges();

                    // Hiển thị lại trên giao diện
                    BindGrid();

                    // Hiển thị rỗng lại trên giao diện
                    EmptyTexbox();

                    // Hiển thị thông báo
                    MessageBox.Show("Xóa sinh viên thành công", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvStudent.Rows.Count - 1)
                {
                    txtStudentID.Text = dgvStudent.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtName.Text = dgvStudent.Rows[e.RowIndex].Cells[1].Value.ToString();
                    var faculty = listFaculties.FirstOrDefault(x => x.FacultyName == dgvStudent.Rows[e.RowIndex].Cells[2].Value.ToString());

                    if (faculty != null)
                    {
                        cmbFaculty.SelectedValue = faculty.FacultyID;
                    }
                    else
                    {
                        cmbFaculty.SelectedIndex = -1; // Bỏ chọn bất kỳ giá trị nào trong ComboBox
                    }

                    txtAverageScore.Text = dgvStudent.Rows[e.RowIndex].Cells[3].Value.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        private void EmptyTexbox()
        {
            txtStudentID.Text = txtName.Text = txtAverageScore.Text = string.Empty;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuSearch_Click(object sender, EventArgs e)
        {
            try
            {
                frmTimKiemSinhVien frm = new frmTimKiemSinhVien();
                frm.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuFaculty_Click(object sender, EventArgs e)
        {
            try
            {
                frmQuanLyKhoa frm = new frmQuanLyKhoa();
                frm.ShowDialog();
                listFaculties = context.Faculties.ToList();
                FillFalcultyCombobox(listFaculties);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripFaculty_Click(object sender, EventArgs e)
        {
            mnuFaculty.PerformClick();
        }

        private void toolStripSearch_Click(object sender, EventArgs e)
        {
            mnuSearch.PerformClick();
        }
    }
}
