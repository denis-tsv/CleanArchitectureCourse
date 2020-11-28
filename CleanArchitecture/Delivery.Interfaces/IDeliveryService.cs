using System;

namespace Delivery.Interfaces
{
    public interface IDeliveryService
    {
        decimal CalculateDeliveryCost(float weight);
    }
}
