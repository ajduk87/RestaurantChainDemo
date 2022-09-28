using FluentValidation;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Factories;
using RestaurantChainApp.Models.Order;
using RestaurantChainApp.Repositories;
using System.Collections.Generic;

namespace RestaurantChainApp.Validators
{
    public class OrderCreateValidator : AbstractValidator<CreateOrderModel>
    {
        public OrderCreateValidator()
        {

            RuleFor(o => o.orderItems)
                .NotEmpty()
                .Must(ValidateOrderItems)
                .WithMessage("Order must have at least one order item.");

            RuleFor(o => o.Total)
               .NotEmpty()
               .Must(ValidateOrderTotal)
               .WithMessage("Order total must be greater than zero.");
        }

        private bool ValidateOrderItems(List<OrderItemDto> orderItems)
        {
            return orderItems.Count > 0;
        }

        private bool ValidateOrderTotal(double total) 
        {
            return total > 0;
        }
    }
}
