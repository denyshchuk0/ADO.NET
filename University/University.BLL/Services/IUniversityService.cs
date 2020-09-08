using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.BLL.Model;

namespace University.BLL.Services
{
    public interface IUniversityService 
    {
        IEnumerable<StudentsDTO> GetStudents();
        IEnumerable<GroupDTO> GetGroup();
        void AddStudents(StudentsDTO student);
        void AddGroup(GroupDTO group);
        void DeleteStudent(StudentsDTO students);
        void DeleteGroup(GroupDTO group);
        void UpdateStudent(StudentsDTO students);
        void UpdateGroup(GroupDTO group);



    }
}
