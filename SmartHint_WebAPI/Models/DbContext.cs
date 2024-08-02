using Microsoft.EntityFrameworkCore;

namespace SmartHint_WebAPI.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext(DbContextOptions
        <DBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Client> Customer { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity => {
                entity.HasKey(k => k.ID);
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
