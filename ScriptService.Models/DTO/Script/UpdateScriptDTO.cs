using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models.DTO.Script
{
    public class UpdateScriptDTO : CreateScriptDTO
    {
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
