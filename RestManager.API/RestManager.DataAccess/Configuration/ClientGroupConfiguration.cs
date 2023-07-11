using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestManager.DataAccess.Configuration.Abstract;
using RestManager.DataAccess.Models;

namespace RestManager.DataAccess.Configuration
{
    public class ClientGroupConfiguration : BaseEntityConfig<ClientGroup>
    {
        public ClientGroupConfiguration()
        {
        }

        public override void Configure(EntityTypeBuilder<ClientGroup> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => x.Clients).WithOne(x => x.Group)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            builder.HasMany(x => x.TableRequests).WithOne(x => x.ClientGroup);
        }
    }
}
