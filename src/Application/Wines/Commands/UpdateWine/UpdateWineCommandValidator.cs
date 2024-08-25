namespace BrainwaveBandits.WinerR.Application.Wines.Commands.UpdateWine;

public class UpdateWineCommandValidator : AbstractValidator<UpdateWineCommand>
{
    public UpdateWineCommandValidator()
    {
        RuleFor(v => v.WineID)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Amount)
            .GreaterThan(0);
    }
}
