using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Interfaces;
using DomainServices.Interfaces;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Order.Dto;

namespace UseCases.Order.Queries.GetById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDeliveryService _deliveryService;
        private readonly IOrderDomainService _orderDomainService;

        public GetOrderByIdQueryHandler(IDbContext dbContext, 
            IMapper mapper, 
            IDeliveryService deliveryService,
            IOrderDomainService orderDomainService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _deliveryService = deliveryService;
            _orderDomainService = orderDomainService;
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
