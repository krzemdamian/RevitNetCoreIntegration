using Autodesk.Revit.UI;
using Clifton.Core.Pipes;
using ModelLibrary_Standard;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App_Framework
{
    class ComunicationServer : IExternalApplication
    {
        private static MyDataModel _testObject;
        public static MyDataModel TestObject
        {
            get
            {
                return _testObject;
            }
            set
            {
                _testObject = value;
            }
        }

        private static ServerPipe _serverPipe;
        public static ServerPipe ServerPipe
        {
            get
            {
                return _serverPipe;
            }
        }


        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel panel = application.CreateRibbonPanel("exercise");

            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData bData = new PushButtonData("SendObject",
               "SendObject", thisAssemblyPath, "App_Framework.Sender");

            PushButton button = panel.AddItem(bData) as PushButton;

            button.ToolTip = "Send Object";


            _serverPipe = new ServerPipe("Test", p => p.StartByteReaderAsync());

            _testObject = new MyDataModel
            {
                test = 1
            };

            ExternalEventHandler handler = new ExternalEventHandler();
            ExternalEvent exEvent = ExternalEvent.Create(handler);
            handler.ev = exEvent;

            _serverPipe.DataReceived += (s, e) => handler.DataReceived(e);



            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
