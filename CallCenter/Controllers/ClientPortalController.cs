using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CallCenter;
using CallCenter.Models;

namespace CallCenter.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientPortalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Audit(int? id=0)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new WorkPlatformAccountModels()
            {
                Account = db.WorkPlatformModels.Find(id),

                Phones = db.PhoneModels.Where(m => m.AccountNumber == id),

                Address = db.AddressModels.Where(m => m.AccountNumber == id).SingleOrDefault(),

                Invoices = db.InvoiceModels.Where(m => m.AccountNumber == id),

                Notes = db.NotesModels.Where(m => m.AccountNumber == id).OrderByDescending(s => s.SeqNumber),

                Check = true,

                Inventory = db.WorkPlatformModels.Where(m => m.ClientID == "TEST01")
            };

            if (model.Account is null)
            {
                model.Check = false;
            }

            return View(model);
        }

        public ActionResult Reports()
        {
            return View();
        }
        public ActionResult Payments()
        {
            return View(db.PaymentsModels.ToList());
        }
        public ActionResult ApprovePayment(int id)
        {
            var result = db.PaymentsModels.SingleOrDefault(b => b.Id == id);
            if (result != null)
            {
                result.Approve = true;
                db.SaveChanges();
            }

            return RedirectToAction("Payments");
        }
    }
}