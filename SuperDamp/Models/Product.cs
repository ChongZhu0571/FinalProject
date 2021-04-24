namespace SuperDamp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Price { get; set; }

        [Key]
        [Column(Order = 3)]
        public string imageURL { get; set; }

        [Key]
        [Column(Order = 4)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1)]
        public string Sex { get; set; }

        [Key]
        [Column(Order = 6)]
        public string Category { get; set; }
    }
}
