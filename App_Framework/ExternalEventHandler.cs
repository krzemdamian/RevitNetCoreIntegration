using Autodesk.Revit.UI;
using Clifton.Core.Pipes;
using ModelLibrary_Standard;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Framework
{
    class ExternalEventHandler : IExternalEventHandler
    {
        internal ExternalEvent ev;
        private PipeEventArgs _args;

        public void Execute(UIApplication app)
        {
            using (MemoryStream stream = new MemoryStream(_args.Data))
            {
                ComunicationServer.TestObject = Serializer.Deserialize<MyDataModel>(stream);
            }
            TaskDialog taskDialog = new TaskDialog("Received message");
            taskDialog.MainContent = string.Format("Received object {0}.", ComunicationServer.TestObject.GetType().ToString()) + Environment.NewLine
               + string.Format("Counter field in this object equals: {0}", ComunicationServer.TestObject.test.ToString());
            taskDialog.MainInstruction = "Do you want to increment value of that variable?";

            taskDialog.MainIcon = TaskDialogIcon.TaskDialogIconNone;
            taskDialog.CommonButtons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No;
            taskDialog.DefaultButton = TaskDialogResult.Yes;
            TaskDialogResult res = taskDialog.Show();
            if (res == TaskDialogResult.Yes)
            {
                ComunicationServer.TestObject.test++;
                using (MemoryStream stream = new MemoryStream())
                {
                    Serializer.Serialize(stream, ComunicationServer.TestObject);
                    ComunicationServer.ServerPipe.WriteBytes(stream.ToArray());
                }
            }
        }

        public string GetName()
        {
            return "self executor";
        }

        internal void DataReceived(PipeEventArgs args)
        {
            _args = args;
            ev.Raise();
        }
    }
}
