using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlantillasWindowsForm
{
    public partial class MultiColoredModernUI : Form
    {
        //Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private List<Button> buttons;
        private Form activeForm;
        
        public MultiColoredModernUI()
        {
            InitializeComponent();

            random = new Random();
            buttons = new List<Button>() { btnProducts, btnCustomer, btnNotifications, btnOrders, btnReporting, btnSetting };

            foreach (Button btn in buttons)
            {
                btn.Click += (s, e) => OpenChildForm((Button)s);
            }

            Reset();
        }

        //Methods
        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.colors.Count);

            while (index == tempIndex) index = random.Next(ThemeColor.colors.Count);

            tempIndex = index;
            string color = ThemeColor.colors[index];

            return ColorTranslator.FromHtml(color);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DesactiveButton();

                    Color color = SelectThemeColor();

                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new Font("Microsoft Sans Serif", 12.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.5);

                    ThemeColor.primaryColor = color;
                    ThemeColor.secondColor = ThemeColor.ChangeColorBrightness(color, -0.5);
                }
            }
        }

        private void DesactiveButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = Color.FromArgb(0, 150, 136);
                }
            }
        }

        private void OpenChildForm(Button btn)
        {
            if (activeForm != null) activeForm.Close();
            
            Form form;
            switch (btn.Name)
            {
                case "btnProducts":
                    form = new Forms.Products();
                    break;
                case "btnCustomer":
                    form = new Forms.Customer();
                    break;
                case "btnNotifications":
                    form = new Forms.Notifications();
                    break;
                case "btnOrders":
                    form = new Forms.Orders();
                    break;
                case "btnReporting":
                    form = new Forms.Reporting();
                    break;
                case "btnSetting":
                    form = new Forms.Setting();
                    break;
                default:
                    form = new Form();
                    break;
            }

            ActivateButton(btn);
            activeForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(form);
            this.panelDesktop.Tag = form;
            form.BringToFront();
            form.Show();
            lblTitle.Text = form.Text;
        }

        private void Reset()
        {
            DesactiveButton();
            lblTitle.Text = "HOME";
            panelTitleBar.BackColor = Color.FromArgb(0, 150, 136);
            panelLogo.BackColor = Color.FromArgb(39, 39, 58);
            currentButton = null;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (activeForm != null) activeForm.Close();
            Reset();
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnCloseChildForm_Click(object sender, EventArgs e) => Application.Exit();
        private void btnMini_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        
    }
}
