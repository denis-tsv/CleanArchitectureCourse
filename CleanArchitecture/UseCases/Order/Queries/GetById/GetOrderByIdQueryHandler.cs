using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interface;
using Delivery.Interfaces;
using DomainServices.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IOrderDomainService _orderDomainService;
        private readonly IDeliveryService _deliveryService;

        public GetOrderByIdQueryHandler(IDbContext dbContext, 
            IMapper mapper, 
            IOrderDomainService orderDomainService,
            IDeliveryService deliveryService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _orderDomainService = orderDomainService;
            _deliveryService = deliveryService;
        }
        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .AsNoTracking()
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == query.Id);

            if (order == null) throw new EntityNotFoundException();

            var dto = _mapper.Map<OrderDto>(order);
            dto.Total = _orderDomainService.GetTotal(order, _deliveryService.CalculateDeliveryCost);

            return dto;
        }
    }
}
