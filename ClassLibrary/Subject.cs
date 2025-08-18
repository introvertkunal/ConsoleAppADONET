
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary
{
    [Table("Subject")]
    public class Subject 
    {

        [Column("INT")]
        public int Id { get; set; }

        [Column("NVARCHAR(30)")]
        public string Name { get; set; }

        [Column("NVARCHAR(60)")]
        public string Description { get; set; }

        [Column("NVARCHAR(10)")]
        public string Code { get; set; }

    }
}
