using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SuperDamp.Models
{
    public class Order
    {
      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int orderNo { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string shipmentInfo { get; set; }
        public string paymentInfo { get; set; }
        public string orderStatus { get; set; }
        public string orderItems { get; set; }
        public DateTime orderTime { get; set; }
        public string trackingNumber { get; set; }

    }
}