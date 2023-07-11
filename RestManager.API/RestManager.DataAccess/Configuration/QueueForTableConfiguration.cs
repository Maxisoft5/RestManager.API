using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestManager.DataAccess.Configuration.Abstract;
using RestManager.DataAccess.Models;

namespace RestManager.DataAccess.Configuration
{
    public class QueueForTableConfiguration : BaseEntityConfig<QueueForTable>
    {
        public QueueForTableConfiguration()
        {
        }

        public override void Configure(EntityTypeBuilder<QueueForTable> builder)
        {
            base.Configure(builder);
        }
    }
}
