using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppADONET
{
    [AttributeUsage(AttributeTargets.All)]
    public class ColumnAttribute : Attribute
    {

    }

    public class TableAttribute : Attribute
    {

    }
}
