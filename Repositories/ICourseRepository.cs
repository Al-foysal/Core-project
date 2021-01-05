using M7_Class_37_Work_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M7_Class_37_Work_01.Repositories
{
    public interface ICourseRepository
    {
        List<Course> Get();
        List<Course> GetWithChild();
        Course Get(int id);
        Course GetWithChild(int id);
        List<Trade> GetTradesList(); //for dopdown
        List<Course> GetCourseOptions(int tradeId);//for dopdown
        bool Insert(Course c);
        bool Update(Course c);
        bool UpdateWithChild(Course c);
        bool Delete(int id);
        bool InsertStudent(Student[] students);

    }
}
