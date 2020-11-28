using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using UseCases.Order.Commands.CreateOrder;
using UseCases.Order.Dto;
using UseCases.Order.Queries.GetById;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;

        public OrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            var result = await _sender.Send(new GetOrderByIdQuery {Id = id});
            return result;
        }

        [HttpPost]
        public async Task<int> Create([FromBody]CreateOrderDto dto)
        {
            var id = await _sender.Send(new CreateOrderCommand {Dto = dto});
            return id;
        }
    }
}
