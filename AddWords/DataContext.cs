using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Word_Puzzle.Model;


namespace Word_Puzzle.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           

        }
        public DbSet<Word> Words { get; set; }
       public DbSet<Definition> Definitions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Words;Trusted_Connection=True;TrustServerCertificate=true;");
        }

       
    }
}
