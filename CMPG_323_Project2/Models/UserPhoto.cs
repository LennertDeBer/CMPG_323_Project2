using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CMPG_323_Project2.Models
{
    [Table("UserPhoto")]
    [Index(nameof(PhotoId), Name = "IX_UserPhoto_Photo_ID")]
    [Index(nameof(RecepientUserId), Name = "IX_UserPhoto_Recepient_User_ID")]
    [Index(nameof(UserId), Name = "IX_UserPhoto_User_ID")]
    public partial class UserPhoto
    {
        [Key]
        [Column("Share_ID")]
        public int ShareId { get; set; }
        [Column("User_ID")]
        public string UserId { get; set; }
        [Column("Photo_ID")]
        public int? PhotoId { get; set; }
        [Column("Recepient_User_ID")]
        public string RecepientUserId { get; set; }

        [ForeignKey(nameof(PhotoId))]
        [InverseProperty("UserPhotos")]
        public virtual Photo Photo { get; set; }
        [ForeignKey(nameof(RecepientUserId))]
        [InverseProperty(nameof(AspNetUser.UserPhotoRecepientUsers))]
        public virtual AspNetUser RecepientUser { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AspNetUser.UserPhotoUsers))]
        public virtual AspNetUser User { get; set; }
    }
}
