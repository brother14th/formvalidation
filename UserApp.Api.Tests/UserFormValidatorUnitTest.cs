using FluentValidation.Results;
using FluentValidation.TestHelper;
using System;
using UserApp.Api.Entities;
using UserApp.Api.Validators;
using Xunit;

namespace UserApp.Api.Tests
{
    public class UserFormValidatorUnitTest
    {

        [Theory]
        [InlineData(null, "'First Name' must not be empty.")]
        [InlineData("", "'First Name' must not be empty.")]
        [InlineData(" ", "'First Name' must not be empty.")]
        [InlineData("abc123", "'First Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")]
        [InlineData("abc", "'First Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")]
        [InlineData("abcdefghijklmn", "'First Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")]
        public void InvalidFirstName_ReturnsError(string firstName, string expected)
        {
            var validator = new UserFormValidator();
            validator.ShouldHaveValidationErrorFor(x=> x.FirstName, firstName).WithErrorMessage(expected);
        }
       
        [Theory]
        [InlineData("abcde")]
        [InlineData("abcdefg")]
        [InlineData("abcdefghij")]
        public void ValidFirstName(string firstName)
        {
            var validator = new UserFormValidator();
            validator.ShouldNotHaveValidationErrorFor(x => x.FirstName, firstName);
        }

        [Theory]
        [InlineData(null, "'Last Name' must not be empty.")]
        [InlineData("", "'Last Name' must not be empty.")]
        [InlineData(" ", "'Last Name' must not be empty.")]
        [InlineData("abc123", "'Last Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")]
        [InlineData("abc", "'Last Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")]
        [InlineData("abcdefghijklmn", "'Last Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")]
        public void InvalidLastName_ReturnsError(string LastName, string expected)
        {
            var validator = new UserFormValidator();
            validator.ShouldHaveValidationErrorFor(x => x.LastName, LastName).WithErrorMessage(expected);
        }
       
        [Theory]
        [InlineData("abcde")]
        [InlineData("abcdefg")]
        [InlineData("abcdefghij")]
        public void ValidLastName(string LastName)
        {
            var validator = new UserFormValidator();
            validator.ShouldNotHaveValidationErrorFor(x => x.LastName, LastName);
        }

        [Theory]
        [InlineData(null, "'Address' must not be empty.")]
        [InlineData("", "'Address' must not be empty.")]
        [InlineData(" ", "'Address' must not be empty.")]
        [InlineData("abcdefghi", "'Address' should only contain alphanumeric characters! Min. length is 10 and max. length is 100!")]  //9 chars
        [InlineData("123abcdefghi>", "'Address' should only contain alphanumeric characters! Min. length is 10 and max. length is 100!")] //contains special chars
        [InlineData("wOfteF0qBg2Jsyu6N01dUY0fKcukzyQb5yYARziftGcVpHuKciOCJNTCvWvthxIXAvd2qHavX7r6p35yrxTh6EkaOPSSmmvMo4wXI", "'Address' should only contain alphanumeric characters! Min. length is 10 and max. length is 100!")] //101 chars
        public void InvalidAddress_ReturnsError(string address, string expected)
        {
            var validator = new UserFormValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Address, address).WithErrorMessage(expected);
        }

        [Theory]
        [InlineData("123abcdefg")]  //10 chars
        [InlineData("123abcdefghijklmn")] 
        [InlineData("wOfteF0qBg2Jsyu6N01dUY0fKcukzyQb5yYARziftGcVpHuKciOCJNTCvWvthxIXAvd2qHavX7r6p35yrxTh6EkaOPSSmmvMo4wX")] //100 chars
        public void ValidAddress(string address)
        {
            var validator = new UserFormValidator();
            validator.ShouldNotHaveValidationErrorFor(x => x.Address, address);
        }

        [Theory]
        [InlineData(null, "'Age' must not be empty.")]
        [InlineData(0, "'Age' must be between 0 and 150 (exclusive). You entered 0.")]
        [InlineData(150, "'Age' must be between 0 and 150 (exclusive). You entered 150.")]
        public void InvalidAge_ReturnsError(int? age, string expected)
        {
            var validator = new UserFormValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Age, age).WithErrorMessage(expected);
        }

        [Theory]
        [InlineData(1)]  
        [InlineData(20)]
        [InlineData(149)] 
        public void ValidAge(int age)
        {
            var validator = new UserFormValidator();
            validator.ShouldNotHaveValidationErrorFor(x => x.Age, age);
        }

        [Theory]
        [InlineData(17, "", "'Parent Name' must not be empty.")]
        [InlineData(17, "abc123", "'Parent Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")] 
        [InlineData(17, "abc", "'Parent Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")] 
        [InlineData(17, "abcdefghijklmn", "'Parent Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")]
        //validate parent name if it's not empty for age >=18
        [InlineData(18, "abc123", "'Parent Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")]
        [InlineData(18, "abc", "'Parent Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")]
        [InlineData(18, "abcdefghijklmn", "'Parent Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!")]
        public void InvalidParentName_ReturnsError(int age, string ParentName, string expected)
        {
            var validator = new UserFormValidator();
            validator.ShouldHaveValidationErrorFor(x=>x.ParentName,
                new UserForm 
                { 
                    Age = age,
                    ParentName = ParentName
                }).WithErrorMessage(expected);
        }

        [Theory]
        [InlineData("abcde")]
        [InlineData("abcdefg")]
        [InlineData("abcdefghij")]
        public void ValidParentName(string parentName)
        {
            var validator = new UserFormValidator();
            validator.ShouldNotHaveValidationErrorFor(x => x.ParentName, parentName);
        }

        [Theory]
        [InlineData(null, "'Email' must not be empty.")]
        [InlineData("", "'Email' must not be empty.")]
        [InlineData(" ", "'Email' must not be empty.")]
        [InlineData("testAtGmail.com", "'Email' is not a valid email address.")]
        public void InvalidEmail_ReturnsError(string email, string expected)
        {
            var validator = new UserFormValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Email, email).WithErrorMessage(expected);
        }

        [Theory]
        [InlineData("test@test.com")]
        public void ValidEmail(string email)
        {
            var validator = new UserFormValidator();
            validator.ShouldNotHaveValidationErrorFor(x => x.Email, email);
        }

        [Theory]
        [InlineData(null, "'Website' must not be empty.")]
        [InlineData("", "'Website' must not be empty.")]
        [InlineData(" ", "'Website' must not be empty.")]
        [InlineData("Mywebsite", "'Website' URL is invalid!")]
        public void InvalidWebsite_ReturnsError(string Website, string expected)
        {
            var validator = new UserFormValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Website, Website).WithErrorMessage(expected);
        }

        [Theory]
        [InlineData("http://www.google.com")]
        public void ValidWebsite(string website)
        {
            var validator = new UserFormValidator();
            validator.ShouldNotHaveValidationErrorFor(x => x.Website, website);
        }

        [Theory]
        [InlineData("firstabc", "lastxyz", "123Clementi", 17, "parentabc", "test@test.com", "http://github.com")]
        [InlineData("firstabc", "lastxyz", "123Clementi", 18, "", "test@test.com", "http://github.com")]
        [InlineData("firstabc", "lastxyz", "123Clementi", 19, "", "test@test.com", "http://github.com")]
        [InlineData("firstabc", "lastxyz", "123Clementi", 19, "parentabc", "test@test.com", "http://github.com")] 
        public void ValidUserForm(string firstName, string lastName, string address, int age, string parentName, string email, string website)
        {
            var validator = new UserFormValidator();
            ValidationResult validationResult = validator.Validate(new Entities.UserForm
            {
               FirstName = firstName,
               LastName = lastName,
               Address = address,
               Age = age,
               ParentName = parentName,
               Email = email,
               Website = website
            });
            Assert.True(validationResult.IsValid);
        }
    }
}
