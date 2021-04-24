namespace SuperDamp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
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
        public string Email { get; set; }

        [Key]
        [Column(Order = 3)]
        public string phoneNumber { get; set; }

        [Key]
        [Column(Order = 4)]
        public string customerMessage { get; set; }
    }
}
