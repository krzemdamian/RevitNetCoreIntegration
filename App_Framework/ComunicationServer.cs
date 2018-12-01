using Clifton.Core.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Framework
{
    class ComunicationServer
    {
        private ServerPipe _serverPipe;
        public ComunicationServer()
        {
            _serverPipe = new ServerPipe("Test", p => p.StartByteReaderAsync());
            Console.WriteLine("Press \"t\" to send test string");
            ConsoleKeyInfo rk = Console.ReadKey();
            if(rk.KeyChar == 't')
            {
                SendTest();
            }
        }
             

        public void SendTest()
        {
            string testString = "testing";
            byte[] bytes = Encoding.ASCII.GetBytes(testString);
            _serverPipe.WriteBytes(bytes);
            Console.WriteLine("Test string sent. Exiting...");
            Console.ReadKey();
        }
    }
}
