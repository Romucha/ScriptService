using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models.DTO.User
{
    public class AuthenticatedUserDTO : RegisterUserDTO
    {
        public bool IsAuthenticated { get; set; }
    }
}
