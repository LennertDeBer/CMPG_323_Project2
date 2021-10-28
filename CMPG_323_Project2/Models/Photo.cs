using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CMPG_323_Project2.Models
{
    [Table("Photo")]
    public partial class Photo
    {
        public Photo()
        {
            UserPhotos = new HashSet<UserPhoto>();
        }

        [Key]
        [Column("Photo_ID")]
        public int PhotoId { get; set; }
        [Column("Photo_URL")]
        [StringLength(50)]
        public string PhotoUrl { get; set; }

        [InverseProperty(nameof(UserPhoto.Photo))]
        public virtual ICollection<UserPhoto> UserPhotos { get; set; }
    }
}
