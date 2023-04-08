using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models.Data
{
    public class ScriptUser : IdentityUser
    {
								public bool IsAuthenticated { get; set; }
								public Dictionary<string, string> Claims { get; set; }
				}
}
