using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CallCenter;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    [Authorize]
    public class WorkPlatformController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //WorkPlatformModels workPlatformModels = db.WorkPlatformModels.Find(id);

            PhoneModels PhoneModels = db.PhoneModels.Find(id);

            string query = "SELECT * FROM PhoneModels WHERE AccountNumber = @p0 ";
            IEnumerable<PhoneModels> data = db.Database.SqlQuery<PhoneModels>(query, id);

            if (PhoneModels == null)
            {
                return HttpNotFound();
            }
            return View(data.ToList());
        }
    }
}