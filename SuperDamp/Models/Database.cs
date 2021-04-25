using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SuperDamp.Models
{
    public partial class Database : DbContext
    {
        public Database()
            : base("name=Database_SD")
        {
        }

        public virtual DbSet<Career> Careers { get; set; }
        public virtual DbSet<FAQ> FAQs { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<Cart> carts { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<paymentInfo> PaymentInfos { get; set; }
        public virtual DbSet<Order> orders { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
