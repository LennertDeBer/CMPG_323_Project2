using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CMPG_323_Project2.Models
{
    [Table("Album")]
    public partial class Album
    {
        public Album()
        {
            Contains = new HashSet<Contain>();
            ShareAlbums = new HashSet<ShareAlbum>();
        }

        [Key]
        [Column("Album_ID")]
        public int AlbumId { get; set; }
        [Column("Album_Name")]
        [StringLength(50)]
        public string AlbumName { get; set; }

        [InverseProperty(nameof(Contain.Album))]
        public virtual ICollection<Contain> Contains { get; set; }
        [InverseProperty(nameof(ShareAlbum.Album))]
        public virtual ICollection<ShareAlbum> ShareAlbums { get; set; }
    }
}
