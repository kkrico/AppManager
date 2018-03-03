using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Data.Entity
{
    [Table("SOAPENDPOINT")]
    public partial class Soapendpoint
    {
        [Key]
        [Column("IDSOAPENDPOINT")]
        public int Idsoapendpoint { get; set; }

        [StringLength(2000)]
        [Column("URL")]
        public string Url { get; set; }

        [Column("IDSOAPSERVICE")]
        public int? Idsoapservice { get; set; }

        public virtual Soapservice Soapservice { get; set; }
    }
}
