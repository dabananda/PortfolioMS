namespace PortfolioMS.Server.Domain.Entities
{
    public class ViewCount : BaseEntity
    {
        public long TotalCount { get; set; }
        public long TotalDistinctCount { get; set; }
        public string PageLink { get; set; }
    }
}
