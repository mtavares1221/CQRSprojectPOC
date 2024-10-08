using Infra.Persistence;
using Infra.Repository.IRepositories;
using Infra.Repository.Repositories;

namespace Infra.Repository.UnitOfWork
{
    public class UnitOfWork(MongoDbContext context, 
        IUserRepository userRepository,IWorkspaceRepository workspaceRepository,
        IListsCardsRepository listsCardsRepository) : IUnitOfWork
    {
        private readonly MongoDbContext _context = context;

        public IUserRepository UserRepository => userRepository ?? new UserRepository(context);
        public IWorkspaceRepository WorkspaceRepository => workspaceRepository ?? new WorkspaceRepository(context);
        public IListsCardsRepository ListsCardsRepository => listsCardsRepository ?? new ListsCardsRepository(context);

        //public void Commit()
        //{
        //    _context.SaveChanges();
        //}
    }
}
