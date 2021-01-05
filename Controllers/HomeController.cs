using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M7_Class_37_Work_01.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return View();
        } 
    }
}
