using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppManager.Data.Entity
{
    [Table("WEBSERVER")]
    public partial class Webserver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Webserver()
        {
            Application = new HashSet<Application>();
        }

        [Key]
        [Column("IDWEBSERVER")]
        public int Idwebserver { get; set; }

        [StringLength(255)]
        [Column("NAMEWEBSERVER")]
        public string Namewebserver { get; set; }

        [Column("CREATIONDATE")]
        public DateTime? Creationdate { get; set; }

        [Column("ENDDATE")]
        public DateTime? Enddate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Application> Application { get; set; }
    }
}
