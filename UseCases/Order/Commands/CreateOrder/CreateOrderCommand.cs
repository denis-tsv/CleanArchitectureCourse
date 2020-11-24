using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public CreateOrderDto Dto { get; set; }
    }
}
