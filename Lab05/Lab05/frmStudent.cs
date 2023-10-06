using Lab05.BUS;
using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lab05
{
    public partial class frmStudent : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private string avatarPath = string.Empty;
        public frmStudent()
        {
            InitializeComponent();
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            try
            {
                setGridViewStyle(dgvStudent);
                var listFacultys = facultyService.GetAll();
                var listStudents = studentService.GetAll();
                FillFalcultyCombobox(listFacultys);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {

            }
        }

        private void BindGrid(List<Student> listStudents)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in listStudents)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;
                if (item.Faculty != null)
                    dgvStudent.Rows[index].Cells[2].Value =
                    item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore + "";
                if (item.MajorID != null)
                    dgvStudent.Rows[index].Cells[4].Value = item.Major.Name + "";
            }
        }

        private void ShowAvatar(string ImageName)
        {
            if (string.IsNullOrEmpty(ImageName))
            {
                picAvatar.Image = null;
            }
            else
            {
                string parentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string imagePath = Path.Combine(parentDirectory, "Images", ImageName);
                picAvatar.Image = Image.FromFile(imagePath);
                picAvatar.Refresh();
            }
        }

        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
            this.cmbFaculty.DataSource = listFacultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
            this.cmbFaculty.SelectedIndex = 0;
        }

        private void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle =
            DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void chkUnregisterMajor_CheckedChanged(object sender, EventArgs e)
        {
            var listStudents = new List<Student>();
            if (this.chkUnregisterMajor.Checked)
                listStudents = studentService.GetAllHasNoMajor();
            else
                listStudents = studentService.GetAll();
            BindGrid(listStudents);
        }

        private void EmptyTexbox()
        {
            txtStudentID.Text = txtName.Text = txtAverageScore.Text = avatarPath = string.Empty;
        }

        private void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nhập thiếu thông tin
                if (string.IsNullOrEmpty(txtStudentID.Text) ||
                    string.IsNullOrEmpty(txtName.Text) ||
                    string.IsNullOrEmpty(txtAverageScore.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                //// Kiểm tra mã số sinh viên
                //if (txtStudentID.Text.Length != 10)
                //{
                //    MessageBox.Show("Mã số sinh viên phải có 10 chữ số");
                //    return;
                //}

                // Tạo mới sinh viên
                Student student = new Student();
                student.StudentID = txtStudentID.Text;
                student.FullName = txtName.Text;
                student.FacultyID = int.Parse(cmbFaculty.SelectedValue.ToString());
                student.AverageScore = float.Parse(txtAverageScore.Text);

                // Copy avatar vào folder Images
                if (!string.IsNullOrEmpty(avatarPath))
                {
                    string parentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                    string imageFileName = txtStudentID.Text + '_' + Path.GetRandomFileName() + Path.GetExtension(avatarPath);
                    string imagePath = Path.Combine(parentDirectory, "Images", imageFileName);
                    File.Copy(avatarPath, imagePath, true);
                    student.Avatar = imageFileName;
                }

                // Thêm xuống vào table
                studentService.InsertUpdate(student);

                // Hiển thị lại trên giao diện danh sách
                var listStudents = studentService.GetAll();
                BindGrid(listStudents);

                // Hiển thị rỗng các field
                EmptyTexbox();

                // Hiển thị thông báo
                MessageBox.Show("Thành công", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avatarPath = openFileDialog.FileName;
                    picAvatar.Image = Image.FromFile(avatarPath);
                }
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

                var result = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này không?", "Thông tin từ hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Xóa sinh viên
                    studentService.Delete(txtStudentID.Text);

                    // Hiển thị lại trên giao diện
                    var listStudents = studentService.GetAll();
                    BindGrid(listStudents);

                    // Hiển thị rỗng lại trên giao diện
                    EmptyTexbox();

                    // Hiển thị thông báo
                    MessageBox.Show("Xóa sinh viên thành công", "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToLower(), "Thông tin từ hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    txtAverageScore.Text = dgvStudent.Rows[e.RowIndex].Cells[3].Value.ToString();
                    var student = studentService.FindByID(txtStudentID.Text);
                    ShowAvatar(student.Avatar);
                    var faculty = facultyService.FindByFacultyName(dgvStudent.Rows[e.RowIndex].Cells[2].Value.ToString());
                    cmbFaculty.SelectedValue = faculty.FacultyID;
                    chkUnregisterMajor.Checked = string.IsNullOrEmpty(dgvStudent.Rows[e.RowIndex].Cells[2].Value.ToString());
                }
            }
            catch (Exception)
            {
            }
        }

        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtStudentID.Text))
                {
                    frmRegister frm = new frmRegister();
                    frm.ShowDialog();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
