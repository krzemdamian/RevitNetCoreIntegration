using Clifton.Core.Pipes;
using ModelLibrary_Standard;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Worker_Core
{
    class Worker
    {
        ClientPipe _clientPipe;

        public Worker()
        {
            Console.WriteLine("Press any key to connect to server pipe.");
            _clientPipe = new ClientPipe(".", "Test", p => p.StartByteReaderAsync());
            _clientPipe.Connect();
            Console.WriteLine("Pipe connected.");
            _clientPipe.DataReceived += (sender, args) => ReceiveObject(args);

        }

        private void ReceiveObject(PipeEventArgs args)
        {
            Console.WriteLine("Recived data.");
            try
            {
                MyDataModel deserializedObject;
                using (MemoryStream stream = new MemoryStream(args.Data))
                {
                    deserializedObject = Serializer.Deserialize<MyDataModel>(stream);
                }
                Console.Write("Object successfuly received.");
                Console.Write(string.Format("Deserialized object's field value equals: {0}", deserializedObject.test));
                Console.WriteLine(string.Format("Object is now incremented to {0}" +
                    "and is sent back to server.",++deserializedObject.test));
                using (MemoryStream stream = new MemoryStream())
                {
                    Serializer.Serialize(stream, deserializedObject);
                    _clientPipe.WriteBytes(stream.ToArray());
                }
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("Exeption occured: {0}", ex.Message));
            }
        }
    }
}
