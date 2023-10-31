using FluentValidation;

namespace Learning.API.Validators
{
    public class AddWalkRequestValidator :AbstractValidator<Models.DTO.AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).NotEmpty();
        }
    }
}
