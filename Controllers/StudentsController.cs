using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M7_Class_37_Work_01.Models;
using M7_Class_37_Work_01.Repositories;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
namespace M7_Class_37_Work_01.Controllers
{
    public class StudentsController : Controller
    {
        IStudentRepository repo;
        public StudentsController(IStudentRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index(int page =1, int courseId=0)
        {
          
            var list = repo.GetCourseList();
            list.Insert(0, new Course { CourseId = 0, CourseName = "All" });
            ViewBag.Courses = list;
            int pageSize = 5;
            var data = repo.Get();
               
            if(courseId> 0)
            {
                return View(data.Where(s => s.CourseId == courseId).ToPagedList(page, pageSize));
            }
            return View(data.ToPagedList(page, pageSize));
        }
        public IActionResult Create(string postBack="")
        {
            var list = repo.GetCourseList();
            ViewBag.Courses = list;
            if(postBack!= "")
            {
                ViewBag.PostBack = "PostBack";
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                if (repo.Insert(s))
                {
                    return RedirectToAction("Create", new { postBack = "PostBack" });
                }
            }
            var list = repo.GetCourseList();
            ViewBag.Courses = list;

            return View(s);
        }
        public IActionResult Edit(int id,string postBack = "")
        {
            var list = repo.GetCourseList();
            ViewBag.Courses = list;
            if (postBack != "")
            {
                ViewBag.PostBack = "PostBack";
            }
            var student = repo.Get(id);
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student s)
        {
            if (ModelState.IsValid)
            {
                if (repo.Update(s))
                {
                    return RedirectToAction("Edit", new {id=s.StudentId, postBack = "PostBack" });
                }
            }
            var list = repo.GetCourseList();
            ViewBag.Courses = list;
            
            
            return View(s);
        }
        public IActionResult Delete(int id)
        {
            return View(repo.GetWithParent(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DoDelete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }
        
    }
}