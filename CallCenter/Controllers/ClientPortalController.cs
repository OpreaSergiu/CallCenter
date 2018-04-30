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
    public class ClientPortalController : Controller
    {
        private Context db = new Context();

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

            string query_phones = "SELECT * FROM PhoneModels WHERE AccountNumber = @p0 ";
            string query_address = "SELECT * FROM AddressModels WHERE AccountNumber = @p0 ";
            string query_invoices = "SELECT * FROM InvoiceModels WHERE AccountNumber = @p0 ";
            string query_notes = "SELECT * FROM NotesModels WHERE AccountNumber = @p0 ORDER BY SeqNumber DESC";

            var model = new WorkPlatformAccountModels()
            {
                Account = db.WorkPlatformModels.Find(id),

                Phones = (db.Database.SqlQuery<PhoneModels>(query_phones, id)),

                Address = db.AddressModels.SqlQuery(query_address, id).SingleOrDefault(),

                Invoices = (db.Database.SqlQuery<InvoiceModels>(query_invoices, id)),

                Notes = (db.Database.SqlQuery<NotesModels>(query_notes, id))
            };

            return View(model);
        }
        public ActionResult Reports()
        {
            return View();
        }
        public ActionResult Payments()
        {
            return View();
        }
    }
}