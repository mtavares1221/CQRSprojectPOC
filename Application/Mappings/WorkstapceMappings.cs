using Application.WorkspaceCQ.ViewModels;
using AutoMapper;
using Domain.Entity;

namespace Application.Mappings
{
    public class WorkstapceMappings : Profile
    {
        public WorkstapceMappings()
        {
            CreateMap<Workspace, WorkspaceVIewModel>()
                .ForMember(x => x.UserId,x => x.MapFrom(x => x.User!.Id));
        }
    }
}
