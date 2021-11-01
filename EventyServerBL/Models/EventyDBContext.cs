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

        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderExtra> OrderExtras { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<PlaceMedium> PlaceMedia { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ReviewsMedium> ReviewsMedia { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Yard> Yards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=EventyDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.ChatBuyers)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Chat_BuyerId_FK");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.ChatSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Chat_SellerId_FK");
            });

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.ToTable("Hall");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageText)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.WhenSent)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Message_ChatId_FK");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.ToTable("Office");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDateAndTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StartDateAndTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Extra)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ExtraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders_ExtraId_FK");

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

            modelBuilder.Entity<OrderExtra>(entity =>
            {
                entity.Property(e => e.ExtraType)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("Place");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PlaceAddress)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PublishedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Place_OwnerId_FK");
            });

            modelBuilder.Entity<PlaceMedium>(entity =>
            {
                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MimeType).HasMaxLength(255);

                entity.Property(e => e.PlaceType)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.PlaceMedia)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlaceMedia_PlaceId_FK");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.Property(e => e.PaymentDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Receipts_CustomerId_FK");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Receipts_OrderId_FK");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Reviews_OrderId_FK");
            });

            modelBuilder.Entity<ReviewsMedium>(entity =>
            {
                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MimeType).HasMaxLength(255);

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.ReviewsMedia)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ReviewsMedia_ReviewId_FK");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534BEEB2A45")
                    .IsUnique();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ProfileImage)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Yard>(entity =>
            {
                entity.ToTable("Yard");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
