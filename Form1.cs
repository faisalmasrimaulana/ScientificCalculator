using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScientificCalculator
{
    public partial class scientificCalculatorForm : Form
    {
        bool isToggled = false; //variabel untuk mengecek apakah tombol sudah ditekan atau belum
        bool isON = false; //variabel untuk mengecek apakah kalkulator dalam keadaan on atau off
        double number = 0.0;
        public scientificCalculatorForm()
        {
            InitializeComponent();
            richTextBox1.Text = "";
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void advanceFiture_Click(object sender, EventArgs e)
        {
            isToggled = !isToggled; //ketika tombol ditekan, maka nilai variabel isToggled akan berubah
            if (isToggled)
            {
                basicCalculatorButton.Visible = true;
                advanceCalculatorButton.Visible = true;
                functionCalculatorButton.Visible = true;
                formulaCalculatorButton.Visible = true;
            }
            else
            {
                basicCalculatorButton.Visible = false;
                advanceCalculatorButton.Visible = false;
                functionCalculatorButton.Visible = false;
                formulaCalculatorButton.Visible = false;
            }
        }

        private void OnOffButton_Click(object sender, EventArgs e)
        {
            //Kondisi awal, tanpa tulisan, dan teks pada tombol adalah ON
            //Ketika tombol on ditekan maka tulisannya muncul, dan teks pada tombol adalah OFF ketika tombol on ditekan kembali, maka tulisannya hilang dan teks pada tombol adalah ON
            isToggled = !isToggled;//ketika tombol ditekan, maka nilai variabel isToggled akan berubah
            if (isToggled)
            {
                OnOffButton.Text = "OFF";
                richTextBox1.Text = "0";
                isON = true;
            }
            else
            {
                OnOffButton.Text = "ON";
                richTextBox1.Text = "";
                isON = false;
            }
        }

        private void numberButton_Click(object sender, EventArgs e)
        {
            if (isON)
            {
                Button btn = sender as Button; // Ambil tombol yang diklik
                if (btn != null)
                {
                    if (richTextBox1.Text == "0")
                        richTextBox1.Text = btn.Text; // Ganti 0 dengan angka pertama
                    else
                        richTextBox1.Text += btn.Text; // Tambah angka di belakang
                }
            }
        }


        private void negativePositiveButton_Click(object sender, EventArgs e)
        {
            //ketika ada 0, maka tanda negatif tidak ditambahkan
            //ketika ada angka selain 0 maka tanda negatif akan ditambahkan
            //ketika tanda negatif sudah ditambahkan, maka tanda negatif akan dihapus
            //tanda negatif harus selalu berada di depan angka
            if (isON)
            {
                if (richTextBox1.Text != "0")
                {
                    if (richTextBox1.Text.StartsWith("-"))
                    {
                        richTextBox1.Text = richTextBox1.Text.Substring(1);
                    }
                    else
                    {
                        richTextBox1.Text = "-" + richTextBox1.Text;
                    }
                }
            }
        }

        private void commaButton_Click(object sender, EventArgs e)
        {
            //ketika 0, bisa ditambahkan komanya
            //ketika sudah ada koma, maka koma tidak bisa ditambahkan
            if (isON)
            {
                if (!richTextBox1.Text.Contains(","))
                {
                    richTextBox1.Text += ",";
                }
            }
        }
    }
}
