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

namespace ScriptService.API.Tests.Fixtures
{
    public class ScriptFixture : IDisposable
    {
        private readonly ScriptDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ScriptsController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public List<Script> Scripts { get; private set; }
        public ScriptsController Controller { get; private set; }
        public ScriptFixture()
        {
												//configuration
												var mockConfiguration = new Mock<IConfiguration>();
												IConfiguration configuration = mockConfiguration.Object;
												//scripts
												Scripts = new List<Script>();
            //script database
            DbContextOptionsBuilder<ScriptDbContext> optionsBuilder = new DbContextOptionsBuilder<ScriptDbContext>();
												_dbContext = new ScriptDbContext(configuration, optionsBuilder.UseInMemoryDatabase("Test script database").Options);
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
            _unitOfWork = new UnitOfWork(_dbContext);

            Controller = new ScriptsController(_logger, _unitOfWork, _mapper);
        }

								public void Dispose()
								{
												_dbContext.Dispose();
												_unitOfWork.Dispose();
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
												await _dbContext.Scripts.AddRangeAsync(Scripts);
												await _dbContext.SaveChangesAsync();
								}

								public void ClearData()
								{
												Scripts.Clear();
												_dbContext.Scripts.RemoveRange(_dbContext.Scripts);
								}
				}
}
