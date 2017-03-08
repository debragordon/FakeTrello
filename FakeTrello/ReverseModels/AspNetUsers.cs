using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeTrello.ReverseModels
{
    public class AspNetUsers
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; }

        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        public Boolean EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public Boolean PhoneNumberConfirmed { get; set; }

        [Required]
        public Boolean TwoFactorEnabled { get; set; }

        public DateTime LockoutEndDateUtc { get; set; }

        [Required]
        public Boolean LockoutEnabled { get; set; }

        [Required]
        public int AccessFailedCount { get; set; }

        [MaxLength(256)]
        [Required]
        public string UserName { get; set; }

        public ICollection<AspNetRoles> Roles { get; set; }
    }
}
    //Id = c.String(nullable: false, maxLength: 128),
    //EmailConfirmed = c.Boolean(nullable: false),
    //PhoneNumberConfirmed = c.Boolean(nullable: false),
    //TwoFactorEnabled = c.Boolean(nullable: false),
    //LockoutEnabled = c.Boolean(nullable: false),
    //AccessFailedCount = c.Int(nullable: false),
    //UserName = c.String(nullable: false, maxLength: 256),