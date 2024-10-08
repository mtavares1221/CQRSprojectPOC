using Application.Response;
using MediatR;

namespace Application.WorkspaceCQ.Commands
{
    public record DeleteWorkspaceCommand : IRequest<ResponseBase<DeleteWorkspaceCommand>>
    {
        public string Id { get; set; }
    }
}
