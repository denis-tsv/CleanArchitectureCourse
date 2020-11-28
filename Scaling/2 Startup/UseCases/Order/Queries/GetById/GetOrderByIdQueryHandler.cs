using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Interfaces;
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

        public GetOrderByIdQueryHandler(IDbContext dbContext, IMapper mapper, IDeliveryService deliveryService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
            var totalWeight = order.Items.Sum(x => x.Product.Weight);
            var deliveryCost = _deliveryService.CalculateDeliveryCost(totalWeight);
            dto.Total = order.GetTotal() + deliveryCost;

            return dto;
        }
    }
}
