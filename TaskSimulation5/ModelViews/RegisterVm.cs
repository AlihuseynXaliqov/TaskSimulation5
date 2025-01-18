using FluentValidation;

namespace TaskSimulation5.ModelViews
{
    public class RegisterVm
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class RegisterValidator : AbstractValidator<RegisterVm>
    {
        public RegisterValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().NotNull().MinimumLength(3).WithMessage("Adin minimum uzunluqu 3 olmalidir");
            RuleFor(x => x.Username).NotEmpty().NotNull().MinimumLength(3).WithMessage("Adin minimum uzunluqu 3 olmalidir");
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Emaili duzgun daxil et");
            RuleFor(x => x.Password).NotEmpty().NotNull()
                .MinimumLength(8).WithMessage("Parolun uzunluqu min 8 olmalidi")
                .Matches("[A-Z]").WithMessage("Parolda minimum bir boyuk herf olmalidi")
                .Matches("[a-z]").WithMessage("Parolda minimum bir kicik herf olmalidi")
                .Matches("[0-9]").WithMessage("Parolda minimum bir reqem olmalidi")
                .Matches("^[A-Za-z0-9]").WithMessage("Parolda minimum bir reqem olmalidi");
            RuleFor(x => x).Must(x => x.Password == x.ConfirmPassword).WithMessage("Parollar eyni deyil");




        }
    }
}
