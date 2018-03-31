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
        public ActionResult Index()
        {
            return View();
        }
    }
}