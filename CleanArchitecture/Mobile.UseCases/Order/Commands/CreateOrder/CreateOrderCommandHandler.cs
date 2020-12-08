using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interface;
using Domain.Entities;
using Infrastructure.Interfaces.Integrations;
using Infrastructure.Interfaces.WebApp;
using MediatR;
using WebApp.Interfaces;

namespace Application.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobService _backgroundJobService;
        private readonly ICurrentUserService _currentUserService;

        public CreateOrderCommandHandler(IDbContext dbContext, 
            IMapper mapper, 
            IBackgroundJobService backgroundJobService,
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _backgroundJobService = backgroundJobService;
            _currentUserService = currentUserService;
        }
        public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(command.Dto);
            order.CreateDate = DateTime.UtcNow;
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            _backgroundJobService.Schedule<IEmailService>(emailService => emailService.SendAsync(_currentUserService.Email, "Order created", $"Order {order.Id} created"));
            return order.Id;
        }
    }
}
