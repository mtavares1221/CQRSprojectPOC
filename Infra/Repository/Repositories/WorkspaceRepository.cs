using Domain.Entity;
using Infra.Persistence;
using Infra.Repository.IRepositories;
using Infra.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Repositories
{
    public class WorkspaceRepository(MongoDbContext context) : BaseRepository<Workspace>(context),IWorkspaceRepository
    {
        private readonly MongoDbContext _context = context;
        public async Task<Workspace?> GetWorkspaceAndUserAsync(string workspaceId)
        {
            // Aqui usamos o Id como string
            return await _context.Workspaces
                .Find(workspace => workspace.Id == workspaceId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Workspace>> GetAllWorkspacesAndUserSync(string userId)
        {
            // Aqui também usamos o Id como string
            return await _context.Workspaces
                .Find(workspace => workspace.User!.Id == userId) // Mudamos para UserId que é string
                .ToListAsync();
        }
    }
}
