using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M7_Class_37_Work_01.Models;
using Microsoft.EntityFrameworkCore;
namespace M7_Class_37_Work_01.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        CourseDbContext db;
        public CourseRepository(CourseDbContext db) { this.db = db; }

        public List<Course> Get()
        {
            return db.Courses.ToList();
        }
        public List<Course> GetWithChild()
        {
            return db.Courses
                .Include(x => x.Trade)
                .Include(x => x.Students)
                .ToList();
        }
        public Course Get(int id)
        {
            return db.Courses.FirstOrDefault(x => x.CourseId == id);
        }
        public Course GetWithChild(int id)
        {
            return db.Courses
                .Include(x => x.Trade)
                .Include(x => x.Students)
                .FirstOrDefault(x => x.CourseId == id);
        }
        public List<Course> GetCourseOptions(int tradeId)
        {
            return db.Courses.Where(c => c.TradeId == tradeId).ToList();
        }

        public List<Trade> GetTradesList()
        {
            return db.Trades.ToList();
        }

        public bool Insert(Course c)
        {
            db.Courses.Add(c);
            return db.SaveChanges() > 0;
        }
        public bool InsertStudent(Student[] students)
        {
            db.Students.AddRange(students);
            return db.SaveChanges() > 0;
        }

        public bool Update(Course c)
        {
            db.Entry(c).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        public bool UpdateWithChild(Course c)
        {


            var orignal = db.Courses.Include(x => x.Students).First(x => x.CourseId == c.CourseId);
            orignal.TradeId = c.TradeId;
            orignal.CourseName = c.CourseName;
            orignal.Duration = c.Duration;

            if (c.Students != null && c.Students.Count > 0)
            {
                var students = c.Students.ToArray();
                for (var i = 0; i < students.Length; i++)
                {
                    var temp = orignal.Students.FirstOrDefault(x => x.StudentId == students[i].StudentId);
                    if (temp != null)
                    {
                        temp.StudentName = students[i].StudentName;
                        temp.DOB = students[i].DOB;
                        temp.Email = students[i].Email;
                    }
                    else
                    {
                        orignal.Students.Add(students[i]);
                    }
                }
                var originalStudents = orignal.Students.ToArray();
                for (var i = 0; i < originalStudents.Length; i++)
                {
                    var temp = c.Students.FirstOrDefault(t => t.StudentId == originalStudents[i].StudentId);
                    if (temp == null)
                        db.Students.Remove(originalStudents[i]);
                }
            }
            //db.Entry(c).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            db.Entry(new Course { CourseId = id }).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }
    }
}
