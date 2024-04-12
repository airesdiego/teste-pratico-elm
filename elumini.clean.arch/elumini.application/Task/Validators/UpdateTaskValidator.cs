using FluentValidation;
using elumini.domain.Task.UseCases.Requests;

namespace elumini.application.Task.Validators;

public class UpdateTaskValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskValidator() 
    {
        RuleFor(m => m.Name)
       .NotEmpty()
       .WithMessage("The 'name' field is required");

        RuleFor(m => m.Description)
       .NotEmpty()
       .WithMessage("The 'description' field is required");

       // RuleFor(m => m.StatusId)
       //.NotEmpty()
       //.WithMessage("The 'status' field is required")
       //.GreaterThan(1)
       //.LessThan(5)
       //.WithMessage("Max. status must be between 2 to 4");

        RuleFor(m => m.EndAt)
       .NotEmpty()
       .WithMessage("The 'endAt' field is required")
       .GreaterThan(m => DateTime.Today.AddDays(-1))
       .WithMessage("The 'endAt' must be greater than yesterday");
    }
}
