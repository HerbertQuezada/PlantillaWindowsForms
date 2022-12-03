using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlantillasWindowsForm.Forms
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void LoadTheme()
        {
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(Button))
                {
                    Button btn = (Button)control;
                    btn.BackColor = ThemeColor.primaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.secondColor;
                }
            }
            label4.ForeColor = ThemeColor.secondColor;
            label5.ForeColor = ThemeColor.primaryColor;
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            LoadTheme();

        }
    }
}
