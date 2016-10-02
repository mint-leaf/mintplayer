using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Linq;

namespace WindowsFormsApplication9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("用户名只能输入英文字符！");
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            bool check = Regex.IsMatch(textBox1.Text,"^[(a-z)|(A-Z)]{0,}$");
            if (!check)
            {
                MessageBox.Show("格式输入非法！");
                textBox1.Text = string.Empty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                string str = "server=.;database=MediaPlayer;uid=sa;pwd=aptx4869";
                DataContext datacontext = new DataContext(str);
                Table<users> Users = datacontext.GetTable<users>();
                IQueryable<string> pwd = from c in Users where c.name.Equals(textBox1.Text) select c.passwords;
                string a = string.Empty;
                if (pwd.Any())
                {
                    foreach (string c in pwd)
                    {
                        a += c;
                    }
                    if (a == textBox2.Text)
                        MessageBox.Show("登录成功");
                    Hide();
                    Form2 frm2 = new Form2();
                    frm2.Show();
                    MessageBox.Show("选择音乐文件目录");
                }
                else
                {
                    MessageBox.Show("你还没有注册 ");
                }
            }
            else
            {
                MessageBox.Show("输入不能为空");
            }
        }

        [Table(Name = "users")]
        public class users
        {
            [Column(IsPrimaryKey = true)]
            public string name;
            [Column]
            public string passwords;
        }
    }
}
