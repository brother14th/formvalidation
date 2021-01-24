using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UserApp.Api.Entities;
using UserApp.Api.Validators;
using Xunit;

namespace UserApp.Api.Tests
{
    public class UserFormFunctionTest
    {
        private static Mock<HttpRequest> CreateMockRequest(object body)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            var json = JsonConvert.SerializeObject(body);
            sw.Write(json);
            sw.Flush();
            ms.Position = 0;
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Body).Returns(ms);

            return mockRequest;
        }

        [Theory]
        [InlineData("firstabc", "lastxyz", "123Clementi", 17, "parentabc", "test@test.com", "http://github.com")]
        [InlineData("firstabc", "lastxyz", "123Clementi", 18, "", "test@test.com", "http://github.com")]
        [InlineData("firstabc", "lastxyz", "123Clementi", 19, "", "test@test.com", "http://github.com")]
        [InlineData("firstabc", "lastxyz", "123Clementi", 19, "parentabc", "test@test.com", "http://github.com")] 
        public async Task ValidUserFormAsync(string firstName, string lastName, string address, int age, string parentName, string email, string website)
        {
            var userForm = new Entities.UserForm
            {
               FirstName = firstName,
               LastName = lastName,
               Address = address,
               Age = age,
               ParentName = parentName,
               Email = email,
               Website = website
            };
            Mock<HttpRequest> mockRequest = CreateMockRequest(userForm);
            var result = (OkObjectResult)await UserApi.Run(mockRequest.Object,  new Mock<ILogger>().Object);
            Assert.True(((ApiValidationResult)result.Value).Type=="Success");
        }

        [Theory]
        [InlineData("firstabc", "lastxyz", "123Clementi", 17, "", "test@test.com", "http://github.com",1)]
        [InlineData("abc", "xyz", "123Clementi", 17, "", "test@test.com", "http://github.com", 3)]
        [InlineData("abc", "xyz", "adr", 0, "", "testattest.com", "mywebsite", 7)]
        public async Task InValidUserFormAsync_ReturnsError(string firstName, string lastName, string address, int age, string parentName, string email, string website, int expectedTotalErrors)
        {
            var userForm = new Entities.UserForm
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Age = age,
                ParentName = parentName,
                Email = email,
                Website = website
            };
            Mock<HttpRequest> mockRequest = CreateMockRequest(userForm);
            var result = (BadRequestObjectResult)await UserApi.Run(mockRequest.Object, new Mock<ILogger>().Object);
            Assert.True(((ApiValidationResult)result.Value).Type == "Error" && ((ApiValidationResult)result.Value).Messages.Count == expectedTotalErrors);
        }
    }
}
