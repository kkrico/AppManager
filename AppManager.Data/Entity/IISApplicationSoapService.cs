using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Data.Entity
{
    [Table("IISAPPLICATIONSOAPSERVICE")]
    public partial class IISApplicationSoapService
    {
        [Key]
        [Column("IDIISAPPLICATIONSOAPSERVICE")]
        public int Idiisapplicationsoapservice { get; set; }

        [Column("IDIISAPPLICATION")]
        public int Idiisapplication { get; set; }

        [Column("IDSOAPSERVICE")]
        public int Idsoapservice { get; set; }

        public virtual IISApplication Iisapplication { get; set; }

        public virtual SoapService Soapservice { get; set; }
    }
}
