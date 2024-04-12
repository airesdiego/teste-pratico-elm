using FluentValidation;
using elumini.domain.Task.UseCases.Requests;

namespace elumini.application.Task.Validators;

public class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskValidator() 
    {
        RuleFor(m => m.Name)
       .NotEmpty()
       .WithMessage("The 'name' field is required");

        RuleFor(m => m.Description)
       .NotEmpty()
       .WithMessage("The 'description' field is required");

        RuleFor(m => m.EndAt)
       .NotEmpty()
       .WithMessage("The 'endAt' field is required")
       .GreaterThan(m => DateTime.Now.AddDays(-1))
       .WithMessage("The 'endAt' must be greater than yesterday");
    }
}
