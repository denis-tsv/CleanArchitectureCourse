using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Queries.GetById
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }
}
