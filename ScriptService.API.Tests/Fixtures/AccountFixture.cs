using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

								private List<ScriptUser> scriptUsers;

								public AccountFixture() 
								{
												//script users
												scriptUsers = new List<ScriptUser>();
												//user manager
												var store = new Mock<IUserStore<ScriptUser>>();
												var optionsAccessor = new Mock<IOptions<IdentityOptions>>();
												var passwordHasher = new PasswordHasher<ScriptUser>();
												var userValidators = new UserValidator<ScriptUser>(); 
												var passwordValidators = new PasswordValidator<ScriptUser>();
												var keyNormalizer = new Mock<ILookupNormalizer>();
												var errors = new IdentityErrorDescriber();
												var serviceProvider = new Mock<IServiceProvider>();
												var logger = new Mock<ILogger<UserManager<ScriptUser>>>();
												var userManager = new Mock<UserManager<ScriptUser>>(store.Object, 
																																																																optionsAccessor.Object, 
																																																																passwordHasher, 
																																																																null, 
																																																																null, 
																																																																keyNormalizer.Object, 
																																																																errors,
																																																																serviceProvider.Object,
																																																																logger.Object);

												userManager.Object.UserValidators.Add(userValidators);
												userManager.Object.PasswordValidators.Add(passwordValidators);
												userManager.Setup(x => x.DeleteAsync(It.IsAny<ScriptUser>())).ReturnsAsync(IdentityResult.Success);
												userManager.Setup(x => x.CreateAsync(It.IsAny<ScriptUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<ScriptUser, string>((x, y) => scriptUsers.Add(x));
												userManager.Setup(x => x.UpdateAsync(It.IsAny<ScriptUser>())).ReturnsAsync(IdentityResult.Success);
												//logger
												var accountLogger = new Mock<ILogger<AccountController>>();
												//auth manager
												var configuration = new Mock<IConfiguration>();
												var authManager = new AuthManager(userManager.Object,
																																														configuration.Object);
												//mapper
												var mappingConfiguration = new MapperConfiguration(configuration =>
												{
																configuration.AddProfile(new MapperInitializer());
												});
												var mapper = mappingConfiguration.CreateMapper();
												AccountController = new AccountController(userManager.Object, 
																																																						accountLogger.Object,
																																																						authManager,
																																																						mapper);
								}
				}
}
