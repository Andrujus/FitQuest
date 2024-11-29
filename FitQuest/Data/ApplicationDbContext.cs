using FitQuest.Models;
using Microsoft.EntityFrameworkCore;

namespace FitQuest.Models
{
   public class MQuotes
{
    public int Id { get; set; } // Maps to the "ID" column
    public string MQuote { get; set; } // Maps to the "MQuote" column
    public bool Used { get; set; } // Maps to the "used" column
}

}

namespace FitQuest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<MQuotes> MQuotes { get; set; } //kazkas neok
        public MQuotes GetRandomQuote()
        {
            return MQuotes.OrderBy(q => Guid.NewGuid()).FirstOrDefault();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map the custom User properties to your existing table structure
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Username).HasColumnName("Username");
                entity.Property(e => e.Password).HasColumnName("Password");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.EmailConfirmed).HasColumnName("EmailConfirmed");
            });

            // Map the PasswordResetToken properties to the table structure
            modelBuilder.Entity<PasswordResetToken>(entity =>
            {
                entity.ToTable("PasswordResetTokens");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.Token).HasColumnName("Token");
                entity.Property(e => e.Expiration).HasColumnName("Expiration");
            });
            modelBuilder.Entity<MQuotes>(entity =>
            {
                entity.ToTable("MQuotes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.MQuote).HasColumnName("MQuote");
                entity.Property(e => e.Used).HasColumnName("used");
            });
            
        }
        
    }
}
