using AutoMapper;
using Topics.Core.Mappers.Interfaces;
using Topics.Core.Models;
using Topics.Data.Entities;

namespace Topics.Data.Mappers
{
    public class TopicMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Posts, opt => opt.Ignore());
            Mapper.CreateMap<UserDTO, User>();
            Mapper.CreateMap<Role, RoleDTO>();
            Mapper.CreateMap<RoleDTO, Role>();
            Mapper.CreateMap<Topic, TopicDTO>()
                .ForMember(dest => dest.Posts, opt => opt.Ignore());
            Mapper.CreateMap<TopicDTO, Topic>();
            Mapper.CreateMap<Post, PostDTO>();
            Mapper.CreateMap<PostDTO, Post>();
            Mapper.CreateMap<UserCredentials, UserCredentialsDTO>();
            Mapper.CreateMap<UserCredentialsDTO, UserCredentials>();
        }
    }
}
