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
    public partial class scientificCalculatorForm: Form
    {
        bool isToggled = false;
        public scientificCalculatorForm()
        {
            InitializeComponent();
        }

        private void advanceFiture_Click(object sender, EventArgs e)
        {
            isToggled = !isToggled;
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
    }
}
