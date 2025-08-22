

using ConsoleAppADONET;

namespace ClassLibrary
{
    [SqlTable("Subject")]
    public class Subject 
    {

        [SqlColumn("INT",true,true,true,false)]
        public int Id { get; set; }

        [SqlColumn("NVARCHAR(30)",false,false,false,false)]
        public string Name { get; set; }

        [SqlColumn("NVARCHAR(60)", false, false, false, false)]
        public string Description { get; set; }

        [SqlColumn("NVARCHAR(10)", false, false, false, false)]
        public string Code { get; set; }

    }
}
