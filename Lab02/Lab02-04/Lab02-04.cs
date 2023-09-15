using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_04
{
    public partial class Form1 : Form
    {
        private int accountCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAccountNumber.Text) ||
                string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtBalance.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi");
                    return;
                }

                ListViewItem itemToUpdate = null;
                foreach (ListViewItem item in lvAccounts.Items)
                {
                    if (item.SubItems[0].Text == txtAccountNumber.Text)
                    {
                        itemToUpdate = item;
                        break;
                    }
                }

                if (itemToUpdate == null)
                {
                    // Thêm mới tài khoản
                    ListViewItem newItem = new ListViewItem(txtAccountNumber.Text);
                    newItem.SubItems.Add((++accountCount).ToString());
                    newItem.SubItems.Add(txtCustomerName.Text);
                    newItem.SubItems.Add(txtAddress.Text);
                    newItem.SubItems.Add(txtBalance.Text);
                    lvAccounts.Items.Add(newItem);
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo");
                }
                else
                {
                    // Cập nhật tài khoản
                    itemToUpdate.SubItems[2].Text = txtCustomerName.Text;
                    itemToUpdate.SubItems[3].Text = txtAddress.Text;
                    itemToUpdate.SubItems[4].Text = txtBalance.Text;
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo");
                }

                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearInputs()
        {
            txtAccountNumber.Clear();
            txtCustomerName.Clear();
            txtAddress.Clear();
            txtBalance.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvAccounts.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một tài khoản để xóa!", "Lỗi");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    lvAccounts.SelectedItems[0].Remove();
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo");
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvAccounts.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = lvAccounts.SelectedItems[0];
                    txtAccountNumber.Text = selectedItem.SubItems[0].Text;
                    txtCustomerName.Text = selectedItem.SubItems[1].Text;
                    txtAddress.Text = selectedItem.SubItems[2].Text;
                    txtBalance.Text = selectedItem.SubItems[3].Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
