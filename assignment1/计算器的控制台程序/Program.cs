using System;

namespace calculator01
{
    internal class Program
    {
        static void Main()
        {   
            Console.WriteLine("本程序为实现+ - * /运算的计算器");
            bool icheck = true;
            float numb1 = 0, numb2 = 0 ;
            for(; icheck;)
            {
                try
                {
                    Console.WriteLine("请输入一个数字：");
                    var temp1 = Console.ReadLine();
                    numb1 = float.Parse(temp1);
                    icheck = false; 
                }
                catch 
                {
                    Console.WriteLine("输入错误!");
                }
            }

            icheck = true;
            string temp="";
            for (; icheck;)
            {
                Console.WriteLine("请输入一个支持的运算符：");
                temp = Console.ReadLine();
                switch (temp)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        icheck = false;
                        break;
                    default:
                        Console.WriteLine("输入错误！");
                        break;
                }
            }

            icheck = true;
            for (; icheck;)
            {
                try
                {
                    Console.WriteLine("请输入一个数字：");
                    var temp1 = Console.ReadLine();
                    numb2 = float.Parse(temp1);
                    icheck = false;
                    if (numb2 == 0)
                    {
                        icheck = true;
                        Console.WriteLine("输入错误！");
                    }
                }
                catch
                {
                    Console.WriteLine("输入错误!");
                }
            }

            float finaout = 0;
            switch (temp)
            {
                case "+":
                    finaout = numb1 + numb2;
                    break;
                case "-":
                    finaout = numb1 - numb2;
                    break;
                case "*":
                    finaout = numb1 * numb2;
                    break;
                case "/":
                    finaout = numb1 / numb2;
                    break;
            }
            Console.WriteLine("计算结果为：");
            Console.WriteLine(finaout);
            Console.ReadKey();
        }
    }
}
