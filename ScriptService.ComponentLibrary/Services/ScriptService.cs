using Microsoft.Extensions.Logging;
using ScriptService.Models.Data;
using ScriptService.Models.DTO.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScriptService.ComponentLibrary.Services
{
				public class ScriptManagementService : IScriptManagementService
				{
								private HttpClient _httpClient;
								private ILogger<ScriptManagementService> _logger;
								public ScriptManagementService(HttpClient httpClient, ILogger<ScriptManagementService> logger)
								{
												_httpClient = httpClient;
												_logger = logger;
								}

								public async Task<DetailScriptDTO> AddScriptAsync(DetailScriptDTO script, string token)
								{
												_logger.LogInformation("Attempt to add a new script");
												var content = new StringContent(JsonSerializer.Serialize(script), Encoding.UTF8, "application/json");
												_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
												var response = await _httpClient.PostAsync("api/scripts", content);
												if (response.IsSuccessStatusCode)
												{
																return await JsonSerializer.DeserializeAsync<DetailScriptDTO>(response.Content.ReadAsStream(), new JsonSerializerOptions()
																{
																				PropertyNameCaseInsensitive = true
																});
												}

												return null;
								}

								public async Task DeleteScriptAsync(int id, string token)
								{
												_logger.LogInformation($"Attempt to delete the script with id: {id}");
												_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
												await _httpClient.DeleteAsync($"api/scripts/{id}");
								}

								public async Task<IEnumerable<GetScriptDTO>> GetAllScriptsAsync(string filter = null)
								{
												_logger.LogInformation($"Attempt to get all scripts");
												return await JsonSerializer.DeserializeAsync<IEnumerable<GetScriptDTO>>(await _httpClient.GetStreamAsync($"api/scripts"), new JsonSerializerOptions()
												{
																PropertyNameCaseInsensitive = true
												});
								}

								public async Task<DetailScriptDTO> GetScriptByIdAsync(int id)
								{
												_logger.LogInformation($"Attempt to get the script with id: {id}");
												return await JsonSerializer.DeserializeAsync<DetailScriptDTO>(await _httpClient.GetStreamAsync($"api/scripts/{id}"), new JsonSerializerOptions()
												{
																PropertyNameCaseInsensitive = true
												});
								}

								public async Task UpdateScriptAsync(DetailScriptDTO script, string token)
								{
												_logger.LogInformation($"Attempt to update the script with id: {script.Id}");
												var content = JsonContent.Create(new UpdateScriptDTO()
												{
																Content = script.Content,
																Name = script.Name,
																Type = script.Type,
																UpdatedAt = DateTime.UtcNow
												});
												_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
												
												var response = await _httpClient.PutAsync($"api/scripts/{script.Id}", content);
								}
				}
}
