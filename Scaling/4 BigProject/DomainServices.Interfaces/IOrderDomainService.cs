using System;
using Entities.Models;

namespace DomainServices.Interfaces
{
    public interface IOrderDomainService
    {
        decimal GetTotal(Order order, CalculateDeliveryCost deliveryCostCalculator);
    }
}
