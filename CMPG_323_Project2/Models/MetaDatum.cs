using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CMPG_323_Project2.Models
{
    public partial class MetaDatum
    {
        [Key]
        [Column("Metadata_ID")]
        public int MetadataId { get; set; }
        [StringLength(50)]
        public string Geolocation { get; set; }
        [StringLength(50)]
        public string Tags { get; set; }
        [Column("Captured_Date", TypeName = "date")]
        public DateTime? CapturedDate { get; set; }
        [Column("Captured_By")]
        [StringLength(50)]
        public string CapturedBy { get; set; }
        [Column("Photo_ID")]
        public int? PhotoId { get; set; }

        [ForeignKey(nameof(PhotoId))]
        [InverseProperty("MetaData")]
        public virtual Photo Photo { get; set; }
    }
}
