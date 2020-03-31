﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ChatHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WCFChat.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Хост стартовал.");
                Console.ReadLine();
            }
        }
    }
}