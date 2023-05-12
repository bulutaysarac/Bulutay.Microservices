using AutoMapper;
using FreeCourse.Services.Order.Application.Dtos;

namespace FreeCourse.Services.Order.Application.Mapping
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<Order.Domain.OrderAggregate.Order, OrderDto>();
            CreateMap<Order.Domain.OrderAggregate.OrderItem, OrderItemDto>();
            CreateMap<Order.Domain.OrderAggregate.Address, AddressDto>();
        }
    }
}