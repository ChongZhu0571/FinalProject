using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SuperDamp.Models
{
    public class modelAdaptor
    {
        [NotMapped]
        public User user { get; set; }
        [NotMapped]
        public Career career { get; set; }
        [NotMapped]
        public FAQ faq { get; set; }
        [NotMapped]
        public Message message { get; set; }
        [NotMapped]
        public Product product { get; set; }
        [NotMapped]
        public List<Career> careers { get; set; }
        [NotMapped]
        public List<FAQ> faqs { get; set; }
        [NotMapped]
        public List<Message> messages { get; set; }
        [NotMapped]
        public List<Product> products { get; set; }
        [NotMapped]
        public Cart cart { get; set; }
        [NotMapped]
        public List<Cart> carts { get; set; }
        [NotMapped]
        public List<UserInfo> userInfos { get; set; }
        [NotMapped]
        public List<paymentInfo> paymentInfos { get; set; }
        [NotMapped]
        public UserInfo userInfo { get; set; }
        [NotMapped]
        public paymentInfo paymentInfo { get; set; }
        [NotMapped]
        public Order order { get; set; }
        [NotMapped]
        public List<Order> orders { get; set; }

        [NotMapped]
        public bool isSelected { get; set; }

    }
}