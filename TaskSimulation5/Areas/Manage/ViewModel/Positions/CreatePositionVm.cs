using FluentValidation;

namespace TaskSimulation5.Areas.Manage.ViewModel.Positions
{
    public class CreatePositionVm
    {
        public string? Name { get; set; }

    }
    public class CreatePositionValidator : AbstractValidator<CreatePositionVm>
    {
        public CreatePositionValidator()
        {

            RuleFor(x => x.Name).NotEmpty()
                    .NotNull().WithMessage("Adin duzgun deyil")
                    .MinimumLength(4).WithMessage("Adin uzunlugu minimum 4 ola biler");

        }

    }
}
