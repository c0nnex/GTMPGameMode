using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Server.Base
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportAsAttribute : System.Attribute
    {
        public readonly string ExportedRessourceName;

        public ExportAsAttribute(string name)
        {
            ExportedRessourceName = name;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ExportFunctionAttribute : System.Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Event, AllowMultiple = false)]
    public class ExportEventAttribute : System.Attribute
    {
    }
}
