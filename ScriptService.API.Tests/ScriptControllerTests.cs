using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using ScriptService.API.Controllers;
using ScriptService.API.Tests.Fixtures;
using ScriptService.DataManagement;
using ScriptService.DataManagement.Mapping;
using ScriptService.DataManagement.Repository;
using ScriptService.Models.Data;
using ScriptService.Models.DTO.Script;
using ScriptService.Models.Paging;
using System.Net.Sockets;

namespace ScriptService.API.Tests
{
    [Collection("Script collection")]
				public class ScriptControllerTests
				{
								private readonly ScriptFixture _fixture;
        public ScriptControllerTests(ScriptFixture fixture)
        {
												_fixture = fixture;
        }

        [Fact]
								public async void GetTest()
								{
												//arrange
												_fixture.SeedData();
												//act
												var receivedScriptsActionResult = await _fixture.Controller.Get(new RequestParams());
												var receivedScripts = (receivedScriptsActionResult as OkObjectResult)?.Value as IList<GetScriptDTO>;
												//assert
												Assert.NotNull(receivedScripts);
												Assert.NotEmpty(receivedScripts);
												Assert.Equal(_fixture.Scripts.Count, receivedScripts.Count);
												//cleanup
												_fixture.ClearData();
								}

								[Fact]
								public async void GetByIdTest()
								{
												//arrange
												_fixture.SeedData();
												//act
												var script1ActionResult = await _fixture.Controller.GetById(1);
												var script1 = (script1ActionResult as OkObjectResult)?.Value as DetailScriptDTO;
												//assert
												Assert.NotNull(script1);
												Assert.Equal(script1.Content, _fixture.Scripts.FirstOrDefault(c => c.Id == 1).Content);
												//cleanup
												_fixture.ClearData();

								}

								[Fact]
								public async void PostTest()
								{
												//arrange
												_fixture.SeedData();
												CreateScriptDTO createScriptDTO = new CreateScriptDTO()
												{
																Name = "Test4",
																Content = "Test4",
																Type = ScriptType.sh
												};
												//act
												var postActionResult = await _fixture.Controller.Post(createScriptDTO);
												//assert
												Assert.NotNull(postActionResult);
												//wip
												//cleanup
												_fixture.ClearData();
								}

								[Fact]
								public async void DeleteTest()
								{
												//arrange
												_fixture.SeedData();
												//act
												var deleteActionResult = await _fixture.Controller.Delete(2);
												//assert
												Assert.NotNull(deleteActionResult);
												Assert.IsType<AcceptedResult>(deleteActionResult);
												//cleanup
												_fixture.ClearData();
								}

								[Fact]
								public async void PutTest()
								{
												//arrange
												_fixture.SeedData();
												UpdateScriptDTO updateScriptDTO = new UpdateScriptDTO()
												{
																Content = "Updated content"
												};
												//act
												var putActionResult = await _fixture.Controller.Put(1, updateScriptDTO);
												//assert
												Assert.NotNull(putActionResult);
												Assert.IsType<AcceptedResult>(putActionResult);
												//wip
												//cleanup
												_fixture.ClearData();
								}
				}
}