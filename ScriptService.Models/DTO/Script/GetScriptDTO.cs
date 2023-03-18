using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models.DTO.Script
{
    public class GetScriptDTO : CreateScriptDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
