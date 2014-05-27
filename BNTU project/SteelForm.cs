using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BNTU_project
{
    public partial class SteelForm : Form
    {
        Form1 mainForm;

        public SteelForm(Form1 mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
        }
    }
}
