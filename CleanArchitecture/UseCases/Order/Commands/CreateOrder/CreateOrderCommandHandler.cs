using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interface;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(command.Dto);
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order.Id;
        }
    }
}
