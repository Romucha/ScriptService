using Microsoft.AspNetCore.Components;
using MudBlazor;
using ScriptService.App.Services;

namespace ScriptService.App.Components
{
				public partial class LoginComponent
				{
								[Inject]
								private ScriptAuthenticationStateProvider scriptAuthentication { get; set; }

								[CascadingParameter]
								MudDialogInstance MudDialog { get; set; }

								private MudForm form;

								private async void loginSubmit()
								{
												await form.Validate();
												if (form.IsValid)
												{
																await scriptAuthentication.Login(new Models.DTO.User.LoginUserDTO()
																{
																				Email = scriptAuthentication.CurrentUser.Email,
																				Password = scriptAuthentication.CurrentUser.Password,
																});
																MudDialog.Close();
												}
								}

								private void loginReset()
								{
												MudDialog.Cancel();
								}
				}
}
