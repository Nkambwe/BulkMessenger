using BulkMessager.Data.Entities;
using BulkMessager.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BulkMessager.Data {

    public class DataContext : DbContext {

        public DbSet<Message> Messages { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            MessageEntityConfiguration.Configure(modelBuilder.Entity<Message>());
            base.OnModelCreating(modelBuilder);
        }
    }

}
