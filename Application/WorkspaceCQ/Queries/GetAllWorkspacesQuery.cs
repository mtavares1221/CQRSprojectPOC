using Application.Response;
using Application.Utils;
using Application.WorkspaceCQ.ViewModels;
using MediatR;

namespace Application.WorkspaceCQ.Queries
{
    public record GetAllWorkspacesQuery : QueryBase, IRequest<ResponseBase<PaginetedList<WorkspaceVIewModel>>>
    {
        public string UserId { get; set; }
    }
}
