namespace SuperDamp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Career")]
    public partial class Career
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public string jobTitle { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Type { get; set; }

        [Key]
        [Column(Order = 3)]
        public string Reference { get; set; }

        [Key]
        [Column(Order = 4)]
        public string Location { get; set; }

        [Key]
        [Column(Order = 5)]
        public string Hours { get; set; }

        [Key]
        [Column(Order = 6)]
        public string roleDescription { get; set; }
    }
}
