using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Framework
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.ReadOnly)]
    class Sender : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            TaskDialog taskDialog = new TaskDialog("Object sender");
            taskDialog.CommonButtons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No;
            taskDialog.DefaultButton = TaskDialogResult.Yes;
            taskDialog.MainInstruction = "Do yo want to send object?";
            TaskDialogResult res = taskDialog.Show();
            if (res == TaskDialogResult.Yes)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    Serializer.Serialize(stream, ComunicationServer.TestObject);
                    ComunicationServer.ServerPipe.WriteBytes(stream.ToArray());
                }
            }
            
            return Result.Succeeded;
        }
    }
}
