using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CMPG_323_Project2.Models
{
    [Table("Contain")]
    [Index(nameof(AlbumId), Name = "IX_Contain_Album_ID")]
    [Index(nameof(PhotoId), Name = "IX_Contain_Photo_ID")]
    public partial class Contain
    {
        [Key]
        [Column("Contain_ID")]
        public int ContainId { get; set; }
        [Column("Album_ID")]
        public int? AlbumId { get; set; }
        [Column("Photo_ID")]
        public int? PhotoId { get; set; }

        [ForeignKey(nameof(AlbumId))]
        [InverseProperty("Contains")]
        public virtual Album Album { get; set; }
        [ForeignKey(nameof(PhotoId))]
        [InverseProperty("Contains")]
        public virtual Photo Photo { get; set; }
    }
}
