using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Viết xử lí tính tổng trong sự kiện Add_click
                float number1 = float.Parse(txtNumber1.Text);
                float number2 = float.Parse(txtNumber2.Text);
                float result = number1 + number2;
                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex) //Khi gặp bất kì lỗi nào sẽ vào catch
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            try
            {
                float number1 = float.Parse(txtNumber1.Text);
                float number2 = float.Parse(txtNumber2.Text);
                float result = number1 - number2;
                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            try
            {
                float number1 = float.Parse(txtNumber1.Text);
                float number2 = float.Parse(txtNumber2.Text);
                float result = number1 * number2;
                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            try
            {
                float number1 = float.Parse(txtNumber1.Text);
                float number2 = float.Parse(txtNumber2.Text);
                float result = number1 / number2;
                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ActiveControl = txtNumber1;
        }
    }
}
