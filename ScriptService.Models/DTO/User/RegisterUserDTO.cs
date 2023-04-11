using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models.DTO.User
{
    public class RegisterUserDTO : LoginUserDTO
    {
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public ICollection<string> Roles { get; set; }

								[Compare(nameof(Password))]
								public string ConfirmPassword { get; set; }
    }
}
