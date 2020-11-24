using System;
using System.Linq;
using DomainServices.Interfaces;
using Domain.Entities;

namespace DomainServices.Implementation
{
    public class OrderDomainService : IOrderDomainService
    {
        public decimal GetTotal(Order order)
        {
            return order.Items.Sum(x => x.Quantity * x.Product.Price);
        }
    }
}
