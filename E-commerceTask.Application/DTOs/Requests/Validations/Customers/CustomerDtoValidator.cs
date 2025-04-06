using E_commerceTask.Application.DTOs.Requests.Customers;
using FluentValidation;

namespace E_commerceTask.Application.DTOs.Requests.Validations.Customers
{
    public class CustomerDtoValidator :AbstractValidator<CreateCustomerDTO>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters long.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone number is required.");
        }
    }
}
