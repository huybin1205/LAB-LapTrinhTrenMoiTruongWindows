using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnChooseASeat_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackColor == Color.White)
                btn.BackColor = Color.Blue;
            else if (btn.BackColor == Color.Blue)
                btn.BackColor = Color.White;
            else if (btn.BackColor == Color.Yellow)
                MessageBox.Show("Ghế đã được bán!!");
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            List<Button> buttons = GetButtons(this);
            int money = 0;
            foreach (var btn in buttons)
            {
                if(btn.BackColor == Color.Blue)
                {
                    int numberSeat = int.Parse(btn.Text);
                    if(numberSeat > 0 && numberSeat < 6)
                    {
                        money += 30000;
                    }
                    else if (numberSeat > 5 && numberSeat < 11)
                    {
                        money += 40000;
                    }
                    else if (numberSeat > 10 && numberSeat < 16)
                    {
                        money += 50000;
                    }
                    else if (numberSeat > 15 && numberSeat < 21)
                    {
                        money += 80000;
                    }

                    btn.BackColor = Color.Yellow;
                }
            }
            txtMoney.Text = money.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            List<Button> buttons = GetButtons(this);
            foreach (var btn in buttons)
            {
                if (btn.BackColor == Color.Blue)
                    btn.BackColor = Color.White;
            }
            txtMoney.Text = "0";
        }

        private List<Button> GetButtons(Control control)
        {
            List<Button> buttons = new List<Button>();
            foreach (Control c in control.Controls)
            {
                if (c is Button && c.Name.Contains("button"))
                {
                    Button button = (Button)c;
                    buttons.Add(button);
                }

                if (c.HasChildren)
                {
                    return GetButtons(c);
                }
            }

            return buttons;
        }
    }
}
