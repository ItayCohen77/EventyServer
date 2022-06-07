using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EventyServerBL.Models
{
    public partial class EventyDBContext : DbContext
    {
        public EventyDBContext()
        {
        }

        public EventyDBContext(DbContextOptions<EventyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<HouseBackyard> HouseBackyards { get; set; }
        public virtual DbSet<LikedPlace> LikedPlaces { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<PlaceType> PlaceTypes { get; set; }
        public virtual DbSet<PrivateHouse> PrivateHouses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = localhost\\SQLEXPRESS; Database=EventyDB; Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<LikedPlace>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PlaceId })
                    .HasName("PK_LikedPlaces_UserID_PlaceID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LikedPlaces)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LikedPlaces_UserID");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders_PlaceId_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders_UserId_FK");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PlaceImage1).HasDefaultValueSql("('default_pl.jpg')");

                entity.Property(e => e.PlaceImage2).HasDefaultValueSql("('default_pl.jpg')");

                entity.Property(e => e.PlaceImage3).HasDefaultValueSql("('default_pl.jpg')");

                entity.Property(e => e.PlaceImage4).HasDefaultValueSql("('default_pl.jpg')");

                entity.Property(e => e.PlaceImage5).HasDefaultValueSql("('default_pl.jpg')");

                entity.Property(e => e.PlaceImage6).HasDefaultValueSql("('default_pl.jpg')");

                entity.Property(e => e.PublishedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Place_OwnerId_FK");

                entity.HasOne(d => d.PlaceTypeNavigation)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.PlaceType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Place_PlaceType_FK");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProfileImage).HasDefaultValueSql("('default_pfp.jpg')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
