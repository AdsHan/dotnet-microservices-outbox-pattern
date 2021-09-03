using FluentValidation;
using MOP.Core.Commands;
using MOP.Order.Domain.Enum;
using System;
using System.Collections.Generic;

namespace MOP.Order.API.Application.Messages.Commands.OrderCommand
{

    public class CreateOrderCommand : Command
    {
        public Guid CustomerId { get; set; }
        public ShippingType Shipping { get; set; }
        public string? Observation { get; set; }
        public List<CreateOrderItemCommand> Items { get; set; }

        public override bool Validate()
        {
            BaseResult.ValidationResult = new CreateOrderValidation().Validate(this);
            return BaseResult.ValidationResult.IsValid;
        }

        public class CreateOrderValidation : AbstractValidator<CreateOrderCommand>
        {
            public CreateOrderValidation()
            {
                RuleFor(c => c.CustomerId)
                    .NotEmpty()
                    .WithMessage("O código do cliente não foi informado");

                RuleFor(c => c.Shipping)
                    .NotEmpty()
                    .WithMessage("O tipo de frete não foi informado");

                RuleFor(c => c.Observation)
                    .MaximumLength(512)
                    .WithMessage("Tamanho máximo da observação é de 8000 caracteres.");

                RuleFor(c => c.Items)
                    .Must(x => x.Count > 0)
                    .WithMessage("Não foram informados os itens");

                RuleForEach(x => x.Items).SetValidator(new ItemsValidator());

                //RuleFor(c => c.Items).Custom((list, context) =>
                //{
                //    if (list == null || list.Count == 0)
                //    {
                //        context.AddFailure("O pedido deve conter no mínimo 1 item.");
                //    }
                // });
            }
        }

        public class ItemsValidator : AbstractValidator<CreateOrderItemCommand>
        {
            public ItemsValidator()
            {
                RuleFor(c => c.ProductId)
                    .NotEmpty()
                    .WithMessage("O código do produto não foi informado");

                RuleFor(c => c.Quantity)
                    .NotEmpty()
                    .GreaterThan(0)
                    .WithMessage("A quantidade não foi informada");

                RuleFor(c => c.UnitPrice)
                    .NotEmpty()
                    .GreaterThan(0)
                    .WithMessage("O valor unitário não foi informado");

                RuleFor(c => c.Discount)
                    .Must(x => x == DiscountType.None || x == DiscountType.Percentage || x == DiscountType.Value)
                    .WithMessage("O tipo do desconto é inválido");

                RuleFor(c => c.DiscountValue)
                    .NotEmpty()
                    .WithMessage("O valor do desconto não foi informado");
            }
        }
    }

    public class CreateOrderItemCommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DiscountType Discount { get; set; }
        public decimal DiscountValue { get; set; }
    }

}