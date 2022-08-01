using FluentValidation;
using TestForVersta.Models;

namespace TestForVersta.Validators;

/// <summary>
/// Represents a validator for <see cref="OrderInsertModel"/>.
/// </summary>
public class OrderInsertModelValidator : AbstractValidator<OrderInsertModel>
{
    /// <summary>
    /// Initializes a new instance of <see cref="OrderInsertModelValidator"/>.
    /// </summary>
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
