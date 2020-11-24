using System.Collections.Generic;

namespace Application
{
    public class CreateOrderDto
    {
        public List<OrderItemDto> Items { get; set; } 
    }
}