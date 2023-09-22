using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03_03
{
    public partial class Form1 : Form
    {
        List<Student> students = new List<Student>();

        public Form1()
        {
            InitializeComponent();
        }

        private void mnNew_Click(object sender, EventArgs e)
        {
            frmAddStudent frm = new frmAddStudent();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Lấy thông tin sinh viên từ Form nhập liệu
                Student student = frm.student;

                // Kiểm tra trùng mã số sinh viên
                if (students.Any(sv => sv.ID == student.ID))
                {
                    MessageBox.Show("Mã số sinh viên đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Thêm sinh viên vào danh sách
                    students.Add(student);

                    // Hiển thị sinh viên trong DataGridView
                    dgvData.Rows.Add(dgvData.Rows.Count, student.ID, student.Name, student.Faculty, student.AverageScore);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            mnNew.PerformClick();
        }

        private void mnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keySeach = txtSearch.Text.ToLower();

            // Lọc và hiển thị sinh viên chứa tên tìm kiếm
            dgvData.Rows.Clear();
            foreach (Student student in students)
            {
                if (student.Name.ToLower().Contains(keySeach))
                {
                    dgvData.Rows.Add(dgvData.Rows.Count, student.ID, student.Name, student.Faculty, student.AverageScore);
                }
            }
        }
    }
}
