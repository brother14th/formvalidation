using System;
using System.Collections.Generic;
using System.Text;

namespace UserApp.Api.Validators
{
    public static class ValidatorMessage
    {
        public const string AlphabeticalMsg= "{0} should only contain alphabetical characters!";
        public const string AlphaNumericMsg = "{0} should only contain alphanumeric characters!";
        public const string MinMaxLengthMsg = "Min. length is {0} and max. length is {1}!";
        public const string URLMsg = "{0} URL is invalid!";
    }

    public static class ValidatorRegEx
    {
        public const string PersonName = "^[a-zA-Z]{5,10}$";
        public const string Address = "^[0-9a-zA-Z]{10,100}$";
    }

    public class ApiValidationResult
    {
        public String Type { get; set; }
        public List<ApiValidationResultDetail> Messages { get; set; }
    }
    public class ApiValidationResultDetail
    {
        public String Field { get; set; }
        public String Error { get; set; }
    }
}
