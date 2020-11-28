using System.Collections.Generic;

namespace UseCases.Order.Dto
{
    public class CreateOrderDto
    {
        public List<OrderItemDto> Items { get; set; } 
    }
}