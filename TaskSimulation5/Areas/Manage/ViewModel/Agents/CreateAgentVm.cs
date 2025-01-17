using FluentValidation;
using TaskSimulation5.Areas.Manage.ViewModel.Positions;

namespace TaskSimulation5.Areas.Manage.ViewModel.Agents
{
    public class CreateAgentVm
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int PositionId { get; set; }
        public IFormFile formFile { get; set; }
    }
    public class CreateAgentValidator : AbstractValidator<CreateAgentVm>
    {
        public CreateAgentValidator()
        {

            RuleFor(x => x.Name).NotEmpty()
                    .NotNull().WithMessage("Adin duzgun deyil")
                    .MinimumLength(4).WithMessage("Adin uzunlugu minimum 4 ola biler");
            RuleFor(x => x.formFile).NotEmpty().NotNull().WithMessage("Sekil duzgun deyil");
            RuleFor(x=>x.PositionId).NotEmpty().NotNull().WithMessage("Position duzgun deyil");

        }

    }
}
