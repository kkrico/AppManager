using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Data.Entity
{
    [Table("IISWEBSITE")]
    public partial class IISWebsite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IISWebsite()
        {
            IISWebsitesoapservices = new HashSet<IISWebsitesoapservice>();
            Logentry = new HashSet<Logentry>();
        }

        [Key]
        [Column("IDIISWEBSITE")]
        public int Idiiswebsite { get; set; }

        [Column("IDAPPLICATION")]
        public int? Idapplication { get; set; }

        [Required]
        [StringLength(99)]
        [Column("NAMEWEBSITE")]
        public string Namewebsite { get; set; }

        [StringLength(256)]
        [Column("APPLOGPATH")]
        public string Applogpath { get; set; }

        public virtual Application Application { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IISWebsitesoapservice> IISWebsitesoapservices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Logentry> Logentry { get; set; }
    }
}
