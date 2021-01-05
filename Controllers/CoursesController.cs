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
    public class CoursesController : Controller
    {
        ICourseRepository repo;
        
        public CoursesController(ICourseRepository repo) {
            
            this.repo = repo;
                }
        public IActionResult Index(int page = 1, int tradeId=0)
        {
            var list = repo.GetTradesList();
            list.Insert(0, new Trade { TradeId = 0, TradeName = "All" });
            ViewBag.Trades = list;
            int pageSize = 5;
            var data = repo.GetWithChild();
            if(tradeId > 0)
            {
                return View(data.Where(x=> x.TradeId == tradeId).ToPagedList(page, pageSize));
            }
            return View(data.ToPagedList(page, pageSize));


        }
        public IActionResult Create()
        {
            var list = repo.GetTradesList();
            list.Insert(0, new Trade { TradeId = 0, TradeName = "Select" });
            ViewBag.Trades = list;
            return View();
        }
        public IActionResult GetCourseOptions(int id)
        {
            var data = repo.GetCourseOptions(id);
            data.Insert(0, new Course { CourseId = 0, CourseName = "Select" });
            return Json(data);
        }
        public IActionResult SaveStudents([FromBody]Student[] students)
        {
            if (ModelState.IsValid)
            {
                repo.InsertStudent(students);
                return Json(new { success = true, data = "Saved" });
            }
            return Json(new { success = false, data = "Error" });
        }
        public IActionResult CreateSingle( string postBack="")
        {
            var list = repo.GetTradesList();
            //list.Insert(0, new Trade { TradeId = 0, TradeName = "Select" });
            ViewBag.Trades = list;
            if(postBack != "")
            {
                ViewBag.PostBack = "Success";
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateSingle(Course c)
        {
            if (ModelState.IsValid)
            {
                if(repo.Insert(c))
                {
                    return RedirectToAction("CreateSingle", new { postBack = "postBack" });
                }
                
            }
            var list = repo.GetTradesList();
           // list.Insert(0, new Trade { TradeId = 0, TradeName = "Select" });
            ViewBag.Trades = list;
            return View(c);
        }
        public IActionResult EditSingle(int id, string postBack = "")
        {
            var list = repo.GetTradesList();
            //list.Insert(0, new Trade { TradeId = 0, TradeName = "Select" });
            ViewBag.Trades = list;
            if (postBack != "")
            {
                ViewBag.PostBack = "Success";
            }
            var data = repo.Get(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult EditSingle(Course c)
        {
            if (ModelState.IsValid)
            {
                if (repo.Update(c))
                {
                    return RedirectToAction("EditSingle", new { postBack = "postBack" });
                }

            }
            var list = repo.GetTradesList();
            // list.Insert(0, new Trade { TradeId = 0, TradeName = "Select" });
            ViewBag.Trades = list;
            return View(c);
        }
        public IActionResult Edit(int id)
        {
            var list = repo.GetTradesList();
            //list.Insert(0, new Trade { TradeId = 0, TradeName = "Select" });
            ViewBag.Trades = list;
            var data = repo.GetWithChild(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult SaveCourse([FromBody]Course c)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateWithChild(c);
                    return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public IActionResult Delete(int id)
        {
            return View(repo.GetWithChild(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DoDelete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
    
}
