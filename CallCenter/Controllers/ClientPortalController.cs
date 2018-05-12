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
using Microsoft.AspNet.Identity;

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

            string user_name = User.Identity.GetUserName();
            var user_client = db.UserClientIdModels.SingleOrDefault(b => b.UserEmail == user_name);

            var emptyModel = new WorkPlatformAccountModels()
            {
                Phones = db.PhoneModels.Where(m => m.AccountNumber == -1),

                Invoices = db.InvoiceModels.Where(m => m.AccountNumber == -1),

                Notes = db.NotesModels.Where(m => m.AccountNumber == -1),

                Check = false,

                Inventory = db.WorkPlatformModels.Where(m => m.ClientID == user_client.ClientID)
            };

            var model = new WorkPlatformAccountModels()
            {
                Account = db.WorkPlatformModels.Find(id),

                Phones = db.PhoneModels.Where(m => m.AccountNumber == id),

                Address = db.AddressModels.Where(m => m.AccountNumber == id).SingleOrDefault(),

                Invoices = db.InvoiceModels.Where(m => m.AccountNumber == id),

                Notes = db.NotesModels.Where(m => m.AccountNumber == id).OrderByDescending(s => s.SeqNumber),

                Check = true,

                Inventory = db.WorkPlatformModels.Where(m => m.ClientID == user_client.ClientID)
            };


            if (model.Account is null)
            {
                model.Check = false;
            }
            else
            {
                if (model.Account.ClientID == user_client.ClientID)
                {
                    return View(model);
                }
                else
                {
                    return View(emptyModel);
                }
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