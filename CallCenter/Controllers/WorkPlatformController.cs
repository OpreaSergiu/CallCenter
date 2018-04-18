using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallCenter.Controllers
{
    [Authorize]
    public class WorkPlatformController : Controller
    {
        public ActionResult Index(int id = 0)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}