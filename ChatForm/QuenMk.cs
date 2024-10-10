using ChatBusinessLogic.BusinessLogic;
using ChatBusinessLogic.Model;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatForm
{
    public partial class QuenMk : Form
    {
        public QuenMk()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else if (!(int.TryParse(textBox2.Text, out int result)))
            {
                MessageBox.Show("ID người dùng không hợp lệ!", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                QuenMkModel model = new QuenMkModel();
                model.UserName = textBox1.Text;
                model.UserId = int.Parse(textBox2.Text);
                model.PhoneNumber = textBox3.Text;
                LoginBll chat = new LoginBll();
                if (chat.Laylaimatkhau(model) != "INVALID")
                {
                    MessageBox.Show("Mật khẩu của bạn là: " + chat.Laylaimatkhau(model), "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    textBox2.Text = "";
                    textBox1.Text = "";
                    textBox3.Text = "";
                }
                else
                {
                    MessageBox.Show("Tài khoản không tồn tại hoặc sai mật khẩu!", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    textBox2.Text = "";
                    textBox1.Text = "";
                    textBox3.Text = "";
                }
            }
        }

        private void QuenMk_Load(object sender, EventArgs e)
        {

        }
    }
}
