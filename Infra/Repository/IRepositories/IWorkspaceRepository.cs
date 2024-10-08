using Domain.Entity;
using Infra.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.IRepositories
{
    public interface IWorkspaceRepository : IBaseRepository<Workspace>
    {
        public Task<Workspace?> GetWorkspaceAndUserAsync(string workspaceId);
        Task<List<Workspace>> GetAllWorkspacesAndUserSync(string userId);
    }
}
