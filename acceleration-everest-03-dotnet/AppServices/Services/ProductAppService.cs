﻿using AppModels.Products;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;

namespace AppServices.Services;

public class ProductAppService : IProductAppService
{
    private readonly IPortfolioProductService _portfolioProductService;
    private readonly IPortfolioService _portfolioService;
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public ProductAppService(
        IPortfolioProductService portfolioProductService,
        IPortfolioService portfolioService,
        IProductService productService,
        IOrderService orderService,
        IMapper mapper)
    {
        _portfolioProductService = portfolioProductService ?? throw new System.ArgumentNullException(nameof(portfolioProductService));
        _portfolioService = portfolioService ?? throw new System.ArgumentNullException(nameof(portfolioService));
        _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        _orderService = orderService ?? throw new System.ArgumentNullException(nameof(orderService));
        _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
    }

    public long Create(CreateProduct createProductDto)
    {
        var mappedProduct = _mapper.Map<Product>(createProductDto);
        mappedProduct.DaysToExpire = (mappedProduct.ExpirationAt.Subtract(mappedProduct.IssuanceAt)).Days;

        return _productService.Create(mappedProduct);
    }

    public void Delete(long productId)
    {
        _productService.Delete(productId);
    }

    public IEnumerable<ProductResult> GetAllProducts()
    {
        var products = _productService.GetAllProducts();

        foreach (Product product in products)
        {
            IEnumerable<PortfolioProduct> portfoliosproducts;

            try
            {
                portfoliosproducts = _portfolioProductService.GetPortfolioProductByProductId(product.Id);
            }
            catch (ArgumentException)
            {
                continue;
            }

            List<Portfolio> portfolios = new();

            foreach (PortfolioProduct portfolioproduct in portfoliosproducts)
            {
                try
                {
                    var portfolio = _portfolioService.GetPortfolioById(portfolioproduct.PortfolioId);

                    portfolios.Add(portfolio);
                }
                catch (ArgumentException)
                {
                    continue;
                }
            }

            product.Portfolios = _mapper.Map<List<Portfolio>>(portfolios);

            try
            {
                var orders = _orderService.GetOrdersByPortfolioId(product.Id);
                product.Orders = _mapper.Map<List<Order>>(orders);
            }
            catch (ArgumentException)
            {
                product.Orders = new List<Order>();
            }
        }

        return _mapper.Map<IEnumerable<ProductResult>>(products);
    }

    public ProductResult GetProductById(long productId)
    {
        var product = _productService.GetProductById(productId);

        try
        {
            var portfoliosproducts = _portfolioProductService.GetPortfolioProductByProductId(product.Id);

            List<Portfolio> portfolios = new();

            foreach (PortfolioProduct portfolioproduct in portfoliosproducts)
            {
                try
                {
                    var portfolio = _portfolioService.GetPortfolioById(portfolioproduct.PortfolioId);

                    portfolios.Add(portfolio);
                }
                catch (ArgumentException)
                {
                    continue;
                }
            }

            product.Portfolios = _mapper.Map<List<Portfolio>>(portfolios);

            try
            {
                var orders = _orderService.GetOrdersByPortfolioId(product.Id);
                product.Orders = _mapper.Map<List<Order>>(orders);
            }
            catch (ArgumentException)
            {
                product.Orders = new List<Order>();
            }

            return _mapper.Map<ProductResult>(product);
        }
        catch (ArgumentException)
        {
            product.Portfolios = new List<Portfolio>();
            product.Orders = new List<Order>();

            return _mapper.Map<ProductResult>(product);
        }
    }

    public void Update(long productId, UpdateProduct updateProductDto)
    {
        var mappedProduct = _mapper.Map<Product>(updateProductDto);
        mappedProduct.Id = productId;

        _productService.Update(mappedProduct);
    }
}