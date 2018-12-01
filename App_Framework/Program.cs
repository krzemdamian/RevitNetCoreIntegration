using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Comunication server starting...");
            ComunicationServer cs = new ComunicationServer();
            Console.WriteLine("Server started");
        }
    }
}
