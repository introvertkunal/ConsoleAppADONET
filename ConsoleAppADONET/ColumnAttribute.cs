using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppADONET
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple =true)]
    public class SqlColumnAttribute : Attribute
    {
        public string SqlType {  get; set; }

        public bool ISPrimaryKey { get; set; }

        public bool IsAutoIncremented { get; set; }

        public bool IsIndexed { get; set; }

        public bool ISNullable { get; set; }

        public SqlColumnAttribute(string sqlType, bool isPrimaryKey, bool isAutoIncremented, bool isIndexed, bool isNullable)
        {
            SqlType = sqlType;
            ISPrimaryKey = isPrimaryKey;
            IsAutoIncremented = isAutoIncremented;
            IsIndexed = isIndexed;
            ISNullable = isNullable;

        }

        
    }

    public class SqlTableAttribute : Attribute
    {
        public string TableName { get; set; }

        public SqlTableAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
