using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;
            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richText.ForeColor = fontDlg.Color;
                richText.Font = fontDlg.Font;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetDefault();
        }

        private void ResetDefault()
        {
            // Lấy toàn bộ font của hệ thống
            foreach (FontFamily font in new InstalledFontCollection().Families)
            {
                cmbFonts.Items.Add(font.Name);
            }
            // Tạo dữ liệu cho font size
            foreach (var size in new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 })
            {
                cmbSizes.Items.Add(size.ToString());
            }
            cmbFonts.SelectedItem = "Tahoma";
            cmbSizes.SelectedItem = "14";
            UpdateTextFont();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ResetDefault();
            richText.Clear();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Tập tin văn bản|*.txt;*.rtf";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                if (filePath.EndsWith(".txt"))
                {
                    richText.LoadFile(filePath, RichTextBoxStreamType.PlainText);
                }
                else if (filePath.EndsWith(".rtf"))
                {
                    richText.LoadFile(filePath, RichTextBoxStreamType.RichText);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Tập tin RTF|*.rtf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richText.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            ToggleStyle(FontStyle.Bold);
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            ToggleStyle(FontStyle.Italic);
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            ToggleStyle(FontStyle.Underline);
        }

        private void ToggleStyle(FontStyle style)
        {
            if (richText.SelectionFont != null)
            {
                FontStyle currentStyle = richText.SelectionFont.Style;

                // Kiểm tra xem style đã tồn tại trong currentStyle chưa
                if (currentStyle.HasFlag(style))
                {
                    currentStyle &= ~style; // Bỏ style nếu đã có
                }
                else
                {
                    currentStyle |= style; // Thêm style nếu chưa có
                }

                richText.SelectionFont = new Font(richText.SelectionFont, currentStyle);
            }
        }

        private void UpdateTextFont()
        {
            try
            {
                string selectedFontName = cmbFonts.SelectedItem.ToString();
                float selectedFontSize = cmbSizes.SelectedItem != null ? float.Parse(cmbSizes.SelectedItem.ToString()) : 14;
                Font selectedFont = new Font(selectedFontName, selectedFontSize);
                richText.SelectionFont = selectedFont;
            }
            catch (Exception)
            {
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            btnNew.PerformClick();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            btnSave.PerformClick();
        }

        private void cmbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextFont();
        }

        private void cmbSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextFont();
        }
    }
}
