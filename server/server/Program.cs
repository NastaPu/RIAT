﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(Console.ReadLine());
            server.Start();
        }
    }
}
