using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Data.Entity
{
    [Table("SOAPSERVICE")]
    public partial class Soapservice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Soapservice()
        {
            Iiswebsitesoapservice = new HashSet<IISWebsitesoapservice>();
            Soapendpoint = new HashSet<Soapendpoint>();
        }

        [Key]
        [Column("IDSOAPSERVICE")]
        public int Idsoapservice { get; set; }

        [Required]
        [StringLength(99)]
        [Column("NAMESOAPSERVICE")]
        public string Namesoapservice { get; set; }

        [Column("VERSIONNUMBER")]
        public float? Versionnumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IISWebsitesoapservice> Iiswebsitesoapservice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Soapendpoint> Soapendpoint { get; set; }
    }
}
