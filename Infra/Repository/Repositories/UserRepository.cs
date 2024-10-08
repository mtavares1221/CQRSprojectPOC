using Domain.Entity;
using Infra.Persistence;
using Infra.Repository.IRepositories;
using Infra.Repository.UnitOfWork;

namespace Infra.Repository.Repositories
{
    public class UserRepository(MongoDbContext context) : BaseRepository<User>(context), IUserRepository
    {
    }
}
