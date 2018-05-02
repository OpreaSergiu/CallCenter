using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallCenter.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BackofficeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Trust()
        {
            return View();
        }
        public ActionResult NewBusiness()
        {
            return View();
        }
        public ActionResult PaymentRequest()
        {
            return View();
        }
    }
}