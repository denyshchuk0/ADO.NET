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
            CreateMap<Student, StudentsDTO>();
            CreateMap<StudentsDTO, Student>();
            CreateMap<GroupDTO, Groups>();
            CreateMap<Groups, GroupDTO>();
        }
    }
}
