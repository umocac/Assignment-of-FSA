using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddForEach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GenericList<double> list = new GenericList<double>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);

            PrintList(list);
            ListInf(list);

            Console.ReadKey();
        }

        public class Node<T>{
            public Node<T> Next { get; set; }
            public T Data { get; set; }
            public Node(T d) {
                this.Data = d;
                this.Next = null;
            }
        }

        public class GenericList<T>
        {
            private Node<T> head;
            private Node<T> tail;
            public GenericList()
            {
                head = tail = null;
            }
            public Node<T> Head()
            {
                return head;
            }
            public void Add(T d)
            {
                Node<T> n = new Node<T>(d);
                if(tail == null) {
                    head = tail = n;
                }
                else
                {
                    tail.Next = n;
                    tail = n;
                }
            }
            public void Foreach(Action<T> action)
            {
                Node<T> n = head;
                while(n != null)
                {
                    action(n.Data);
                    n = n.Next;
                }
            }
        }

        public static void PrintList<T>(GenericList<T> list)
        {
            list.Foreach(n => Console.Write(n + " "));
            Console.WriteLine();
        }
        public static T ListMax<T>(GenericList<T> list) where T : IComparable<T>
        {
            if (list.Head() == null)
            {
                throw new InvalidOperationException("链表为空！");
            }
            T max = list.Head().Data;
            list.Foreach(d => { if (d.CompareTo(max) > 0) { max = d; } });
            return max;
        }
        public static T ListMin<T>(GenericList<T> list) where T : IComparable<T>
        {
            if(list.Head() == null)
            {
                throw new InvalidOperationException("链表为空！");
            }
            T min=list.Head().Data;
            list.Foreach(d => { if (d.CompareTo(min) < 0) { min = d; } });
            return min;
        }
        public static T ListSum<T>(GenericList<T> list) 
        {
            dynamic sum = default(T);
            list.Foreach(d => { sum += d; });
            return sum; 
        }

        public static void ListInf<T>(GenericList<T> list)where T: IComparable<T>
        {
            try
            {
                Console.WriteLine("链表的最大值 = " + ListMax(list));
                Console.WriteLine("链表的最小值 = " + ListMin(list));
                Console.WriteLine("链表的和 = " + ListSum(list));
            }
            catch(InvalidOperationException)
            {
                Console.WriteLine("链表为空！");
            }
        }
    }
}
