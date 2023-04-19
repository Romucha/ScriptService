using AutoMapper;
using ScriptService.Models.Data;
using ScriptService.Models.DTO;
using ScriptService.Models.DTO.Script;
using ScriptService.Models.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.DataManagement.Mapping
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer() 
        {
            CreateMap<ScriptUser, LoginUserDTO>().ReverseMap();
            CreateMap<ScriptUser, RegisterUserDTO>().ReverseMap();
            CreateMap<ScriptUser, AuthenticatedUserDTO>().ReverseMap();
            CreateMap<Script, CreateScriptDTO>().ReverseMap();
            CreateMap<Script, GetScriptDTO>().ReverseMap();
            CreateMap<Script, UpdateScriptDTO>().ReverseMap();
            CreateMap<Script, DetailScriptDTO>().ReverseMap();
        }
    }
}
