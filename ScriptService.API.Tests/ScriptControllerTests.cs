using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using ScriptService.API.Controllers;
using ScriptService.DataManagement;
using ScriptService.DataManagement.Mapping;
using ScriptService.DataManagement.Repository;
using ScriptService.Models.Data;
using ScriptService.Models.DTO.Script;
using ScriptService.Models.Paging;
using System.Net.Sockets;

namespace ScriptService.API.Tests
{
				public class ScriptControllerTests
				{
								[Fact]
								public async void GetTest()
								{
												//arrange
												//configuration
												var mockConfiguration = new Mock<IConfiguration>();
												var configuration = mockConfiguration.Object;

												//db context options
												var mockDbContextOptions = new Mock<DbContextOptions<ScriptDbContext>>();
												var dbContextOptions = mockDbContextOptions.Object;

												//script database
												var scripts = new List<Script>()
												{
																new Script()
																{
																				Id = 1,
																				Name = "Test1",
																				Type = ScriptType.ps1,
																				Content = "Test1"
																},
																new Script()
																{
																				Id = 2,
																				Name = "Test2",
																				Type = ScriptType.vbs,
																				Content = "Test2"
																},
																new Script()
																{
																				Id = 3,
																				Name = "Test3",
																				Type = ScriptType.sh,
																				Content = "Test3"
																},
												};
												var queriableScripts = scripts.AsQueryable();
												var mockDbSet = new Mock<DbSet<Script>>();

												mockDbSet.As<IQueryable<Script>>().Setup(m => m.Provider).Returns(queriableScripts.Provider);
												mockDbSet.As<IQueryable<Script>>().Setup(m => m.Expression).Returns(queriableScripts.Expression);
												mockDbSet.As<IQueryable<Script>>().Setup(m => m.ElementType).Returns(queriableScripts.ElementType);
												mockDbSet.As<IQueryable<Script>>().Setup(m => m.GetEnumerator()).Returns(() => queriableScripts.GetEnumerator());



												var mockDbContext = new Mock<ScriptDbContext>();
												mockDbContext.Setup(c => c.Scripts)
																								 .Returns(mockDbSet.Object);
												ScriptDbContext dbContext = mockDbContext.Object;
												//logger
												var mockLogger = new Mock<ILogger<ScriptsController>>();
												ILogger<ScriptsController> logger = mockLogger.Object;
												//mapper
												var mappingConfiguration = new MapperConfiguration(configuration =>
												{
																configuration.AddProfile(new MapperInitializer());
												});
												var mapper = mappingConfiguration.CreateMapper();

												//manageable data
												IUnitOfWork unitOfWork = new UnitOfWork(dbContext);

												ScriptsController scriptsController = new ScriptsController(logger, unitOfWork, mapper);
												//act
												var receivedScriptsActionResult = await scriptsController.Get(new RequestParams());
												var receivedScripts = (receivedScriptsActionResult as OkObjectResult)?.Value as IList<GetScriptDTO>;
												//assert
												Assert.NotNull(receivedScripts);
												Assert.NotEmpty(receivedScripts);
												Assert.Equal(scripts.Count, receivedScripts.Count);
								}

								[Fact]
								public void GetByIdTest()
								{
												Assert.Fail("So far noting here");
								}

								[Fact]
								public void PostTest()
								{
												Assert.Fail("So far noting here");
								}

								[Fact]
								public void DeleteTest()
								{
												Assert.Fail("So far noting here");
								}

								[Fact]
								public void PutTest()
								{
												Assert.Fail("So far noting here");
								}
				}
}