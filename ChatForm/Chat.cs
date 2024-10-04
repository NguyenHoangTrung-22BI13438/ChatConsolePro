using ChatBusinessLogic.BusinessLogic;
using ChatBusinessLogic.Model;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChatForm
{
    public partial class Chat : Form
    {
        private int _UserID;
        private int _RelationshipID;
        public Chat(int UserID)
        {
            InitializeComponent();
            ContactListView(UserID);
            AccountInfoModel info = new AccountInfoModel();
            info.UserID = UserID;
            _UserID = UserID;
            AccountInfoBll account = new AccountInfoBll();
            var ct = account.AccountInfo(info);
            textBox3.Text = ct.Name;
            listView1.MouseClick += ListView1_MouseClick;
        }
        public void AddItem1(string friend_name, string avatar, string last_message, int ID)
        {
            ListViewGroup contactGroup = new ListViewGroup(friend_name, HorizontalAlignment.Left);
            listView1.Groups.Add(contactGroup);
            ListViewItem item1 = new ListViewItem(avatar);
            item1.SubItems.Add(last_message);
            item1.Group = contactGroup;
            item1.Tag = ID;
            listView1.Items.Add(item1);
        }

        public void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            listView2.Items.Clear();

            ListViewHitTestInfo info = listView1.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;

            if (item != null && item.Group != null)
            {
                ListViewGroup group = item.Group;
                string groupName = group.Header;
                textBox2.Text = groupName;
                int ID = (int)item.Tag;
                ListMessageModel ListMessage = new ListMessageModel();
                ListMessage.RelationshipID = ID;
                _RelationshipID = ID;
                ListMessageBll message = new ListMessageBll();
                var ms = message.ListMessage(ListMessage);
                for (int i = 0; i < ms.Count; i++)
                {
                    AddItem2(ms.Status[i], ms.Content[i], ms.AuthorID[i], _UserID);
                }
            }
        }

        void AddItem2(int status, string content, int author_id, int ID)
        {
            if (author_id == ID)
            {
                ListViewItem item = new ListViewItem("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add(content);
                item.SubItems.Add(status.ToString());
                listView2.Items.Add(item);
            }
            else
            {
                ListViewItem item = new ListViewItem(status.ToString());
                item.SubItems.Add(content);
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                listView2.Items.Add(item);
            }
        }



        void ContactListView(int ID)
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("", 350, HorizontalAlignment.Left);
            FriendModel friend = new FriendModel();
            friend.UserID = ID;
            FriendsBll contact = new FriendsBll();
            var ct = contact.Friend(friend);
            for (int i = 0; i < ct.Count; i++)
            {
                AddItem1(ct.Name[i], ct.Avatar[i], ct.LastMessage[i], ct.RelationshipID[i]);
            }

            listView2.View = View.Details;
            listView2.FullRowSelect = true;
            listView2.GridLines = true;
            listView2.Columns.Add("", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("", 400, HorizontalAlignment.Left);
            listView2.Columns.Add("", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("", 400, HorizontalAlignment.Left);
            listView2.Columns.Add("", 60, HorizontalAlignment.Left);

            // màn hình khởi tạo

            ListMessageModel ListMessage = new ListMessageModel();
            textBox2.Text = ct.Name[0];
            ListMessage.RelationshipID = ct.RelationshipID[0];
            _UserID = ID;
            ListMessageBll message = new ListMessageBll();
            var ms = message.ListMessage(ListMessage);
            for (int i = 0; i < ms.Count; i++)
            {
                AddItem2(ms.Status[i], ms.Content[i], ms.AuthorID[i], _UserID);
            }

        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                AddItem2(0, textBox1.Text, _UserID, _UserID);
                AddMessageModel addModel = new AddMessageModel()
                {
                    RelationshipID = _RelationshipID,
                    Content = textBox1.Text,
                    AuthorID = _UserID,
                };
                AddMessageBll add = new AddMessageBll();
                add.AddMessage(addModel);
                textBox1.Text = "";
            }

            // reload
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("", 350, HorizontalAlignment.Left);
            FriendModel friend = new FriendModel();
            friend.UserID = _UserID;
            FriendsBll contact = new FriendsBll();
            var ct = contact.Friend(friend);
            for (int i = 0; i < ct.Count; i++)
            {
                AddItem1(ct.Name[i], ct.Avatar[i], ct.LastMessage[i], ct.RelationshipID[i]);
            }
        }
    }
}