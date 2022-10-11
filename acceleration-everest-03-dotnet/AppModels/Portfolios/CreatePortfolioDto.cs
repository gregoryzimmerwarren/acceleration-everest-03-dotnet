﻿namespace AppModels.Portfolios;

public class CreatePortfolioDto
{
    public CreatePortfolioDto(
        string name, 
        string description,
        long customerId)
    {
        Name = name;
        Description = description;
        CustomerId = customerId;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public long CustomerId { get; set; }
}