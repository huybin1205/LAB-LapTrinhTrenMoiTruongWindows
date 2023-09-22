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
    public partial class frmAddStudent : Form
    {
        public Student student { get; private set; }

        public frmAddStudent()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();
            string name = txtName.Text.Trim();
            double diem;

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || !double.TryParse(txtAverageScore.Text, out diem))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin và điểm phải là số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (diem < 0 || diem > 10)
            {
                MessageBox.Show("Điểm phải nằm trong khoảng từ 0 đến 10.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string faculty = cbFaculty.SelectedItem.ToString();
                student = new Student { ID = id, Name = name, AverageScore = diem, Faculty = faculty };
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void frmAddStudent_Load(object sender, EventArgs e)
        {
            List<string> faculties = new List<string>() { 
                "Công nghệ thông tin",
                "Ngôn ngữ Anh",
                "Quản trị kinh doanh"
            };

            foreach (var faculty in faculties)
            {
                cbFaculty.Items.Add(faculty);
            }

            cbFaculty.SelectedIndex = 0;
        }
    }
}
