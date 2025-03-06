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
        string operation = "";
        bool isNewInput = false;
        public scientificCalculatorForm()
        {
            InitializeComponent();
            richTextBox1.Text = "";
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            richTextBox1.Enabled = false;
            this.KeyPreview = true;
            this.KeyDown += buttonNumber_KeyDown;
            this.KeyDown += buttonDelete_KeyDown;
            this.KeyDown += commaButton_KeyDown;
            this.KeyDown += operationButton_KeyDown;
            this.KeyDown += buttonEqual_KeyDown;
        }

        private void advanceFiture_Click(object sender, EventArgs e)
        {
            isToggled = !isToggled; //ketika tombol ditekan, maka nilai variabel isToggled akan berubah
            if (isToggled)
            {
                basicCalculatorButton.Visible = true;
                advanceCalculatorButton.Visible = true;
                formulaCalculatorButton.Visible = true;
            }
            else
            {
                basicCalculatorButton.Visible = false;
                advanceCalculatorButton.Visible = false;
                formulaCalculatorButton.Visible = false;
            }
            this.ActiveControl = null;
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
            this.ActiveControl = null;
        }

        private void numberButton_Click(object sender, EventArgs e)
        {
            if (isON)
            {
                Button btn = sender as Button; // Ambil tombol yang diklik
                if (btn != null)
                {
                    if (richTextBox1.Text == "0" || isNewInput)
                    {
                        // Saat input baru, kita mulai dari awal, tapi pertahankan minus kalau ada
                        richTextBox1.Text = (richTextBox1.Text.StartsWith("-") ? "-" : "") + btn.Text;
                    }
                    else
                    {
                        // Kalau bukan input baru, tambahkan angka ke akhir string
                        richTextBox1.Text += btn.Text;
                    }

                }
                isNewInput = false;
                this.ActiveControl = null;
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
                    if (richTextBox1.Text.StartsWith("-"))//jika angka sudah negatif, maka tanda negatif akan dihapus
                    {
                        richTextBox1.Text = richTextBox1.Text.Substring(1);
                    }
                    else
                    {
                        richTextBox1.Text = "-" + richTextBox1.Text;//jika angka belum negatif, maka tanda negatif akan ditambahkan
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
                if (!richTextBox1.Text.Contains("."))
                {
                    richTextBox1.Text += ".";
                }
            }
        }
        private void operationButton_Click(object sender, EventArgs e)
        {
            if (isON)
            {
                Button btn = sender as Button;
                if (btn != null)
                {
                    if (!isNewInput) // Cegah perhitungan dobel saat tombol operasi ditekan berulang
                    {
                        if (operation != "")
                        {
                            PerformCalculation();
                        }
                        number = double.Parse(richTextBox1.Text);
                    }

                    operation = btn.Text;//menyimpan operasi yang ditekan
                    isNewInput = true;//menandakan input baru
                }
            }
        }
        private void buttonEqual_Click(object sender, EventArgs e)
        {
            if (isON || operation == "")//jika kalkulator dalam keadaan ON atau tidak ada operasi yang ditekan
            {
                PerformCalculation();
                operation = "";//menghapus operasi yang disimpan
            }
        }

        private void PerformCalculation()//method untuk melakukan perhitungan
        {
            double newNumber = double.Parse(richTextBox1.Text);
            double result = 0.0;

            switch (operation)
            {
                case "+":
                    result = number + newNumber;
                    break;
                case "-":
                    result = number - newNumber;
                    break;
                case "x":
                    result = number * newNumber;
                    break;
                case "/":
                    if (newNumber != 0)
                    {
                        result = number / newNumber;
                        break;
                    }
                    else
                    {
                        richTextBox1.Text = "Error";
                        break;
                    }
                case "√x":
                    result = Math.Sqrt(newNumber);
                    break;
                case "x^2":
                    result = Math.Pow(newNumber, 2);
                    break;
                case "1/x":
                    if (newNumber != 0)
                    {
                        result = 1 / number;
                        break;
                    }
                    else
                    {
                        richTextBox1.Text = "Error";
                        break;
                    }
                case "Mod":
                    result = number % newNumber;
                    break;
            }
            richTextBox1.Text = result.ToString();
            number = result;//menyimpan hasil perhitungan
            isNewInput = true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //menghapus satu karakter terakhir
            if (isON)
            {
                if (richTextBox1.Text.Length > 1)
                {
                    richTextBox1.Text = richTextBox1.Text.Substring(0, richTextBox1.Text.Length - 1);
                    if (richTextBox1.Text == "-")
                    {
                        richTextBox1.Text = "0";
                    }
                }
                else
                {
                    richTextBox1.Text = "0";
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (isON)
            {
                number = 0.0;
                operation = "";
                isNewInput = false;
                richTextBox1.Text = "0";
            }
        }
        // Event handler untuk tombol persen

        private void buttonPercent_Click(object sender, EventArgs e)
        {
            if (!isON) return; // Pastikan kalkulator dalam keadaan ON

            double currentValue = double.Parse(richTextBox1.Text);
            double percentValue;

            if (!string.IsNullOrEmpty(operation))
            {
                // Jika ada operasi sebelumnya, maka angka kedua diubah menjadi persen dari angka pertama
                switch (operation)
                {
                    case "+":
                        percentValue = number + (number * (currentValue / 100));
                        richTextBox1.Text = percentValue.ToString();
                        operation = "";
                        break;
                    case "-":
                        percentValue = number - (number * (currentValue / 100));
                        richTextBox1.Text = percentValue.ToString();
                        operation = "";
                        break;
                    case "x":
                        percentValue = number * (number * (currentValue / 100));
                        richTextBox1.Text = percentValue.ToString();
                        operation = "";
                        break;
                    case "/":
                        percentValue = number / (currentValue / 100);
                        operation = "";
                        break;
                }
            }
            else
            {
                // Jika tidak ada operasi, angka itu sendiri dipersenkan
                percentValue = currentValue / 100;
                richTextBox1.Text = percentValue.ToString();
            }

            isNewInput = true; // Menandakan input baru setelah persen ditekan
        }

        private void buttonNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isON) return; // Pastikan kalkulator dalam keadaan ON
            if (e.KeyCode == Keys.D8 && e.Shift)
            {
                return;
            }
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                string angka = (e.KeyCode - Keys.D0).ToString();

                // Cek apakah tombol yang sesuai ditemukan di UI
                Button targetButton = this.Controls.Find("button" + angka, true).FirstOrDefault() as Button;
                if (targetButton != null)
                {
                    e.SuppressKeyPress = true; // Mencegah input default dari keyboard
                    numberButton_Click(targetButton, EventArgs.Empty);
                }
            }
            else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                string angka = (e.KeyCode - Keys.NumPad0).ToString();

                Button targetButton = this.Controls.Find("button" + angka, true).FirstOrDefault() as Button;
                if (targetButton != null)
                {
                    e.SuppressKeyPress = true; // Mencegah input default dari keyboard
                    numberButton_Click(targetButton, EventArgs.Empty);
                }
            }
        }


        private void buttonDelete_KeyDown(object sender, KeyEventArgs e)
        {
            if(!isON) return;
            if (e.KeyCode == Keys.Back)
            {
                buttonDelete_Click(sender, EventArgs.Empty);
            }
        }

        private void commaButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isON) return;
            if (e.KeyCode == Keys.Oemcomma || e.KeyCode == Keys.Decimal)
            {
                commaButton_Click(sender, EventArgs.Empty);
            }
        }

        private void operationButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isON) return;
            Button targetButton = null;

            if (e.KeyCode == Keys.Oemplus && e.Shift)
            {
                targetButton = this.Controls.Find("buttonPlus", true).FirstOrDefault() as Button;
            }
            else if (e.KeyCode == Keys.OemMinus)
            {
                targetButton = this.Controls.Find("buttonSubtract", true).FirstOrDefault() as Button;
            }
            else if (e.KeyCode == Keys.Oem2)
            {
                targetButton = this.Controls.Find("buttonDivide", true).FirstOrDefault() as Button;
            }
            else if (e.KeyCode == Keys.D8 && Control.ModifierKeys.HasFlag(Keys.Shift))
            {
                Console.WriteLine("Shift + 8 ditekan");
                targetButton = this.Controls.Find("buttonMultiply", true).FirstOrDefault() as Button;
            }
            if (targetButton != null)
            {
                e.SuppressKeyPress = true; // Mencegah input default Windows Forms
                operationButton_Click(targetButton, EventArgs.Empty);
                targetButton.PerformClick();
                this.ActiveControl = null;
            }
        }

        private void buttonEqual_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isON) return;
            if (e.KeyCode == Keys.Enter)
            {
                buttonEqual_Click(sender, EventArgs.Empty);
            }
        }
    }
}
