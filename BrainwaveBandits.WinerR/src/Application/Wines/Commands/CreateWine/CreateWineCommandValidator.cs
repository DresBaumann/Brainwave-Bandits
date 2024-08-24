namespace BrainwaveBandits.WinerR.Application.Wines.Commands.CreateWine;

public class CreateWineCommandValidator : AbstractValidator<CreateWineCommand>
{
    public CreateWineCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Amount)
            .GreaterThan(0);
    }
}
