using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CMPG_323_Project2.Models
{
    [Table("Share_Album")]
    [Index(nameof(AlbumId), Name = "IX_Share_Album_Album_ID")]
    [Index(nameof(RecipientUserId), Name = "IX_Share_Album_Recipient_User_ID")]
    [Index(nameof(UserId), Name = "IX_Share_Album_User_ID")]
    public partial class ShareAlbum
    {
        [Key]
        [Column("Share_Album_ID")]
        public int ShareAlbumId { get; set; }
        [Column("User_ID")]
        public string UserId { get; set; }
        [Column("Album_ID")]
        public int? AlbumId { get; set; }
        [Column("Recipient_User_ID")]
        public string RecipientUserId { get; set; }
        [Column("Access_Granted")]
        public bool? AccessGranted { get; set; }

        [ForeignKey(nameof(AlbumId))]
        [InverseProperty("ShareAlbums")]
        public virtual Album Album { get; set; }
        [ForeignKey(nameof(RecipientUserId))]
        [InverseProperty(nameof(AspNetUser.ShareAlbumRecipientUsers))]
        public virtual AspNetUser RecipientUser { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AspNetUser.ShareAlbumUsers))]
        public virtual AspNetUser User { get; set; }
    }
}
