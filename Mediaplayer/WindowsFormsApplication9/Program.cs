using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Windows.Forms;
using System.Data.Linq.Mapping;
using System.IO;

namespace WindowsFormsApplication9
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }

        public static Dictionary<string, string> menu = new Dictionary<string, string>();
        public static List<List<string>> list3 = new List<List<string>>();
        public static int minu = 0;
    }

}
