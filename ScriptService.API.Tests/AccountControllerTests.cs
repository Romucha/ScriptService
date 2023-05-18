using Microsoft.AspNetCore.Mvc;
using ScriptService.API.Tests.Fixtures;
using ScriptService.Models.Data;
using ScriptService.Models.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.API.Tests
{
				public class AccountControllerTests
				{
								private AccountFixture accountFixture;
								public AccountControllerTests() 
								{
												accountFixture = new AccountFixture();
								}
								[Fact]
								public async void RegisterTest()
								{
												//arrange
												RegisterUserDTO registerUserDTO = new RegisterUserDTO()
												{
																Email = "user@test.com",
																PhoneNumber = "1234567890",
																Password = "Pa$$w0rd",
																ConfirmPassword = "Pa$$w0rd",
																Roles = new List<string>() { "User" }
												};

												//act
												var registerResult = await accountFixture.AccountController.Register(registerUserDTO);
												//assert
												Assert.NotNull(registerResult);
												Assert.IsType<AcceptedResult>(registerResult);
								}
				}
}
