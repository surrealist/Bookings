using FluentValidation;
using GMM.Bookings.Models.DTOs;

namespace GMM.Bookings.APIs.Validators
{
  public class NewCourseValidator : AbstractValidator<NewCourse>
  {
    public NewCourseValidator()
    {
      RuleFor(x => x.CoursePrefix)
        .NotNull()
        .MinimumLength(2)
        .MaximumLength(2)
        .WithMessage($"Course prefix must be 2 characters");

      RuleFor(x => x.Name)
        .NotNull().NotEmpty();

      RuleFor(x => x.Price)
        .GreaterThanOrEqualTo(0m);

      RuleFor(x => x.Hours)
        .GreaterThanOrEqualTo(0.0)
        .Must(x => x % 0.5 == 0.0);
    }
  }
}
