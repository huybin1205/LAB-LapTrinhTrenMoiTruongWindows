using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbFaculty.SelectedIndex = 0;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStudentID.Text == "" || txtFullName.Text == "" || txtAverageScore.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin sinh viên");

                int selectedRow = GetSelectedRow(txtStudentID.Text);
                if (selectedRow == -1) // Thêm mới
                {
                    selectedRow = dgvStudent.Rows.Add();
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }

                CountQtyMaleOrFemale();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtStudentID.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy MSSV cần xóa!");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        dgvStudent.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                }

                CountQtyMaleOrFemale();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CountQtyMaleOrFemale()
        {
            int sumMale = 0, sumFemale = 0;
            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (row.Cells[2].Value == "Nam")
                {
                    sumMale++;
                }
                else if (row.Cells[2].Value == "Nữ")
                {
                    sumFemale++;
                }
            }
            txtSumMale.Text = sumMale.ToString();
            txtSumFemale.Text = sumFemale.ToString();
        }

        private void InsertUpdate(int selectedRow)
        {
            dgvStudent.Rows[selectedRow].Cells[0].Value = txtStudentID.Text;
            dgvStudent.Rows[selectedRow].Cells[1].Value = txtFullName.Text;
            dgvStudent.Rows[selectedRow].Cells[2].Value = optFemale.Checked ? "Nữ" : "Nam";
            dgvStudent.Rows[selectedRow].Cells[3].Value = float.Parse(txtAverageScore.Text).ToString();
            dgvStudent.Rows[selectedRow].Cells[4].Value = cmbFaculty.Text;
        }

        private int GetSelectedRow(string studentID)
        {
            if (dgvStudent.Rows.Count > 1)
            {
                for (int i = 0; i < dgvStudent.Rows.Count - 1; i++)
                {
                    if (dgvStudent.Rows[i].Cells[0].Value.ToString() == studentID)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbFaculty.SelectedIndex == 0)
                {
                    optFemale.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvStudent.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvStudent.SelectedRows[0];

                    txtStudentID.Text = selectedRow.Cells[0].Value.ToString();
                    txtFullName.Text = selectedRow.Cells[1].Value.ToString();

                    if (selectedRow.Cells[0].Value.ToString() == "Nữ")
                        optFemale.Checked = true;
                    else
                        optMale.Checked = true;

                    txtAverageScore.Text = selectedRow.Cells[3].Value.ToString();

                    int selectedFaculty = cmbFaculty.Items.IndexOf(selectedRow.Cells[4].Value.ToString());
                    cmbFaculty.SelectedIndex = selectedFaculty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
