using Application.Response;
using Application.WorkspaceCQ.ViewModels;
using Domain.Entity;
using MediatR;

namespace Application.WorkspaceCQ.Commands
{
    public record CreateWorkspaceCommand : IRequest<ResponseBase<WorkspaceVIewModel>>
    {
        public string? Title { get; set; }
        public string? UserId { get; set; }
    }
}
