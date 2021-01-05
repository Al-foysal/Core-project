using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M7_Class_37_Work_01.Models;
using M7_Class_37_Work_01.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
namespace M7_Class_37_Work_01.Controllers
{
    //[Authorize]
    public class TradesController : Controller
    {
        ITradeRepo repo;
        public TradesController(ITradeRepo repo) { this.repo = repo; }
        public IActionResult Index(int page=1)
        {
            int size = 5;
            var data = repo.GetWithChildred()
                .ToPagedList(page, size);
           
            return View(data);
        }
        public IActionResult CreateTradeWithCourse()
        {
            return View();
        }
        public IActionResult CreateTradeWithCourse1()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CreateTradeWithCourse([FromBody]Trade t)
        {
            if (ModelState.IsValid)
            {
                if (repo.Insert(t))
                    return Json(new { success = true });
                else
                    return Json(new { success = false });
            }
            else
            {
              return Json(new { success = false });
            }
            //return View();
        }
        public IActionResult GetCourseAddForm()
        {
            return PartialView("_CourseAddRowPartial");
        }
        public IActionResult Create(string postBack = "")
        {
            if (postBack != "")
            {
                ViewBag.PostBack = "Success";
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Trade t)
        {
            if (ModelState.IsValid)
            {
                if (repo.Insert(t))
                {
                    return RedirectToAction("Create", new { postBack = "postBack" });
                }
            }
            return View(t);
        }
        public IActionResult EditWithCourse(int id)
        {
            var data = repo.Get(id);
            if (data == null)
                return NotFound();
            return View(data);
        }
        [HttpPost]
        public IActionResult EditWithCourse([FromBody]Trade t)
        {
            if (ModelState.IsValid)
            {
                repo.Update(t);
                return Json(new { success = true });
              
            }
            return Json(new { success = false });
            
        }
    }
}