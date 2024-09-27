﻿using Application.Response;
using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Classe que contém os métodos action da entidade User.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator, IConfiguration configuration, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Rota responsável pela criação de um novo usuário.
        /// </summary>
        /// <param name="command">Um objeto CreateUserCommand</param>
        /// <returns>Os dados do usuário criado.</returns>
        /// <remarks>
        /// Exemplo de request:
        /// ```
        /// POST /api/auth/users
        /// {
        ///    "name": "John",
        ///    "surname": "Doe",
        ///    "username": "JDoe",
        ///    "email": "jdoe@mail.com",
        ///    "password": "123456"
        /// }
        /// ```
        /// </remarks>
        /// <response code="200">Retorna os dados de um novo usuário</response>
        /// <response code="400">Se algum dado for digitado incorretamente</response>
        [HttpPost("users")]
        public async Task<ActionResult<ResponseBase<UserInfoViewModel>>> CreateUser(CreateUserCommand command)
        {
            var request = await _mediator.Send(command);

            if(request.ResponseInfo is null)
            {
                var userInfo = request.Value;

                if(userInfo is not null)
                {
                    var cookieOptionsToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(7)
                    };

                    _ = int.TryParse(_configuration["JWT:RefreshTokenExpirationTimeInDays"], out int refreshTokenExpirationTimeInDays);

                    var cookieOptionsRefreshToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(refreshTokenExpirationTimeInDays)
                    };

                    Response.Cookies.Append("jwt", request.Value!.TokenJWT!, cookieOptionsToken);
                    Response.Cookies.Append("refreshToken", request.Value!.RefreshToken!, cookieOptionsRefreshToken);
                    return Ok(_mapper.Map<UserInfoViewModel>(request.Value));
                }
            }

            return BadRequest(request);
        }

        /// <summary>
        /// Rota responsável pelo login de um usuário.
        /// </summary>
        /// <param name="command">Um objeto LoginUserCommand</param>
        /// <returns>Os dados do usuário logado.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<ResponseBase<UserInfoViewModel>>> Login(LoginUserCommand command)
        {
            var request = await _mediator.Send(command);

            if (request.ResponseInfo is null)
            {
                var userInfo = request.Value;

                if (userInfo is not null)
                {
                    var cookieOptionsToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(7)
                    };

                    _ = int.TryParse(_configuration["JWT:RefreshTokenExpirationTimeInDays"], out int refreshTokenExpirationTimeInDays);

                    var cookieOptionsRefreshToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(refreshTokenExpirationTimeInDays)
                    };

                    Response.Cookies.Append("jwt", request.Value!.TokenJWT!, cookieOptionsToken);
                    Response.Cookies.Append("refreshToken", request.Value!.RefreshToken!, cookieOptionsRefreshToken);
                    return Ok(_mapper.Map<UserInfoViewModel>(request.Value));
                }
            }

            return BadRequest(request);
        }

        /// <summary>
        /// Rota responsável por renovar o token JWT.
        /// </summary>
        /// <param name="command">Um objeto RefreshTokenCommand</param>
        /// <returns>Os dados do usuário com novo token.</returns>
        [HttpPost("refresh-token")]
        public async Task<ActionResult<ResponseBase<UserInfoViewModel>>> RefreshToken(RefreshTokenCommand command)
        {
            var request = await _mediator.Send(new RefreshTokenCommand
            {
                Username = command.Username,
                RefreshToken = Request.Cookies["refreshToken"]
            });

            if (request.ResponseInfo is null)
            {
                var userInfo = request.Value;

                if (userInfo is not null)
                {
                    var cookieOptionsToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(7)
                    };

                    _ = int.TryParse(_configuration["JWT:RefreshTokenExpirationTimeInDays"], out int refreshTokenExpirationTimeInDays);

                    var cookieOptionsRefreshToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(refreshTokenExpirationTimeInDays)
                    };

                    Response.Cookies.Append("jwt", request.Value!.TokenJWT!, cookieOptionsToken);
                    Response.Cookies.Append("refreshToken", request.Value!.RefreshToken!, cookieOptionsRefreshToken);
                    return Ok(_mapper.Map<UserInfoViewModel>(request.Value));
                }
            }

            return BadRequest(request);
        }
    }
}
