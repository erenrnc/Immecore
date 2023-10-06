using FluentValidation;

namespace ImmedisWeb.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email).NotNull();
            RuleFor(x => x.Password).NotNull();
            RuleFor(x => x.Password).Length(3, 6);
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
