using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UserApp.Api.Entities;

namespace UserApp.Api.Validators
{
    public class UserFormValidator: AbstractValidator<UserForm>
    {

        public UserFormValidator()
        {
            
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotEmpty().Matches(ValidatorRegEx.PersonName)
                .WithMessage(String.Format(ValidatorMessage.AlphabeticalMsg, "'First Name'") + " " + string.Format(ValidatorMessage.MinMaxLengthMsg,5,10));

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop).NotEmpty().Matches(ValidatorRegEx.PersonName)
                .WithMessage(String.Format(ValidatorMessage.AlphabeticalMsg, "'Last Name'") + " " + string.Format(ValidatorMessage.MinMaxLengthMsg, 5, 10));

            RuleFor(x => x.Address).Cascade(CascadeMode.Stop).NotEmpty().Matches(ValidatorRegEx.Address)
                .WithMessage(String.Format(ValidatorMessage.AlphaNumericMsg, "'Address'") + " "  + string.Format(ValidatorMessage.MinMaxLengthMsg, 10, 100));

            RuleFor(x => x.Age).Cascade(CascadeMode.Stop).NotEmpty().ExclusiveBetween(0,150); //0 < age < 150

            RuleFor(x => x.ParentName).Cascade(CascadeMode.Stop).NotEmpty().Matches(ValidatorRegEx.PersonName).When(x => x.Age < 18||!String.IsNullOrEmpty(x.ParentName))
                .WithMessage(String.Format(ValidatorMessage.AlphabeticalMsg, "'Parent Name'") + " "  + string.Format(ValidatorMessage.MinMaxLengthMsg, 5, 10));

            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(150).EmailAddress();

            RuleFor(x => x.Website).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(2048).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage(String.Format(ValidatorMessage.URLMsg,"'Website'"));
        }
    }
}
