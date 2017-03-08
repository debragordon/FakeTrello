using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeTrello.ReverseModels
{
    public class AspNetRoles
    {
        [Key]
        [MaxLength]
        public string Id { get; set; }

        [MaxLength]
        public string Name { get; set; }

        public ICollection<AspNetUsers> Users { get; set; }

    }
}