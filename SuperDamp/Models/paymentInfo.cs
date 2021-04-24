using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuperDamp.Models
{
    public class paymentInfo
    {
        public string username { get; set; }
        
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")]
        [Key]
        public string cardNo { get; set; }
        public DateTime expireDate { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")]
        public string cvv { get; set; }
    }
}