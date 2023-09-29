using Lab04_04.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04_04
{
    public partial class frmQuanLyDonHang : Form
    {
        ProductOrderContextDB context;
        public frmQuanLyDonHang()
        {
            InitializeComponent();
        }

        private void frmQuanLyDonHang_Load(object sender, EventArgs e)
        {
            context = new ProductOrderContextDB();
            ShowOrder();
        }

        private void ShowOrder()
        {
            var isShowAll = chkShowAll.Checked;
            var startDate = dtpStartDate.Value.Date;
            var endDate = dtpEndDate.Value.Date;

            using (ProductOrderContextDB context = new ProductOrderContextDB())
            {
                var query = from invoice in context.Invoices
                            join order in context.Orders on invoice.InvoiceNo equals order.InvoiceNo
                            join product in context.Products on order.ProductID equals product.ProductID
                            group new { invoice, order, product } by new { invoice.InvoiceNo, invoice.OrderDate, invoice.DeliveryDate } into grouped
                            select new
                            {
                                InvoiceNo = grouped.Key.InvoiceNo,
                                OrderDate = grouped.Key.OrderDate,
                                DeliveryDate = grouped.Key.DeliveryDate,
                                TotalPrice = grouped.Sum(x => x.order.Quantity * x.order.Price)
                            };
                if (!isShowAll)
                {
                    query = query.Where(invoice => invoice.DeliveryDate >= startDate && invoice.DeliveryDate <= endDate);
                }

                var invoices = query.ToList();
                dgvInvoice.Rows.Clear();
                foreach (var invoice in invoices)
                {
                    int index = dgvInvoice.Rows.Add();
                    dgvInvoice.Rows[index].Cells[0].Value = (index+1).ToString();
                    dgvInvoice.Rows[index].Cells[1].Value = invoice.InvoiceNo;
                    dgvInvoice.Rows[index].Cells[2].Value = invoice.OrderDate;
                    dgvInvoice.Rows[index].Cells[3].Value = invoice.DeliveryDate;
                    dgvInvoice.Rows[index].Cells[4].Value = invoice.TotalPrice;
                }
            }
        }

        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            ShowOrder();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            ShowOrder();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            ShowOrder();
        }
    }
}
