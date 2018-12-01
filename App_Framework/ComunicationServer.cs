using Clifton.Core.Pipes;
using ModelLibrary_Standard;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App_Framework
{
    class ComunicationServer
    {
        MyDataModel _testObject;

        private static ManualResetEvent _waitHandle = new ManualResetEvent(false);
        private ServerPipe _serverPipe;
        public ComunicationServer()
        {
            _serverPipe = new ServerPipe("Test", p => p.StartByteReaderAsync());
            Console.WriteLine("Press \"t\" to send test string.");
            ConsoleKeyInfo rk = Console.ReadKey();
            if(rk.KeyChar == 't')
            {
                SendTest();
            }
        }

        public void SendTest()
        {
            _testObject = new MyDataModel
            {
                test = 1
            };
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, _testObject);
                _serverPipe.WriteBytes(stream.ToArray());
            }
            Console.WriteLine("Listening for response.");
            _serverPipe.DataReceived += (sender, args) => ListenResponse(args);
            _waitHandle.WaitOne();
        }

        private void ListenResponse(PipeEventArgs args)
        {
            using (MemoryStream stream = new MemoryStream(args.Data))
            {
                _testObject = Serializer.Deserialize<MyDataModel>(stream);
            }
            Console.WriteLine("Object successfuly received.");
            Console.WriteLine(string.Format("Deserialized object's field value equals: {0}", _testObject.test));
            _waitHandle.Set();
        }


        //public void SendTest()
        //{
        //    string testString = "testing";
        //    byte[] bytes = Encoding.ASCII.GetBytes(testString);
        //    _serverPipe.WriteBytes(bytes);
        //    Console.WriteLine("Test string sent.");
        //}
    }
}
