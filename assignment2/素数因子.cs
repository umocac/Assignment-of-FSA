using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//程序实现输出指定数据的所有素数因子
namespace ssyz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入一个数字");
            int innumb = 0;
            string snumb = "";
            bool incheck = true;
            for (; incheck;)
            {
                try
                {
                    snumb = Console.ReadLine();
                    innumb = int.Parse(snumb);
                    incheck = false;
                }
                catch
                {
                    Console.WriteLine("输入错误！");
                }
            }
 
            List<int>yslt= new List<int>();
            yslt = NumbJudge(innumb);

            Console.WriteLine("此数字素数因子如下：");
            foreach(int i in yslt)
            {
                Console.Write(i+" "); 

            }
            Console.ReadKey();
        }

        static List<int> NumbJudge(int innumb)
        {
            List<int> yslt = new List<int>();
            int divnum = 2;
            while (divnum < innumb)
            {
                if (innumb % divnum != 0)
                {
                    divnum += 1;
                    continue;
                }
                if (yslt.Count != 0 && yslt[yslt.Count - 1] != divnum)
                {
                    yslt.Add(divnum);
                }
                if (yslt.Count == 0)
                {
                    yslt.Add(divnum);
                }
                innumb /= divnum;
            }
            yslt.Add(innumb);
            return yslt;
        }
    }
}
