using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Test;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindowsFormsApplication9
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }
        Test.Mp3 player = new Mp3();
        int num = 0;
        int control = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            btn1();
            player.play();
            if (control == 1)
            {
                Task bar1 = Task.Run(()=>barchange());
                control = 0;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button2.Visible = false;
            button2.Enabled = false;
            folderBrowserDialog1.ShowDialog();
            string path = folderBrowserDialog1.SelectedPath;
            Program.menu = List(path);
            List<string> list = Program.menu.Keys.ToList();
            Program.list3 = show(list);
            foreach (string m in Program.list3[num])
            {
                listView1.Items.Add(m);
            }
            if (label1.Text == string.Empty)
            {
                player.FileName = Program.menu.First().Value;
                label1.Text = Program.menu.First().Key;
            }
            Program.minu = player.CurrentPosition;
            label2.Text = Program.minu.ToString();
            label3.Text = minu_change(player.Duration);
            customProgressBar1.Maximum = player.Duration;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btn2();
            player.Puase();
        }

        public Dictionary<string,string> List(string a)
        {
            DirectoryInfo dicon = new DirectoryInfo(a);
            FileInfo[] files = dicon.GetFiles();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (FileInfo file in files)
            {
                dict.Add(file.Name.Replace(".mp3", ""), file.FullName);
            }
            return dict;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var a = listView1.SelectedItems;
            string vf = string.Empty;
            string df = string.Empty;
            foreach (ListViewItem sel in a)
            {
                vf += sel.Text;
            }
            var clicked = Program.menu.Where(p => p.Key.Equals(vf)).Select(b => b.Value);
            foreach (string dr in clicked)
            {
                df += dr;
            }
            Test.Mp3 Player = new Mp3();
            Player.FileName = df;
            btn1();
            label1.Text = vf;
            Player.play();
            Program.minu =Player.CurrentPosition;
            Task bar = Task.Run(()=>barchange());
            label3.Text = minu_change(Player.Duration);
        }

        public List<List<string>> show(List<string> LIST)//listview的显示
        {
            int a = LIST.Count / 7;
            int b = LIST.Count % 7;
            int c = 0;
            if (b > 0)
            {
                a += 1;
            }
            List<string[]> List1 = new List<string[]>();
            List<List<string>> List2 = new List<List<string>>();
            for (int i = 0; i < a; i++)
            {
                string[] range = new string[7];
                LIST.CopyTo(c,range,0,7);
                c += 7;
                List1.Add(range);
                if (c + 7 > LIST.Count)
                {
                    if (c  < LIST.Count)
                    {
                        string[] Range = new string[7];
                        LIST.CopyTo(c,Range,0,LIST.Count-c);
                        List1.Add(Range);
                        break;
                    }
                }
                if (c  == LIST.Count)
                    break;                        
            }
            foreach (string[] vd in List1)
            {
                string[] frd = vd;
                List2.Add(frd.ToList());
            }
            return List2;
        }

        public void btn1()
        {
            button2.Visible = true;
            button1.Visible = false;
            button1.Enabled = false;
            button2.Enabled = true;
        }

        public void btn2()
        {
            button1.Visible = true;
            button2.Visible = false;
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (num > 0 && num < Program.list3.Count)
                --num;
            listView1.Clear();
            foreach (string m in Program.list3[num])
            {
                listView1.Items.Add(m);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (num>=0 && num < Program.list3.Count-1)
                ++num;
            listView1.Clear();
            foreach (string m in Program.list3[num])
            {
                listView1.Items.Add(m);
            }
        }

        

        public void barchange()//进度条的变化及数字的变化
        {
            var timer = new System.Timers.Timer(1000) {AutoReset=true};
            timer.Elapsed += delegate 
            {
                customProgressBar1.Invoke((Action)delegate {customProgressBar1.PerformStep();});
                Program.minu ++;
                label2.Invoke((Action)delegate { label2.Text = minu_change(Program.minu); });
                if (Program.minu == customProgressBar1.Maximum)
                {
                    timer.Dispose();
                    label2.Text = minu_change(Program.minu);
                }
            };
            timer.Start();
            label1.TextChanged += delegate
            {
                timer.Dispose();
                customProgressBar1.Value = 0;
            };
            button2.MouseClick += delegate
            {
                timer.Stop();
                label2.Invoke((Action)delegate { label2.Text = minu_change(Program.minu); });
            };
            button1.Click += delegate
            {
                try
                {
                    timer.Start();
                }
                catch (Exception e)
                {
                    customProgressBar1.Invoke((Action)delegate { customProgressBar1.Value = (Program.minu) ; });
                }
            };
        }

        public string minu_change(int m)//执行数值转换
        {
            string Minu=string.Empty;
            int a = m / 60;
            int b = m % 60;
            if (b - 10 < 0)
            {
                Minu = a.ToString() + ":0" + b.ToString();
            }
            else
            {
                Minu = a.ToString() + ":" + b.ToString();
            }
            return Minu;
        }

        public int Minu_change(string n)//执行数值反转换
        {
            int num = 0;
            string[] Num=n.Split(':');
            int a = Convert.ToInt32(Num[0]) * 60;
            int b = Convert.ToInt32(Num[1]);
            num = a + b;
            return num;
        }

    }
}
