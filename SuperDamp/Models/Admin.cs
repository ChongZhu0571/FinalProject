using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace SuperDamp.Models
{
    public class Admin
    {
        [Key]
        [Required]
        public string adminName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}