using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderMagForms
{
    internal static class Program
    {
        public static void Show(List<OrderDetails> od)
        {
            foreach (OrderDetails od2 in od)
            {
                Console.WriteLine(od2);
            }
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
