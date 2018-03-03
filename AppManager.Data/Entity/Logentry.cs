using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Data.Entity
{
    [Table("LOGENTRY")]
    public partial class Logentry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDLOGENTRY")]
        public int Idlogentry { get; set; }

        [Column("IDIISWEBSITE")]
        public int? Idiiswebsite { get; set; }

        [Column("LOGTYPE")]
        public short? Logtype { get; set; }

        [Column("URLPATH")]
        [StringLength(256)]
        public string Urlpath { get; set; }

        [Column("METHOD")]
        [StringLength(2000)]
        public string Method { get; set; }

        [Column("MESSAGE")]
        [StringLength(2000)]
        public string Message { get; set; }

        [Column("HASH")]
        [StringLength(255)]
        public string Hash { get; set; }

        [Column("APPLOGID")]
        public int Applogid { get; set; }

        public virtual IISWebsite IISWebsite { get; set; }
    }
}
