using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Data.Entity
{
    [Table("LOGENTRY")]
    public class Logentry
    {
        [Key]
        [Column("IDLOGENTRY")]
        public int Idlogentry { get; set; }

        [Column("LOGTYPE")]
        public short? Logtype { get; set; }

        [Column("IDIISAPPLICATION")]
        public int? Idiisapplication { get; set; }

        [StringLength(256)]
        [Column("URLPATH")]
        public string Urlpath { get; set; }

        [StringLength(2000)]
        [Column("METHOD")]
        public string Method { get; set; }

        [StringLength(2000)]
        [Column("MESSAGE")]
        public string Message { get; set; }

        [StringLength(255)]
        [Column("HASH")]
        public string Hash { get; set; }

        [Column("APPLOGID")]
        public int Applogid { get; set; }

        [Column("CREATIONDATE")]
        public DateTime? Creationdate { get; set; }

        [Column("ENDDATE")]
        public DateTime? Enddate { get; set; }

        public virtual IISApplication Iisapplication { get; set; }
    }
}