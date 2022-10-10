﻿using AppModels.Orders;
using DomainModels.Enums;
using FluentValidation;
using System;

namespace AppServices.Validators.Create;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(order => order.Quotes)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(order => order.NetValue)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Net value must be more than R$0,00.");

        RuleFor(order => order.LiquidatedAt)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateTime.Now.Date);

        RuleFor(order => order.Direction)
            .NotNull()
            .Must(EnunIsValid)
            .IsInEnum();

        RuleFor(order => order.PortfolioId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(order => order.ProductId)
            .NotEmpty()
            .GreaterThan(0);
    }

    private bool EnunIsValid(int direction)
    {
        if(direction != 1 || direction != 2)
            return false;
        
        return true;
    }
}