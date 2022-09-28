using FluentValidation;
using Npgsql;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Factories;
using RestaurantChainApp.Models.Order;
using RestaurantChainApp.Repositories;
using System.Collections.Generic;

namespace RestaurantChainApp.Validators
{
    public class OrderUpdateValidator : AbstractValidator<UpdateOrderModel>
    {
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;
        private readonly IRepositoryFactory repositoryFactory;

        private readonly OrdersRepository ordersRepository;

        public OrderUpdateValidator(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.databaseConnectionFactory = databaseConnectionFactory;

            ordersRepository = repositoryFactory.CreateOrdersRepository();

            RuleFor(o => o.Id)
             .NotEmpty()
             .Must(ValidateOrderId)
             .WithMessage("Order specified doesn't exist in the database");

            RuleFor(o => o.orderItems)
               .NotEmpty()
               .Must(ValidateOrderItems)
               .WithMessage("Order must have at least one order item.");

            RuleFor(o => o.Total)
               .NotEmpty()
               .Must(ValidateOrderTotal)
               .WithMessage("Order total must be greater than zero.");
        }

        private bool ValidateOrderId(int id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Create())
            {
                bool exists = ordersRepository.Exists(connection, id);
                return exists;
            }
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
