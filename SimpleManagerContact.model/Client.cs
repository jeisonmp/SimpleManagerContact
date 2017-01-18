namespace SimpleManagerContact.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public DateTime? LastPurchase { get; set; }

        public Guid SellerId { get; set; }

        public Guid? RegionId { get; set; }

        public Guid? ClassificationId { get; set; }

        public Guid ClientId { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Occupation { get; set; }

        public virtual Classification Classification { get; set; }

        public virtual Region Region { get; set; }

        public virtual User User { get; set; }
    }
}
