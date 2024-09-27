using Infra.Repository.IRepositories;

namespace Infra.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IWorkspaceRepository WorkspaceRepository { get; }
        IListsCardsRepository ListsCardsRepository { get; }
        void Commit();
    }
}
