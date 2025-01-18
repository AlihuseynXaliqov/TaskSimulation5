using FluentValidation;

namespace TaskSimulation5.ModelViews
{
    public class LoginVm
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
    public class LoginValidator : AbstractValidator<LoginVm>
    {
        public LoginValidator()
        {
            RuleFor(x => x.EmailOrUsername).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
