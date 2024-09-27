﻿using Domain.Entity;
using Infra.Persistence;
using Infra.Repository.IRepositories;
using Infra.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Repositories
{
    public class WorkspaceRepository(TasksDbContext context) : BaseRepository<Workspace>(context),IWorkspaceRepository
    {
        private readonly TasksDbContext _context = context;
        public async Task<Workspace?> GetWorkspaceAndUserAsync(Guid workspaceId)
        {

            return await _context.Workspaces
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == workspaceId);

        }

        public async Task<List<Workspace>> GetAllWorkspacesAndUserSync(Guid userId)
        {
            return await _context.Workspaces
                .Include(x => x.User)
                .Where(x => x.User!.Id == userId)
                .ToListAsync();
        }
    }
}
