using Application.Response;
using Application.WorkspaceCQ.Commands;
using Application.WorkspaceCQ.ViewModels;
using AutoMapper;
using Infra.Repository.UnitOfWork;
using MediatR;

namespace Application.WorkspaceCQ.Handlers
{
    public class EditWorkspaceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<EditWorkspaceCommand, ResponseBase<WorkspaceVIewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<ResponseBase<WorkspaceVIewModel>> Handle(EditWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var workspace = await _unitOfWork.WorkspaceRepository.GetWorkspaceAndUserAsync(request.Id);

            if (workspace == null)
            {
                return new ResponseBase<WorkspaceVIewModel>
                {
                    ResponseInfo = new ResponseInfo
                    {
                        ErrorDescription = "Nenhum workspace encontrado com o Id informado",
                        HTTPStatus = 400,
                        Title = "Workspace não encontrado"
                    }
                };
            }

            if(request.Title != null)
                workspace.Title = request.Title;

            if (request.Status != null)
                workspace.Status = request.Status;

            //salvar o workspace
            await _unitOfWork.WorkspaceRepository.Update(workspace);

            return new ResponseBase<WorkspaceVIewModel>
            {
                ResponseInfo = null,
                Value = _mapper.Map<WorkspaceVIewModel>(workspace)
            };
        }
    }
}
