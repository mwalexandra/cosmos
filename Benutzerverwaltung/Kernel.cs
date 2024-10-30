using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace Benutzerverwaltung
{
    public class Kernel : Sys.Kernel
    {

        string versionString = "Version 0.1";
        DateTime momentofSTart; 

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
            momentofSTart = DateTime.Now;
        }

        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();



            Console.Write("Text typed: ");
            Console.WriteLine(input);
        }
    }
}
