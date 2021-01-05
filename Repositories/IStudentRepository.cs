using M7_Class_37_Work_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M7_Class_37_Work_01.Repositories
{
    public interface IStudentRepository
    {
        List<Student> Get();
        Student Get(int id);
        Student GetWithParent(int id);
        List<Course> GetCourseList();
        bool Insert(Student s);
        bool Update(Student s);
        bool Delete(int id);
    }
}
