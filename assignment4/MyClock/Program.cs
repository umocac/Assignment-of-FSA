using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClock
{
    internal class Program
    {
        public delegate void ClockHandler(object sender, ClockEventArg arg);

        public class ClockEventArg
        {
            public int h { get; set; }
            public int m { get; set; }
            public int ah {  get; set; }
            public int am { get; set; }
        }

        public class Clock
        {
            public event ClockHandler run;
            ClockEventArg arg = new ClockEventArg();
            public Clock(int h, int m, int ah, int am)
            {
                arg.h = h;
                arg.m = m;
                arg.ah = ah;
                arg.am = am;
            }
            public void Running()
            {
                arg.h = (arg.h + arg.m / 59) % 24;
                arg.m = (arg.m + 1) % 60;
                run(this, arg);
            }
            public void GetTime(out int h, out int m)
            {
                h = arg.h;
                m = arg.m;
            }
            public bool SetTime(int h, int m)
            {
                if (h > 23 || m > 59)
                {
                    return false;
                }
                arg.h = h;
                arg.m = m;
                return true;
            }
            public bool SetAlarmT(int ah,int am)
            {
                if (ah > 23 || am > 59)
                {
                    return false;
                }
                arg.ah = ah;
                arg.am = am;
                return true;
            }
        }

        public class Form
        {
            public Clock clock1 = new Clock(0, 0, -1, -1);
            public Form()
            {
                clock1.run += new ClockHandler(Tick);
                clock1.run += ShowTime;
                clock1.run += Alarm;
            }
            void Tick(object sender, ClockEventArg arg)
            {
                Console.WriteLine("Tick.");
            }
            void ShowTime(object sender, ClockEventArg arg)
            {
                int h, m;
                clock1.GetTime(out h, out m);
                Console.WriteLine($"time = {h}:{m}");
            }
            void Alarm(object sender, ClockEventArg arg)
            {
                if (arg.m == arg.am && arg.h == arg.ah)
                {
                    Console.WriteLine("The clock is ringing.");
                }

            }
        }

        static async Task Main(string[] args)
        {
            Form form= new Form();
            form.clock1.SetAlarmT(0, 15);
            for(int i = 0; i < 16; i += 1)
            {
                form.clock1.Running();
                await Task.Delay(1000);

            }

            Console.ReadKey();
        }
    }
    
}
