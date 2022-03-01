using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme7HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
                string path = @"data.csv";

                Repository rep = new Repository(path);
                rep.PrintDbToConsole();
                rep.Add(new Diary());
                rep.Save("newdata.csv");

                Console.ReadKey();
        }
        
    }
}
