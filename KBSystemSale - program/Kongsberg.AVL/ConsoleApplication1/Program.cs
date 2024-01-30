using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System_Post_Controller_Service;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Service1();
            s.Start();
            Console.WriteLine("odpaliłem");
            Console.ReadLine();
        }
    }
}
