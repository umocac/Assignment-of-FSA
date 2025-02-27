using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//埃氏筛法求素数
namespace aissf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> ints = new List<int>();
            for(int i = 2; i < 101; i += 1)
            {
                ints.Add(i);
            }
            for(int i = 0; i < ints.Count; i += 1)
            {
                int j = 2;
                while (ints[i] * j < 101)
                {
                    ints.Remove(ints[i] * j);
                    j += 1;
                }
            }
            InfOut(ints);
        }
        static void InfOut(List<int> ints)
        {
            Console.WriteLine("2-100的素数如下：");
            for (int i = 0; i < ints.Count; i += 1)
            {
                Console.Write(ints[i]);
                Console.Write(" ");
            }

            Console.ReadKey();
        }
    }
}
