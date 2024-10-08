using Application.Response;
using Application.WorkspaceCQ.ViewModels;
using Domain.Enum;
using MediatR;

namespace Application.WorkspaceCQ.Commands
{
    public record EditWorkspaceCommand : IRequest<ResponseBase<WorkspaceVIewModel>>
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public StatusItemEnum? Status { get; set; }
    }
}
