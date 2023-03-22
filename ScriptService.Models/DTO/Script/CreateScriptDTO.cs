using ScriptService.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models.DTO.Script
{
    public class CreateScriptDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public ScriptType Type { get; set; }

        [ScriptContentMaxSize(150, ErrorMessage = "Script content is too big.")]
        public string Content { get; set; }
    }
}
