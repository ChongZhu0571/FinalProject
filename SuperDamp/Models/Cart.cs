using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SuperDamp.Models
{
    public class Cart
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Column(Order = 1)]
        public string username { get; set; }
        [Column(Order = 2)]
        public int productId { get; set; }
        [Column(Order = 3)]
        public int quantity { get; set; }
        [NotMapped]
        public Product product { get; set; }
        [NotMapped]
        public bool isSelected { get; set; }

        public override string ToString()
        {
            string output = productId + "  " + isSelected;
            return output;
        }


    }
}