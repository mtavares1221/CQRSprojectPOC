using Application.Response;
using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entity;
using Domain.Enum;
using Infra.Repository.UnitOfWork;
using MediatR;
using Services.AuthService;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseBase<RefreshTokenViewModel?>>
{
    private readonly IAuthService _authService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
    {
        _authService = authService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseBase<RefreshTokenViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Chame o método de forma assíncrona
        var isUniqueEmailAndUsername = await _authService.UniqueEmailAndUsername(request.Email!, request.Username!);

        if (isUniqueEmailAndUsername == ValidationFieldsUserEnum.EmailUnavailable)
        {
            return new ResponseBase<RefreshTokenViewModel>
            {
                ResponseInfo = new()
                {
                    Title = "Email indisponível.",
                    ErrorDescription = "O Email apresentado já está sendo utilizado. Tente outro.",
                    HTTPStatus = 400
                },
                Value = null
            };
        }

        if (isUniqueEmailAndUsername == ValidationFieldsUserEnum.UsernameUnavailable)
        {
            return new ResponseBase<RefreshTokenViewModel>
            {
                ResponseInfo = new()
                {
                    Title = "Username indisponível.",
                    ErrorDescription = "O username apresentado já está sendo utilizado. Tente outro.",
                    HTTPStatus = 400
                },
                Value = null
            };
        }

        if (isUniqueEmailAndUsername == ValidationFieldsUserEnum.UsernameAndEmailUnavailable)
        {
            return new ResponseBase<RefreshTokenViewModel>
            {
                ResponseInfo = new()
                {
                    Title = "Username e Email indisponíveis.",
                    ErrorDescription = "O username e o email apresentados já estão sendo utilizados. Tente outros.",
                    HTTPStatus = 400
                },
                Value = null
            };
        }

        // Cria o usuário
        var user = _mapper.Map<User>(request);
        user.RefreshToken = _authService.GenerateRefreshToken();
        user.PasswordHash = _authService.HashingPassword(request.Password!);

        await _unitOfWork.UserRepository.Create(user);
        //await _unitOfWork.Commit(); // Não esqueça de salvar as mudanças

        var refreshTokenVM = _mapper.Map<RefreshTokenViewModel>(user);
        refreshTokenVM.TokenJWT = _authService.GenerateJWT(user.Email!, user.Username!);

        return new ResponseBase<RefreshTokenViewModel>
        {
            ResponseInfo = null,
            Value = refreshTokenVM
        };
    }
}
