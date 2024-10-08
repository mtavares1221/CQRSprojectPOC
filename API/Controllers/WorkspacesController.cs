using Application.WorkspaceCQ.Commands;
using Application.WorkspaceCQ.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public static class WorkspacesController
    {
        public static void WorkspacesRoutes(this WebApplication app)
        {
            var group = app.MapGroup("Workspaces").WithTags("Workspaces");
                //.RequireAuthorization();

            group.MapPost("",Create);

            group.MapPut("{id:guid}",Edit);

            group.MapDelete("{id:guid}",Delete);

            group.MapGet("{id:guid}", Get);

            group.MapGet("",  GetAll);
        }

        //delegates

        public static async Task<IResult> Create(
            [FromServices] IMediator _mediator,
            [FromBody]CreateWorkspaceCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.ResponseInfo is null)
                return Results.Ok(result.Value);

            return Results.BadRequest(result.ResponseInfo);
        }
        public static async Task<IResult> Edit(
            [FromServices] IMediator _mediator,
            [FromBody] EditWorkspaceCommand command)
        {

            var result = await _mediator.Send(command);

            if (result.ResponseInfo is null)
                return Results.Ok(result.Value);

            return Results.BadRequest(result.ResponseInfo);

        }

        public static async Task<IResult> Delete([FromServices] IMediator _mediator,string id)
        {

            var result = await _mediator.Send(new DeleteWorkspaceCommand { Id = id });

            if (result.ResponseInfo is null)
                return Results.NoContent();

            return Results.BadRequest(result.ResponseInfo);


        }

        public static async Task<IResult> Get([FromServices] IMediator _mediator, string id)
        {
            var result = await _mediator.Send(new GetWorkspaceQuery { Id = id });

            if (result.ResponseInfo is null)
                return Results.Ok(result.Value);

            return Results.BadRequest(result.ResponseInfo);
        }


        public static async Task<IResult> GetAll([FromServices] IMediator _mediator,
            [FromQuery]string userId,
            [FromQuery] int pageSize,
            [FromQuery] int pageIndex)
        {

            var result = await _mediator.Send(new GetAllWorkspacesQuery
            {
                UserId = userId,
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            if (result.ResponseInfo is null)
                return Results.Ok(result.Value);

            return Results.BadRequest(result.ResponseInfo);

        }
    }
}
