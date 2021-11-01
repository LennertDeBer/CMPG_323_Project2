using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CMPG_323_Project2.Models
{
    [Index(nameof(NormalizedEmail), Name = "EmailIndex")]
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            ShareAlbumRecipientUsers = new HashSet<ShareAlbum>();
            ShareAlbumUsers = new HashSet<ShareAlbum>();
            UserPhotoRecepientUsers = new HashSet<UserPhoto>();
            UserPhotoUsers = new HashSet<UserPhoto>();
        }

        [Key]
        public string Id { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string NormalizedUserName { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        [InverseProperty(nameof(AspNetUserClaim.User))]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        [InverseProperty(nameof(AspNetUserLogin.User))]
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        [InverseProperty(nameof(ShareAlbum.RecipientUser))]
        public virtual ICollection<ShareAlbum> ShareAlbumRecipientUsers { get; set; }
        [InverseProperty(nameof(ShareAlbum.User))]
        public virtual ICollection<ShareAlbum> ShareAlbumUsers { get; set; }
        [InverseProperty(nameof(UserPhoto.RecepientUser))]
        public virtual ICollection<UserPhoto> UserPhotoRecepientUsers { get; set; }
        [InverseProperty(nameof(UserPhoto.User))]
        public virtual ICollection<UserPhoto> UserPhotoUsers { get; set; }
    }
}
