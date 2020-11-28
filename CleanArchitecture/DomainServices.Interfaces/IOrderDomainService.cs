using System;
using Domain.Entities;

namespace DomainServices.Interfaces
{
    public interface IOrderDomainService
    {
        decimal GetTotal(Order order, CalculateDeliveryCost deliveryCostCalculator);
    }
}
