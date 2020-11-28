using AutoMapper;
using Entities.Models;
using UseCases.Order.Dto;

namespace UseCases.Order.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Entities.Models.Order, OrderDto>();
            CreateMap<CreateOrderDto, Entities.Models.Order>();
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
