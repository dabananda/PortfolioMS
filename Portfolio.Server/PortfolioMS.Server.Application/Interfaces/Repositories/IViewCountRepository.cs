using PortfolioMS.Server.Domain.Entities;

namespace PortfolioMS.Server.Application.Interfaces.Repositories
{
    public interface IViewCountRepository
    {
        Task<ViewCount?> GetByPageLinkAsync(string pageLink);
    }
}
