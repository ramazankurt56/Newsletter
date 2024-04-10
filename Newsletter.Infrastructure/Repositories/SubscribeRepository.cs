using GenericRepository;
using Newsletter.Domain.Models;
using Newsletter.Domain.Repositories;
using Newsletter.Infrastructure.Context;

namespace Newsletter.Infrastructure.Repositories;

internal sealed class SubscribeRepository : Repository<Subscribe, ApplicationDbContext>, ISubscribeRepository
{
    public SubscribeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
