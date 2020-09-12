using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.BLL.Model;
using University.DAL;

namespace University.BLL.Utils
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Student, StudentsDTO>().ForMember(x=>x.GroupName, opt=>opt.MapFrom(x=>x.Groups.Name));
            CreateMap<StudentsDTO, Student>().ForMember(x=>x.Groups, opt=>opt.MapFrom(x=>new Groups { Name = x.GroupName}));
            CreateMap<GroupDTO, Groups>();
            CreateMap<Groups, GroupDTO>();
        }
    }
}
