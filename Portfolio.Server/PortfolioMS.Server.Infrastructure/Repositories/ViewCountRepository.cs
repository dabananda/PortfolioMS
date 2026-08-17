using Microsoft.EntityFrameworkCore;
using PortfolioMS.Server.Application.Interfaces.Repositories;
using PortfolioMS.Server.Domain.Entities;
using PortfolioMS.Server.Infrastructure.Data;

namespace PortfolioMS.Server.Infrastructure.Repositories
{
    public class ViewCountRepository(ApplicationDbContext context) : IViewCountRepository
    {
        public async Task<ViewCount?> GetByPageLinkAsync(string pageLink)
        {
            return await context.ViewCounts.FirstOrDefaultAsync(x => x.PageLink == pageLink);
        }
    }
}
