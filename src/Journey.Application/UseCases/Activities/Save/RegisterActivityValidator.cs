using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Activities.Save;

public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
{
    public RegisterActivityValidator()
    {
        RuleFor(request => request.Name).NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_EMPTY);
        
    }
}