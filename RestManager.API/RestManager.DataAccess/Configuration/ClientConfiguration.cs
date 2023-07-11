using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestManager.DataAccess.Configuration.Abstract;
using RestManager.DataAccess.Models;

namespace RestManager.DataAccess.Configuration
{
    public class ClientConfiguration : BaseEntityConfig<Client>
    {
        public ClientConfiguration()
        {
        }

        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Group).WithMany(x => x.Clients)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
