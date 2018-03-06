using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Data.Entity
{
    [Table("APPLICATION")]
    public partial class Application
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Application()
        {
            Iisapplication = new HashSet<IISApplication>();
        }

        [Key]
        [Column("IDAPPLICATION")]
        public int Idapplication { get; set; }

        [Column("IDWEBSERVER")]
        public int? Idwebserver { get; set; }

        [Required]
        [StringLength(99)]
        [Column("NAMEAPPLICATION")]
        public string Nameapplication { get; set; }

        [StringLength(5)]
        [Column("INITIALSAPPLICATION")]
        public string Initialsapplication { get; set; }

        [Column("CREATIONDATE")]
        public DateTime? Creationdate { get; set; }

        [Column("ENDDATE")]
        public DateTime? Enddate { get; set; }

        public virtual Webserver Webserver { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IISApplication> Iisapplication { get; set; }
    }
}
