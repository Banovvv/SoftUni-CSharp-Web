using BattleCards.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BattleCards.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext()
        {
        }

        public ApplicationDataContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(@"Server=.\SQLEXPRESS;Database=BattleCards;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
