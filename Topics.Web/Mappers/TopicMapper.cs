using AutoMapper;
using System.Web.Mvc;
using Topics.Core.Mappers.Interfaces;
using Topics.Core.Models;
using Topics.Web.ViewModels;

namespace Topics.Web.Mappers
{
    public class TopicMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<TopicDTO, TopicVM>();
            Mapper.CreateMap<TopicVM, TopicDTO>();
            Mapper.CreateMap<PostVM, PostDTO>();
            Mapper.CreateMap<PostDTO, PostVM>();
            Mapper.CreateMap<RoleDTO, RoleVM>();
            Mapper.CreateMap<RoleVM, RoleDTO>();

            Mapper.CreateMap<UserDTO, UserVM>();
            Mapper.CreateMap<UserVM, UserDTO>();

            Mapper.CreateMap<int, RoleDTO>()
                .ForMember(dest => dest.RoleID, opts => opts.MapFrom(src => src));

            Mapper.CreateMap<RoleDTO, SelectListItem>()
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.RoleID.ToString()))
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.Name));
            Mapper.CreateMap<TopicDTO, SelectListItem>()
               .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.TopicID.ToString()))
               .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.Name));
            Mapper.CreateMap<UserDTO, SelectListItem>()
               .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.UserID.ToString()))
               .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.UserName));
            Mapper.CreateMap<int, RoleDTO>()
                .ForMember(dest => dest.RoleID, opts => opts.MapFrom(src => src));
        }
    }
}
