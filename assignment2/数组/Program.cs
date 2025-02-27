using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace int_array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //指定的数组↓
            double[] arr = { 1, 2, 3, 4, 5, 6, 8 };
            Console.Write("数组的最大值 = ");    Console.WriteLine(arr.Max());
            Console.Write("数组的最小值 = ");    Console.WriteLine(arr.Min());
            Console.Write("  数组的均值 = ");    Console.WriteLine((double)arr.Sum() / arr.Length);
            Console.Write("数组元素的和 = ");    Console.WriteLine(arr.Sum());
            Console.ReadKey();
        }

    }
}
