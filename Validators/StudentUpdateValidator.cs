using FluentValidation;
using WebStarterDBApp.DTO;

namespace WebStarterDBApp.Validators
{
    public class StudentUpdateValidator : AbstractValidator<StudentUpdateDTO>
    {
        public StudentUpdateValidator()
        {
            RuleFor(s => s.Firstname)
               .NotEmpty()
               .WithMessage("Το πεδίο 'Όνομα' δεν μπορεί να είναι κενό")
               .Length(2, 50)
               .WithMessage("Το πεδίο 'Όνομα' πρέπει να είναι μεταξύ 2-50 χαρακτήρες.");

            RuleFor(s => s.Lastname)
                .NotEmpty()
                .WithMessage("Το πεδίο 'Επώνυμο' δεν μπορεί να είναι κενό")
                .Length(2, 50)
                .WithMessage("Το πεδίο 'Επώνυμο' πρέπει να είναι μεταξύ 2-50 χαρακτήρες.");
        }
    }
}
