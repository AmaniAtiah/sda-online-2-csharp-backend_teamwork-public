using Backend.Dtos;
using Backend.EntityFramework;
using AutoMapper;
using Backend.Models;

namespace Backend.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
   
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductDtos>();
        }
    }
}