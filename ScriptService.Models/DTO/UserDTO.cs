using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models.DTO
{
	public class UserDTO
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[StringLength(15, ErrorMessage = "Your password is limited from {2} to {1} characters", MinimumLength = 6)]
		public string Password { get; set; }
	}
}
