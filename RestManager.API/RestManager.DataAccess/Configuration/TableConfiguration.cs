using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestManager.DataAccess.Configuration.Abstract;
using RestManager.DataAccess.Models;

namespace RestManager.DataAccess.Configuration
{
    public class TableConfiguration : BaseEntityConfig<Table>
    {
        public TableConfiguration() 
        {
         
        }

        public override void Configure(EntityTypeBuilder<Table> builder)
        {
            base.Configure(builder);
            builder.HasMany(x => x.TableRequests).WithOne(x => x.Table);
                

            builder.HasOne(x => x.Restorant)
                .WithMany(x => x.Tables);
        }

    }
}
