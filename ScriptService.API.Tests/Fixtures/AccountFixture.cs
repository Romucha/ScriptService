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

								public AccountFixture() 
								{
												//user manager
												var store = new Mock<IUserStore<ScriptUser>>();
												var optionsAccessor = new Mock<IOptions<IdentityOptions>>();
												var passwordHasher = new PasswordHasher<ScriptUser>();
												var userValidators = new Mock<IEnumerable<IUserValidator<ScriptUser>>>();
												var passwordValidators = new Mock<IEnumerable<IPasswordValidator<ScriptUser>>>();
												var keyNormalizer = new Mock<ILookupNormalizer>();
												var errors = new IdentityErrorDescriber();
												var serviceProvider = new Mock<IServiceProvider>();
												var logger = new Mock<ILogger<UserManager<ScriptUser>>>();
												UserManager<ScriptUser> userManager = new UserManager<ScriptUser>(store.Object, 
																																																																														optionsAccessor.Object, 
																																																																														passwordHasher, 
																																																																														userValidators.Object, 
																																																																														passwordValidators.Object, 
																																																																														keyNormalizer.Object, 
																																																																														errors,
																																																																														serviceProvider.Object,
																																																																														logger.Object);
												//logger
												var accountLogger = new Mock<ILogger<AccountController>>();
												//auth manager
												var configuration = new Mock<IConfiguration>();
												var authManager = new AuthManager(userManager,
																																														configuration.Object);
												//mapper
												var mappingConfiguration = new MapperConfiguration(configuration =>
												{
																configuration.AddProfile(new MapperInitializer());
												});
												var mapper = mappingConfiguration.CreateMapper();
												AccountController = new AccountController(userManager, 
																																																						accountLogger.Object,
																																																						authManager,
																																																						mapper);
								}
				}
}
