using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestManager.DataAccess.Configuration.Abstract;
using RestManager.DataAccess.Models;

namespace RestManager.DataAccess.Configuration
{
    public class TableRequestConfiguration : BaseEntityConfig<TableRequest>
    {
        public TableRequestConfiguration()
        {
        }

        public override void Configure(EntityTypeBuilder<TableRequest> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.ClientGroup).WithMany(x => x.TableRequests)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            builder.HasOne(x => x.Table).WithMany(x => x.TableRequests)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }
    }
}
