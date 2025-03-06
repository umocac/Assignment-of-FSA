using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shape
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Factory fac = new Factory();

            Rectangle rect = fac.CreateShape(5, 6);
            Rectangle rect1 = fac.CreateShape(3, 9);
            Rectangle rect2 = fac.CreateShape(12, 11);
            Square sq = fac.CreateShape(6);
            Square sq1 = fac.CreateShape(9);
            Triangle trg = fac.CreateShape(5, 6, 7);
            Triangle trg1 = fac.CreateShape(3, 5, 7);
            Triangle trg2 = fac.CreateShape(12, 6, 5);
            Triangle trg3 = fac.CreateShape(3, 1, 9);
            Triangle trg4 = fac.CreateShape(2, 3, 6);

            ShowArea_shape(rect);
            ShowArea_shape(rect1);
            ShowArea_shape(rect2);
            ShowArea_shape(sq);
            ShowArea_shape(sq1);
            ShowArea_shape(trg);
            ShowArea_shape(trg1);
            ShowArea_shape(trg2);
            ShowArea_shape(trg3);
            ShowArea_shape(trg4);

            Console.ReadKey();
        }

        static void ShowArea_shape(object a)
        {
            Shape s= a as Shape;
            if (s == null)
            {
                Console.WriteLine("不是Shape的子类！");
                return;
            }
            if (s.CalcuArea() == -1)
            {
                Console.WriteLine(s.name+"是无效的图形！");
                return;
            }
            Console.WriteLine(s.name + "面积为：" + s.CalcuArea());
        }
    }
    
    interface Shape
    {
        string name { get; }
        double CalcuArea();
    }

    interface Judge
    {
        bool JudgeLegal();
    }

    class Rectangle : Shape
    {
        double llong;
        double lshort;
        public string name { get => $"相邻两边长度为{llong},{lshort}的矩形"; }
        public Rectangle(double llong, double lshot)
        {
            this.llong = llong;
            this.lshort = lshot;
        }
        public double CalcuArea()
        {
            return llong*lshort;
        }

    }

    class Square : Shape
    {
        double length;
        public string name { get => $"长度为{length}的正方形"; }
        public Square(double length)
        {
            this.length = length;
        }
        public double CalcuArea()
        {
            return length * length;
        }
    }

    class Triangle:Shape, Judge
    {
        double a_len, b_len, c_len;
        public string name { get => $"三条边为{a_len},{b_len},{c_len}的三角形"; }
        public Triangle(double a_len, double b_len, double c_len)
        {
            this.a_len = a_len;
            this.b_len = b_len;
            this.c_len = c_len;
        }
        public double CalcuArea()
        {
            if (!JudgeLegal())
            {
                return -1;
            }
            double p = (a_len + b_len + c_len) / 2;
            return Math.Sqrt(p * (p - a_len) * (p - b_len) * (p - c_len));
        }
        public bool JudgeLegal()
        {
            if (a_len + b_len <= c_len)
            {
                return false;
            }
            if (a_len + c_len <= b_len) 
            {
                return false;
            }
            if (b_len + c_len <= a_len)
            {
                return false;
            }
            return true;
        }
    }

    class Factory
    {
        public Factory() { }
        public Square CreateShape(double a)
        {
            return new Square(a);
        }
        public Rectangle CreateShape(double a, double b)
        {
            return new Rectangle(a, b);
        }
        public Triangle CreateShape(double a,double b,double c)
        {
            return new Triangle(a,b,c);
        }
    }
}
