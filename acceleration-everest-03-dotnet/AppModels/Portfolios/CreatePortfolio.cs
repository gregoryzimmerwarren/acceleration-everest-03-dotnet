﻿using System.Text.Json.Serialization;

namespace AppModels.Portfolios;

public class CreatePortfolio
{
    public CreatePortfolio(
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