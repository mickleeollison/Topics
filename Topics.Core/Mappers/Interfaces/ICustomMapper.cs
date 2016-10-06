using AutoMapper;

namespace Topics.Core.Mappers.Interfaces
{
    public interface ICustomMapper
    {
        void CreateMappings(AutoMapper.IConfiguration configuration);
    }
}
