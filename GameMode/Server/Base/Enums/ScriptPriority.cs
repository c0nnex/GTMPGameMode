using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Server.Base
{
    public enum ScriptPriority
    {
        FIRST = 1,
        AFTERINIT = 2,
        BEFOREEND = 999,
        LAST = 1000
    }
}
