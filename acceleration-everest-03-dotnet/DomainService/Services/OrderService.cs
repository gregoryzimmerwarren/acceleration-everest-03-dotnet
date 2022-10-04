﻿using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;

namespace DomainServices.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _repositoryFactory;

        public OrderService(
            IUnitOfWork unitOfWork, 
            IRepositoryFactory repositoryFactory)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repositoryFactory ?? (IRepositoryFactory)_unitOfWork;
        }

        public long Create(Order orderToCreate)
        {
            var repository = _unitOfWork.Repository<Order>();
            repository.Add(orderToCreate);
            _unitOfWork.SaveChanges();

            return orderToCreate.Id;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var repository = _repositoryFactory.Repository<Order>();
            var query = repository.MultipleResultQuery();
            var orders = repository.Search(query);

            if (orders.Count == 0)
                throw new ArgumentException("No order found");

            return orders;
        }

        public Order GetOrderById(long orderId)
        {
            var repository = _repositoryFactory.Repository<Order>();
            var query = repository.SingleResultQuery().AndFilter(order => order.Id == orderId);
            var order = repository.SingleOrDefault(query);

            if (order == null)
                throw new ArgumentException($"No order found for Id: {orderId}");

            return order;
        }

        public IEnumerable<Order> GetOrdersByPortifolioId(long portifolioId)
        {
            var repository = _repositoryFactory.Repository<Order>();
            var query = repository.MultipleResultQuery().AndFilter(order => order.PortifolioId == portifolioId);
            var orders = repository.Search(query);

            if (orders.Count == 0)
                throw new ArgumentException($"No order found for portfolio Id: {portifolioId}");

            return orders;
        }

        public IEnumerable<Order> GetOrdersByProductId(long productId)
        {
            var repository = _repositoryFactory.Repository<Order>();
            var query = repository.MultipleResultQuery().AndFilter(order => order.ProductId == productId);
            var orders = repository.Search(query);

            if (orders.Count == 0)
                throw new ArgumentException($"No order found for product Id: {productId}");

            return orders;
        }
    }
}