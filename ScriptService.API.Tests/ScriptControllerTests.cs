using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
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
        public ScriptControllerTests()
        {
												_fixture = new ScriptFixture();
        }

        [Fact]
								public async void GetTest()
								{
												//arrange
												_fixture.Seed();
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
												_fixture.Seed();
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
												_fixture.Seed();
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
												_fixture.Seed();
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
												_fixture.Seed();
												UpdateScriptDTO updateScriptDTO = new UpdateScriptDTO()
												{
																Content = "Updated content"
												};
												int scriptId = 3;
												//act
												var putActionResult = await _fixture.Controller.Put(scriptId, updateScriptDTO);
												var updatedScript = (await _fixture.Controller.GetById(scriptId) as OkObjectResult)?.Value as DetailScriptDTO;
												//assert
												Assert.NotNull(putActionResult);
												Assert.NotNull(updatedScript);
												Assert.IsType<AcceptedResult>(putActionResult);
												Assert.Equal(updateScriptDTO.Content, updatedScript.Content);
												//wip
												//cleanup
												_fixture.ClearData();
								}
				}
}