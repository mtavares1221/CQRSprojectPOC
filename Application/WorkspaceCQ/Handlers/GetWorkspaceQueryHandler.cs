using Application.Response;
using Application.WorkspaceCQ.Queries;
using Application.WorkspaceCQ.ViewModels;
using AutoMapper;
using Infra.Repository.UnitOfWork;
using MediatR;

namespace Application.WorkspaceCQ.Handlers
{
    public record GetWorkspaceQueryHandler(IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<GetWorkspaceQuery, ResponseBase<WorkspaceVIewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<ResponseBase<WorkspaceVIewModel>> Handle(GetWorkspaceQuery request, CancellationToken cancellationToken)
        {
            var workspace = await _unitOfWork.WorkspaceRepository.GetWorkspaceAndUserAsync(request.Id);
            if (workspace == null) {
                return new ResponseBase<WorkspaceVIewModel>
                {
                    ResponseInfo = new ResponseInfo
                    {
                        ErrorDescription = "Nenhum workspace encontrado com o Id informado",
                        HTTPStatus = 400,
                        Title = "Workspace não encontrado"
                    },
                    Value = null
                };
            }

            return new ResponseBase<WorkspaceVIewModel>
            {
                ResponseInfo = null,
                Value = _mapper.Map<WorkspaceVIewModel>(workspace)
            };

        }
    }
}
