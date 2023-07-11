namespace RestManager.DataAccess.Models.Abstract
{
    public abstract class DbModel
    {
        public long Id { get; }
        public DateTime CreatedAt { get; }
    }
}
