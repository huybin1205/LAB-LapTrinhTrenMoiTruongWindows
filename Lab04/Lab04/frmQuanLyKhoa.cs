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
    public partial class frmQuanLyKhoa : Form
    {
        List<Faculty> listFaculties;
        StudentContextDB context;
        public frmQuanLyKhoa()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                context = new StudentContextDB();
                // Kiểm tra nhập thiếu thông tin
                if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                // Tạo mới sinh viên
                Faculty faculty = new Faculty();
                faculty.FacultyID = int.Parse(txtID.Text);
                faculty.FacultyName = txtName.Text;
                faculty.TotalProfessor = int.Parse(txtTotalProfessor.Text);

                // Thêm xuống vào table
                context.Faculties.Add(faculty);
                // Lưu xuống database
                context.SaveChanges();

                // Hiển thị lại trên giao diện danh sách
                BindGrid();

                // Hiển thị rỗng các field
                EmptyTexbox();

                // Hiển thị thông báo
                MessageBox.Show("Thêm mới thành công", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EmptyTexbox()
        {
            txtID.Text = txtName.Text = txtTotalProfessor.Text = string.Empty;
        }

        private void frmQuanLyKhoa_Load(object sender, EventArgs e)
        {
            try
            {
                context = new StudentContextDB();
                listFaculties = context.Faculties.ToList(); // Lấy các khoa
                BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid()
        {
            listFaculties = context.Faculties.ToList();
            dgvFaculty.Rows.Clear();
            foreach (var item in listFaculties)
            {
                int index = dgvFaculty.Rows.Add();
                dgvFaculty.Rows[index].Cells[0].Value = item.FacultyID;
                dgvFaculty.Rows[index].Cells[1].Value = item.FacultyName;
                dgvFaculty.Rows[index].Cells[2].Value = item.TotalProfessor.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nhập thiếu thông tin
                if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                int facultyID = int.Parse(txtID.Text);

                // Tìm kiếm trong database
                Faculty faculty = context.Faculties.FirstOrDefault(x => x.FacultyID == facultyID);
                if (faculty == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên!", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật thông tin sinh viên
                faculty.FacultyID = int.Parse(txtID.Text);
                faculty.FacultyName = txtName.Text;
                faculty.TotalProfessor = int.Parse(txtTotalProfessor.Text.ToString());

                // Lưu xuống database
                context.SaveChanges();

                // Hiển thị lại trên giao diện danh sách
                BindGrid();

                // Hiển thị rỗng các field
                EmptyTexbox();

                // Hiển thị thông báo
                MessageBox.Show("Cập nhật thành công", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nhập thiếu thông tin
                if (string.IsNullOrEmpty(txtID.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã số!", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int facultyID = int.Parse(txtID.Text);
                // Tìm
                Faculty faculty = context.Faculties.FirstOrDefault(x => x.FacultyID == facultyID);
                if (faculty == null)
                {
                    MessageBox.Show("Không tìm thấy!", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông tin từ hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Xóa
                    context.Faculties.Remove(faculty);
                    // Lưu xuống database
                    context.SaveChanges();

                    // Hiển thị lại trên giao diện
                    BindGrid();

                    // Hiển thị rỗng các field
                    EmptyTexbox();

                    // Hiển thị thông báo
                    MessageBox.Show("Xóa thành công", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvFaculty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvFaculty.Rows.Count - 1)
                {
                    txtID.Text = dgvFaculty.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtName.Text = dgvFaculty.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtTotalProfessor.Text = dgvFaculty.Rows[e.RowIndex].Cells[2].Value.ToString();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
