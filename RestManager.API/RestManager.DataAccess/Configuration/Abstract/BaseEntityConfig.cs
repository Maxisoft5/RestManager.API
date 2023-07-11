using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestManager.DataAccess.Models.Abstract;

namespace RestManager.DataAccess.Configuration.Abstract
{
    public abstract class BaseEntityConfig<TType> : IEntityTypeConfiguration<TType>
            where TType : DbModel
    {

        public BaseEntityConfig()
        {
        
        }

        public virtual void Configure(EntityTypeBuilder<TType> builder)
        {
            builder.HasKey(obj => obj.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
