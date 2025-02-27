using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tplz_matrix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\新建文件夹\vvss\tplz-matrix\matrix.txt";//"矩阵数据路径"
            string content = File.ReadAllText(path);
            Console.WriteLine(content);

            if (JudgeMatrix(content))
            {
                Console.WriteLine("这是一个托普利茨矩阵");
            }
            else
            {
                Console.WriteLine("这不是一个托普利茨矩阵");
            }

            Console.ReadKey();
        }

        static bool JudgeMatrix(string content)
        {
            List<List<string>> list = new List<List<string>>();
            int row = 0;
            content = content.Replace("\r\n", "\n");
            string[] rows=content.Split('\n');
            foreach(string erow in rows)
            {
                if (erow == "")
                {
                    continue;
                }
                list.Add(new List<string>());
                foreach (string estr in erow.Split(' '))
                {
                    if (estr != "")
                    {
                        list[row].Add(estr);
                    }
                }
                row += 1;

            }

            if (list.Count == 1 || list[0].Count == 1)
            {
                return true;
            }

            int line = 0;
            for (line = 0; line < list[0].Count; line += 1)
            {
                row = 0;
                int tl = line;
                string temp = list[row][line];
                while (row < list.Count && tl < list[0].Count)
                {
                    if (temp == list[row][tl])
                    {
                        row += 1;
                        tl += 1;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            for (row = 0; row < list.Count; row += 1)
            {
                line = 0;
                int tr = row;
                string temp = list[row][line];
                while (tr < list.Count && line < list[0].Count)
                {
                    if (temp == list[tr][line])
                    {
                        tr += 1;
                        line += 1;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
