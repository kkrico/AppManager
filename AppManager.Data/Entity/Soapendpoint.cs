using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Data.Entity
{
    [Table("SOAPENDPOINT")]
    public partial class SoapEndpoint
    {
        [Key]
        [Column("IDSOAPENDPOINT")]
        public int Idsoapendpoint { get; set; }

        [StringLength(2000)]
        [Column("URL")]
        public string Url { get; set; }

        [Column("IDSOAPSERVICE")]
        public int? Idsoapservice { get; set; }

        [Column("CREATIONDATE")]
        public DateTime? Creationdate { get; set; }

        [Column("ENDDATE")]
        public DateTime? Enddate { get; set; }

        public virtual SoapService Soapservice { get; set; }
    }
}
