using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.BLL.Model;
using University.DAL;
using University.DAL.Repository;

namespace University.BLL.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly IGenericRepository<Student> repoStudent;
        private readonly IGenericRepository<Groups> repoGroup;
        private readonly IMapper mapper;
        public UniversityService(IGenericRepository<Student> _repoStudent,
           IGenericRepository<Groups> _repoGroup, IMapper _mapper)
        {
            repoStudent = _repoStudent;
            repoGroup = _repoGroup;
            mapper = _mapper;
        }
        public void AddGroup(GroupDTO group)
        {
            var addGroup = mapper.Map<Groups>(group);
            repoGroup.Create(addGroup);
        }

        public void AddStudents(StudentsDTO student)
        {
            var addStudent = mapper.Map<Student>(student);
            repoStudent.Create(addStudent);
        }

        public void DeleteGroup(GroupDTO group)
        {
            var deleteGroup = mapper.Map<Groups>(group);
            repoGroup.Delete(deleteGroup);
        }

        public void DeleteStudent(StudentsDTO student)
        {
            var deleteStudent = mapper.Map<Student>(student);
            repoStudent.Delete(deleteStudent);
        }

        public IEnumerable<GroupDTO> GetGroup()
        {
            var group = repoGroup.Read();
            var modelGroup = mapper.Map<ICollection<GroupDTO>>(group);
            return modelGroup;
        }

        public IEnumerable<StudentsDTO> GetStudents()
        {
            var student = repoStudent.Read();
            var modelStudent = mapper.Map<ICollection<StudentsDTO>>(student);
            return modelStudent;
        }

        public void UpdateGroup(GroupDTO group)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(StudentsDTO students)
        {
            throw new NotImplementedException();
        }
    }
}
