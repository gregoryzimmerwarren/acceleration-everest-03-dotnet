﻿using Bogus;
using DomainModels.Models;
using System.Collections.Generic;

namespace DomainServices.Tests.Fixtures;

public class PortfolioProductFixture
{
    public static PortfolioProduct GeneratePortfolioProductFixture()
    {
        var testPortfolioProduct = new Faker<PortfolioProduct>("pt_BR")
            .CustomInstantiator(faker => new PortfolioProduct(
                portfolioId: 1,
                productId: 1));

        var portfolioProduct = testPortfolioProduct.Generate();
        return portfolioProduct;
    }

    public static IList<PortfolioProduct> GenerateListPortfolioProductFixture(int generatedQuantity)
    {
        var testPortfolioProduct = new Faker<PortfolioProduct>("pt_BR")
            .CustomInstantiator(faker => new PortfolioProduct(
                portfolioId: 1,
                productId: 1));

        var portfolioProduct = testPortfolioProduct.Generate(generatedQuantity);
        return portfolioProduct;
    }
}