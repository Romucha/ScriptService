using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ScriptService.API.Controllers;
using ScriptService.DataManagement.Repository;
using ScriptService.DataManagement;
using ScriptService.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ScriptService.DataManagement.Mapping;
using System.Configuration;

namespace ScriptService.API.Tests.Fixtures
{
    public class ScriptFixture : IDisposable
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ScriptsController> _logger;
								private readonly IConfiguration _configuration;

								private ScriptDbContext _dbContext;
								private IUnitOfWork _unitOfWork 
								{ 
												get 
												{
																if (_configuration == null)
																{
																				return null;
																}
																DbContextOptionsBuilder<ScriptDbContext> optionsBuilder = new DbContextOptionsBuilder<ScriptDbContext>();
																_dbContext = new ScriptDbContext(_configuration, optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
																return new UnitOfWork(_dbContext);
												}
								}
								private bool disposed = false;

								public List<Script> Scripts { get; private set; }
        public ScriptsController Controller { get; private set; }
        public ScriptFixture()
        {
												//configuration
												var mockConfiguration = new Mock<IConfiguration>();
												_configuration = mockConfiguration.Object;
												//scripts
												Scripts = new List<Script>();
            //script database
            
            //logger
            var mockLogger = new Mock<ILogger<ScriptsController>>();
            _logger = mockLogger.Object;
            //mapper
            var mappingConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new MapperInitializer());
            });
            _mapper = mappingConfiguration.CreateMapper();

            //manageable data
            Controller = new ScriptsController(_logger, _unitOfWork, _mapper);
        }

								~ScriptFixture() 
								{
												Dispose(false);
								}

								protected virtual void Dispose(bool disposing)
								{
												if (disposed) return;
												if (disposing)
												{
																_dbContext.Dispose();
																_unitOfWork.Dispose();
												}

												disposed = true;
								}

								public void Dispose()
								{
												Dispose(true);
												GC.SuppressFinalize(this);
								}

								public async void SeedData()
								{
												Scripts = new List<Script>()
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
												_dbContext.Scripts.AddRange(Scripts);
												await _dbContext.SaveChangesAsync();
								}

								public async void ClearData()
								{
												Scripts.Clear();
												_dbContext.Scripts.RemoveRange(_dbContext.Scripts);
												await _dbContext.SaveChangesAsync();
								}
				}
}
