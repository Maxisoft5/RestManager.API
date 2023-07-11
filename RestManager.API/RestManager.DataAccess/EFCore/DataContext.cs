using Microsoft.EntityFrameworkCore;
using RestManager.DataAccess.Models;
using System.Reflection.Metadata;

namespace RestManager.DataAccess.EFCore
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<ClientGroup> ClientGroups { get; set; }
        public DbSet<Restorant> Restorants { get; set; }
        public DbSet<TableRequest> TableRequests { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<QueueForTable> QueuesForTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            modelBuilder.Entity<QueueForTable>().HasOne(x => x.ClientGroup).WithOne(x => x.QueueForTable)
                .HasForeignKey<ClientGroup>(x => x.QueueForTableId).OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }

    }
}
