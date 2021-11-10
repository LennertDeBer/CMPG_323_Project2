using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CMPG_323_Project2.Models
{
    [Table("Photo")]
    public partial class Photo
    {
        public Photo()
        {
            Contains = new HashSet<Contain>();
            MetaData = new HashSet<MetaDatum>();
            UserPhotos = new HashSet<UserPhoto>();
        }

        [Key]
        [Column("Photo_ID")]
        public int PhotoId { get; set; }
        [Column("Photo_URL")]
        [StringLength(100)]
        public string PhotoUrl { get; set; }

        [InverseProperty(nameof(Contain.Photo))]
        public virtual ICollection<Contain> Contains { get; set; }
        [InverseProperty(nameof(MetaDatum.Photo))]
        public virtual ICollection<MetaDatum> MetaData { get; set; }
        [InverseProperty(nameof(UserPhoto.Photo))]
        public virtual ICollection<UserPhoto> UserPhotos { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile imageFile { get; set; }
    }
}
