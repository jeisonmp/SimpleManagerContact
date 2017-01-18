namespace SimpleManagerContact.model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Classification> Classification { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasMany(e => e.Region)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Client)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.SellerId)
                .WillCascadeOnDelete(false);
        }
    }
}
