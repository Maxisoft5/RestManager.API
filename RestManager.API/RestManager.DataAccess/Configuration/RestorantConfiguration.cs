using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestManager.DataAccess.Configuration.Abstract;
using RestManager.DataAccess.Models;

namespace RestManager.DataAccess.Configuration
{
    public class RestorantConfiguration : BaseEntityConfig<Restorant>
    {
        public RestorantConfiguration()
        {
        }

        public override void Configure(EntityTypeBuilder<Restorant> builder)
        {
            base.Configure(builder);
            builder.HasMany(x => x.Tables).WithOne(x => x.Restorant);
            builder.HasIndex(x => x.Address).IsUnique();
        }
    }
}
