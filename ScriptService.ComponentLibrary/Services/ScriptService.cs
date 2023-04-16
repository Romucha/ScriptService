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
								public ScriptManagementService(HttpClient httpClient)
								{
												_httpClient = httpClient;
								}

								public async Task<Script> AddScriptAsync(Script script, string token)
								{
												var content = new StringContent(JsonSerializer.Serialize(script), Encoding.UTF8, "application/json");
												_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
												var response = await _httpClient.PostAsync("api/scripts", content);
												if (response.IsSuccessStatusCode)
												{
																return await JsonSerializer.DeserializeAsync<Script>(response.Content.ReadAsStream(), new JsonSerializerOptions()
																{
																				PropertyNameCaseInsensitive = true
																});
												}

												return null;
								}

								public async Task DeleteScriptAsync(int id, string token)
								{
												_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
												await _httpClient.DeleteAsync($"api/scripts/{id}");
								}

								public async Task<IEnumerable<Script>> GetAllScriptsAsync(string filter = null)
								{
												Console.WriteLine("Meow");
												return await JsonSerializer.DeserializeAsync<IEnumerable<Script>>(await _httpClient.GetStreamAsync($"api/scripts"), new JsonSerializerOptions()
												{
																PropertyNameCaseInsensitive = true
												});
								}

								public async Task<Script> GetScriptByIdAsync(int id)
								{
												return await JsonSerializer.DeserializeAsync<Script>(await _httpClient.GetStreamAsync($"api/scripts/{id}"), new JsonSerializerOptions()
												{
																PropertyNameCaseInsensitive = true
												});
								}

								public async Task UpdateScriptAsync(Script script, string token)
								{
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
