using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;

namespace DomainServices.Services
{
    public class OrderService : IOrderService
    {
        public long Create(Order orderToCreate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(long orderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrdersByPortifolioId(long portifolioId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrdersByProductId(long productId)
        {
            throw new NotImplementedException();
        }

        public void Update(Order orderToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
