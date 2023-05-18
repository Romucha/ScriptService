using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ScriptService.API.Controllers;
using ScriptService.API.Services;
using ScriptService.DataManagement.Mapping;
using ScriptService.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.API.Tests.Fixtures
{
				public class AccountFixture
				{
								public AccountController AccountController { get; set; }

								private UserManager<ScriptUser> userManager;

								private List<ScriptUser> scriptUsers;

								public AccountFixture() 
								{
												//script users
												scriptUsers = new List<ScriptUser>();
												//user manager
												var store = new Mock<IUserStore<ScriptUser>>().Object;
												var optionsAccessor = new Mock<IOptions<IdentityOptions>>().Object;
												var passwordHasher = new PasswordHasher<ScriptUser>();
												var userValidators = new UserValidator<ScriptUser>(); 
												var passwordValidators = new PasswordValidator<ScriptUser>();
												var keyNormalizer = new Mock<ILookupNormalizer>().Object;
												var errors = new IdentityErrorDescriber();
												var serviceProvider = new Mock<IServiceProvider>().Object;
												var logger = new Mock<ILogger<UserManager<ScriptUser>>>().Object;
												var mockUserManager = new Mock<UserManager<ScriptUser>>(store, 
																																																																optionsAccessor, 
																																																																passwordHasher, 
																																																																null, 
																																																																null, 
																																																																keyNormalizer, 
																																																																errors,
																																																																serviceProvider,
																																																																logger);

												mockUserManager.Object.UserValidators.Add(userValidators);
												mockUserManager.Object.PasswordValidators.Add(passwordValidators);
												mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<ScriptUser>())).ReturnsAsync(IdentityResult.Success);
												//mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ScriptUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<ScriptUser, string>((x, y) => scriptUsers.Add(x));
												mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<ScriptUser>())).ReturnsAsync(IdentityResult.Success);
												userManager = mockUserManager.Object;
												//logger
												var accountLogger = new Mock<ILogger<AccountController>>().Object;
												//auth manager
												var configuration = new Mock<IConfiguration>().Object;
												var authManager = new AuthManager(userManager,
																																														configuration);
												//mapper
												var mappingConfiguration = new MapperConfiguration(configuration =>
												{
																configuration.AddProfile(new MapperInitializer());
												});
												var mapper = mappingConfiguration.CreateMapper();
												AccountController = new AccountController(userManager, 
																																																						accountLogger,
																																																						authManager,
																																																						mapper);
								}
				}
}
