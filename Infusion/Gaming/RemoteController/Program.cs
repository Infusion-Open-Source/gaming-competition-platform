using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RemoteController
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("LightCycles - Copyright (C) 2013 Paweł Drozdowski");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details check License.txt file.");
            Console.WriteLine("This is free software, and you are welcome to redistribute it under certain conditions; check License.txt file for the details.");

            RemoteController ctrl = new RemoteController();
            ctrl.Start();
            while (ctrl.IsProcessing)
            {
                Thread.Sleep(0);
            }

            ctrl.Stop();
        }
    }
}
