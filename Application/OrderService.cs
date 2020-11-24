using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IOrderDomainService _orderDomainService;

        public OrderService(AppDbContext dbContext, IMapper mapper, IOrderDomainService orderDomainService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _orderDomainService = orderDomainService;
        }
        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var order = await _dbContext.Orders
                .AsNoTracking()
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order == null) throw new EntityNotFoundException();

            var dto = _mapper.Map<OrderDto>(order);
            dto.Total = _orderDomainService.GetTotal(order);

            return dto;
        }
    }
}
