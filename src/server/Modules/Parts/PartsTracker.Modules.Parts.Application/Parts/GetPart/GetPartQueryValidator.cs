using FluentValidation;

namespace PartsTracker.Modules.Parts.Application.Parts.GetPart;

internal sealed class GetPartQueryValidator : AbstractValidator<GetPartQuery>
{
    public GetPartQueryValidator()
    {
        RuleFor(x => x.PartNumber)
                .NotEmpty().WithMessage("Part number is required.")
                .MaximumLength(50).WithMessage("Part number must not exceed 50 characters.");
    }
}
