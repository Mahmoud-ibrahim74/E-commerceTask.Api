using E_commerceTask.Application.DTOs.Requests.Orders;
// No changes needed in this file as the validator is already correctly implemented.
using FluentValidation;

namespace E_commerceTask.Application.DTOs.Requests.Validations.Orders
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("CustomerId is required")
                .NotEqual(0)
                .WithMessage("CustomerId cannot be 0");

            RuleFor(x => x.ProductIds)
                .NotEmpty()
                .WithMessage("ProductIds is required")
                .Must(x => x.Count > 0)
                .WithMessage("ProductIds cannot be empty")
                .Must(x => x.All(id => id != 0))
                .WithMessage("ProductIds cannot contain 0");
        }
    }
}
