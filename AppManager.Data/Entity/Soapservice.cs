using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AppManager.Data.Entity
{
    [Table("SOAPSERVICE")]
    public class SoapService
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SoapService()
        {
            Iisapplicationsoapservice = new HashSet<IISApplicationSoapService>();
            Soapendpoint = new HashSet<SoapEndpoint>();
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

        [Column("CREATIONDATE")]
        public DateTime? Creationdate { get; set; }

        [Column("ENDDATE")]
        public DateTime? Enddate { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IISApplicationSoapService> Iisapplicationsoapservice { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SoapEndpoint> Soapendpoint { get; set; }
    }
}