using FiveMModsWoofer;
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

namespace Hippo_Menu
{
    public partial class LoginMENU : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect, // x-coordinate of upper-left corner
        int nTopRect, // y-coordinate of upper-left corner
        int nRightRect, // x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse // width of ellipse
    );
        public LoginMENU()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, EventArgs e)
        {
           // NetworkManager.LoginSystemV1 System = new NetworkManager.LoginSystemV1("45.142.115.67");

            // if (System.Login(Username.Text, Password.Text, System.GetHWID()))
            // {

            //}
            if (Username.Text == "admin" && Password.Text == "admin")
            {
                this.Hide();
                new Menu().Show();
            }
               
                 
        }

        private void LoginMENU_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
    }
}

