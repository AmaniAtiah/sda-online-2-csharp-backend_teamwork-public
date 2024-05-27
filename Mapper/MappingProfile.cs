using Backend.Dtos;
using Backend.EntityFramework;
using AutoMapper;

namespace Backend.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Cart, CartDto>();
            CreateMap<CartProduct, CartProductDto>();
            CreateMap<Order, OrderDto>();
            
          
       
            CreateMap<OrderProduct, OrderProductDto>();

             CreateMap<Category, CategoryDto>();
            
            CreateMap<Product, ProductDtos>()
                      .ForMember(dest => dest.Category, opt => opt.Ignore());
        }
    }
}