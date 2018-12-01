using Clifton.Core.Pipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_Core
{
    class Worker
    {
        ClientPipe _clientPipe;

        public Worker()
        {
            Console.WriteLine("Press any key to connect to server pipe");
            _clientPipe = new ClientPipe(".", "Test", p => p.StartByteReaderAsync());
            _clientPipe.Connect();
            Console.WriteLine("Pipe connected");
            string test = string.Empty;
            _clientPipe.DataReceived += (sndr, args) =>
                        test = Encoding.ASCII.GetString(args.Data);
            Console.ReadKey();
            Console.WriteLine(test);
            Console.ReadKey();
        }
    }
}
