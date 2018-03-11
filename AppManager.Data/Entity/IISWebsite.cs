using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AppManager.Data.Entity
{
    [Table("IISWEBSITE")]
    public class IISWebSite
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IISWebSite()
        {
            Iisapplication = new HashSet<IISApplication>();
        }

        [Key]
        [Column("IDIISWEBSITE")]
        public int Idiiswebsite { get; set; }

        [Required]
        [StringLength(99)]
        [Column("NAMEWEBSITE")]
        public string Namewebsite { get; set; }

        [StringLength(256)]
        [Column("APPPOLLNAME")]
        public string Apppollname { get; set; }

        [StringLength(256)]
        [Column("ADRESSWEBSITE")]
        public string Adresswebsite { get; set; }

        [StringLength(256)]
        [Column("IISLOGPATH")]
        public string Iislogpath { get; set; }

        [StringLength(256)]
        [Column("ALIASIISWEBSITE")]
        public string Aliasiiswebsite { get; set; }

        [Column("CREATIONDATE")]
        public DateTime? Creationdate { get; set; }

        [Column("ENDDATE")]
        public DateTime? Enddate { get; set; }

        [Column("PHYSICALPATH")]
        public string PhysicalPath { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IISApplication> Iisapplication { get; set; }

        [Column("IISWEBSITEID")]
        public int IISWebSiteId { get; set; }
    }
}