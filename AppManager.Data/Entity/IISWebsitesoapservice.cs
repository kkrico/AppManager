using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Data.Entity
{
    [Table("IISWEBSITESOAPSERVICE")]
    public partial class IISWebsitesoapservice
    {
        [Key]
        [Column("IISWEBSITESOAPSERVICE")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Iiswebsitesoapservice { get; set; }

        [Column("IDSOAPSERVICE")]
        public int Idsoapservice { get; set; }

        [Column("IDIISWEBSITE")]
        public int Idiiswebsite { get; set; }

        public virtual IISWebsite IISWebsite { get; set; }

        public virtual Soapservice Soapservice { get; set; }
    }
}
