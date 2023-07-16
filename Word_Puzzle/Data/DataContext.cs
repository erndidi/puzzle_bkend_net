using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Puzzle_API.Model;
using Puzzle_API.Model;
using System.Reflection.Emit;
using System.Reflection.Metadata;


namespace Puzzle_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDetail>()
                .HasOne(ud => ud.UserSession)
                .WithOne(us => us.UserDetail)
                .HasForeignKey<UserSession>(us => us.UserDetailId);
        }

        public DbSet<Word> Words { get; set; }
       public DbSet<Definition> Definitions { get; set; }
       
        public DbSet<UserDetail> UserDetail { get; set; }

        public DbSet<UserSession> UserSessions { get; set; }

        public DbSet<UserWord> UserWord { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Words;Trusted_Connection=True;TrustServerCertificate=true;");
        }

       
    }
}
