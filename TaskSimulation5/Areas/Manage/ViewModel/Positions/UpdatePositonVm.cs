using FluentValidation;

namespace TaskSimulation5.Areas.Manage.ViewModel.Positions
{
    public class UpdatePositonVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }

    }
    public class UpdatePositonValidator : AbstractValidator<UpdatePositonVm>
    {
        public UpdatePositonValidator()
        {

            RuleFor(x => x.Name).NotEmpty()
                    .NotNull().WithMessage("Adin duzgun deyil")
                    .MinimumLength(4).WithMessage("Adin uzunlugu minimum 4 ola biler");

        }

    }
}
