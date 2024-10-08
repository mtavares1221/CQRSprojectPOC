using Application.Response;
using Application.Utils;
using Application.WorkspaceCQ.ViewModels;
using MediatR;

namespace Application.WorkspaceCQ.Queries
{
    public record GetWorkspaceQuery : IRequest<ResponseBase<WorkspaceVIewModel>>
    {
        public string Id { get; set; }
    }
}
