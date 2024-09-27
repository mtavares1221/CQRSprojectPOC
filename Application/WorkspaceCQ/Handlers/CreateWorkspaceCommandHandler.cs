using Application.Response;
using Application.WorkspaceCQ.Commands;
using Application.WorkspaceCQ.ViewModels;
using AutoMapper;
using Domain.Entity;
using Infra.Repository.UnitOfWork;
using MediatR;

namespace Application.WorkspaceCQ.Handlers
{
    public class CreateWorkspaceCommandHandler(IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<CreateWorkspaceCommand, ResponseBase<WorkspaceVIewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<ResponseBase<WorkspaceVIewModel>> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(x => x.Id == request.UserId);

            if (user == null)
            {
                return new ResponseBase<WorkspaceVIewModel>
                {
                    ResponseInfo = new ResponseInfo
                    {
                        ErrorDescription = "Nenhum usuároi encontrato com o Id informado",
                        HTTPStatus = 400,
                        Title = "Usuário não encontrado"
                    }
                };
            }

            var workspace = new Workspace
            {
                User = user,
                Title = request.Title
            };

            //salvar o workspace
            await _unitOfWork.WorkspaceRepository.Create(workspace);
            _unitOfWork.Commit();

            return new ResponseBase<WorkspaceVIewModel>
            {
                ResponseInfo = null,
                Value = _mapper.Map<WorkspaceVIewModel>(workspace)
            };
        }
    }
}
