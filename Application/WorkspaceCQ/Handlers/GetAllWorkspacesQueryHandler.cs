using Application.Response;
using Application.Utils;
using Application.WorkspaceCQ.Queries;
using Application.WorkspaceCQ.ViewModels;
using AutoMapper;
using Infra.Repository.UnitOfWork;
using MediatR;

namespace Application.WorkspaceCQ.Handlers
{
    public class GetAllWorkspacesQueryHandler : IRequestHandler<GetAllWorkspacesQuery, ResponseBase<PaginetedList<WorkspaceVIewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllWorkspacesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseBase<PaginetedList<WorkspaceVIewModel>>> Handle(GetAllWorkspacesQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(x => x.Id == request.UserId);
            if (user == null)
            {
                return new ResponseBase<PaginetedList<WorkspaceVIewModel>>
                {
                    ResponseInfo = new ResponseInfo
                    {
                        ErrorDescription = "Nenhum usuário encontrado com o Id informado",
                        HTTPStatus = 400,
                        Title = "Usuário não encontrado"
                    },
                    Value = null
                };
            }

            var workspaces = _unitOfWork.WorkspaceRepository.GetAllWorkspacesAndUserSync(request.UserId);

            return new ResponseBase<PaginetedList<WorkspaceVIewModel>>
            {
                ResponseInfo = null,
                Value = new PaginetedList<WorkspaceVIewModel>(
                    _mapper.Map<List<WorkspaceVIewModel>>(workspaces),
                    request.PageIndex,
                    request.PageSize)
            };
        }
    }
}
