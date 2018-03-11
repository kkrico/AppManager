using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AppManager.Data.Entity
{
    [Table("IISAPPLICATION")]
    public class IISApplication
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IISApplication()
        {
            Iisapplicationsoapservice = new HashSet<IISApplicationSoapService>();
            Logentry = new HashSet<Logentry>();
        }

        [Key]
        [Column("IDIISAPPLICATION")]
        public int Idiisapplication { get; set; }

        [Column("IDIISWEBSITE")]
        public int? Idiiswebsite { get; set; }

        [Column("IDAPPLICATION")]
        public int? Idapplication { get; set; }

        [StringLength(256)]
        [Column("APPLOGPATH")]
        public string Applogpath { get; set; }

        [StringLength(1024)]
        [Column("PHYSICALPATH")]
        public string Physicalpath { get; set; }

        [StringLength(1024)]
        [Column("LOGICALPATH")]
        public string Logicalpath { get; set; }

        [StringLength(1024)]
        [Column("APPPOLLNAME")]
        public string Apppollname { get; set; }

        [StringLength(256)]
        [Column("IISLOGPATH")]
        public string Iislogpath { get; set; }

        [Column("CREATIONDATE")]
        public DateTime? Creationdate { get; set; }

        [Column("ENDDATE")]
        public DateTime? Enddate { get; set; }

        public virtual Application Application { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IISApplicationSoapService> Iisapplicationsoapservice { get; set; }

        public virtual IISWebSite Iiswebsite { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Logentry> Logentry { get; set; }
    }
}