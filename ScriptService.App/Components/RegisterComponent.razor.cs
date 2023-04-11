using Microsoft.AspNetCore.Components;
using MudBlazor;
using ScriptService.App.Services;
using ScriptService.Models.DTO.User;

namespace ScriptService.App.Components
{
    public partial class RegisterComponent
    {
								[Inject]
								private ScriptAuthenticationStateProvider scriptAuthentication { get; set; }

								[CascadingParameter]
								MudDialogInstance MudDialog { get; set; }

								private MudForm form;

								private RegisterUserDTO user { get; set; } = new RegisterUserDTO() { Roles = new List<string> { "User" } };

								private async void registerSubmit()
								{
												await form.Validate();
												if (form.IsValid)
												{
																await scriptAuthentication.Register(user);
																MudDialog.Close();
												}
								}

								private void registerReset()
								{
												MudDialog.Cancel();
								}
				}
}
