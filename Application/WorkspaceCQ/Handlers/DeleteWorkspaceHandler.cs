using Application.Response;
using Application.WorkspaceCQ.Commands;
using Application.WorkspaceCQ.ViewModels;
using AutoMapper;
using Infra.Repository.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkspaceCQ.Handlers
{
    public class DeleteWorkspaceHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<DeleteWorkspaceCommand, ResponseBase<DeleteWorkspaceCommand>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseBase<DeleteWorkspaceCommand>> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var workspace = await _unitOfWork.WorkspaceRepository.Get(x => x.Id == request.Id);

            if (workspace == null)
            {
                return new ResponseBase<DeleteWorkspaceCommand>
                {
                    ResponseInfo = new ResponseInfo
                    {
                        ErrorDescription = "Nenhum workspace encontrado com o Id informado",
                        HTTPStatus = 400,
                        Title = "Workspace não encontrado"
                    }
                };
            }

            var listCards = _unitOfWork.ListsCardsRepository.GetAll().Where(x => x.Workspace == workspace).ToList();

            await _unitOfWork.ListsCardsRepository.DeleteRange(listCards);

            await _unitOfWork.WorkspaceRepository.Delete(workspace);

            return new ResponseBase<DeleteWorkspaceCommand>
            {
                ResponseInfo = null,
                Value = request
            };
        }
    }
}
