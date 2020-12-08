using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interface;
using Delivery.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Mobile.UseCases.Order.BackgroundJobs
{
    public class UpdateOrdersDeliveryStatusJob : IJob
    {
        private readonly IDbContext _dbContext;
        private readonly IDeliveryService _deliveryService;

        public UpdateOrdersDeliveryStatusJob(IDbContext dbContext, IDeliveryService deliveryService)
        {
            _dbContext = dbContext;
            _deliveryService = deliveryService;
        }

        public async Task ExecuteAsync()
        {
            var orders = await _dbContext.Orders
                .Where(x => x.Status == OrderStatus.Created)
                .ToListAsync();

            var deliveredItems = orders
                .Select(x => new {Order = x, Task = _deliveryService.IsDeliveredAsync(x.Id)})
                .ToList();

            await Task.WhenAll(deliveredItems.Select(x => x.Task));

            foreach (var item in deliveredItems)
            {
                if (item.Task.Result)
                {
                    item.Order.Status = OrderStatus.Delivered;
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
