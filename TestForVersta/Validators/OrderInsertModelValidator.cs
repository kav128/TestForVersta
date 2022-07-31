using FluentValidation;
using TestForVersta.Models;

namespace TestForVersta.Validators;

public class OrderInsertModelValidator : AbstractValidator<OrderInsertModel>
{
    public OrderInsertModelValidator()
    {
        RuleFor(model => model.SenderCity).NotEmpty().MaximumLength(20);
        RuleFor(model => model.SenderAddress).NotEmpty().MaximumLength(64);
        RuleFor(model => model.ReceiverCity).NotEmpty().MaximumLength(20);
        RuleFor(model => model.ReceiverAddress).NotEmpty().MaximumLength(64);
        RuleFor(model => model.Weight).GreaterThan(0);
        RuleFor(model => model.DeliveryDate).NotEmpty().GreaterThanOrEqualTo(new DateTime(2020, 1, 1));
    }
}
