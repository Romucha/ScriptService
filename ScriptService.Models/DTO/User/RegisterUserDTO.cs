﻿using System;
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
    }
}