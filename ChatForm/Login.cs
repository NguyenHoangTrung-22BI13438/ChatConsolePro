using ChatBusinessLogic.BusinessLogic;
using ChatBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChatForm
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuenMk a = new QuenMk();
            a.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                LoginModel model = new LoginModel();
                model.UserName = textBox1.Text;
                model.PassWord = textBox2.Text;
                LoginBll chat = new LoginBll();
                var rs = chat.Login(model);
                if (rs.IsSuccess)
                {
                    Chat a = new Chat(rs.UserID);
                    a.ShowDialog();
                    textBox2.Text = "";
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không tồn tại!", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    textBox2.Text = "";
                    textBox1.Text = "";
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
