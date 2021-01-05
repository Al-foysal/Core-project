using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M7_Class_37_Work_01.Models;
using Microsoft.EntityFrameworkCore;

namespace M7_Class_37_Work_01.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        CourseDbContext db;
        public StudentRepository(CourseDbContext db) { this.db = db; }
        public List<Student> Get()
        {
            return db.Students
                .Include(s => s.Course)
                .ThenInclude(c => c.Trade)
                .ToList();
                
        }

        public Student Get(int id)
        {
            return db.Students.FirstOrDefault(x => x.StudentId == id);
        }
        public Student GetWithParent(int id)
        {
            return db.Students.Include(x=> x.Course).FirstOrDefault(x => x.StudentId == id);
        }
        public List<Course> GetCourseList()
        {
            return db.Courses.ToList();
        }
        public bool Insert(Student s)
        {
            db.Students.Add(s);
            return db.SaveChanges() > 0;
        }
        public bool Update(Student s)
        {
            db.Entry(s).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            db.Entry(new Student { StudentId = id }).State = EntityState.Deleted;
            return db.SaveChanges() > 0;

        }
    }
}
