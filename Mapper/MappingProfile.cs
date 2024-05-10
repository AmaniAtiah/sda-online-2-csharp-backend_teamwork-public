
using Backend.Dtos;
using Backend.EntityFramework;
using AutoMapper;

namespace Backend.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
        
    }
}