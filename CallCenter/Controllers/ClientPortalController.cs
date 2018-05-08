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
            return View(db.PaymentsModels.ToList());
        }
        public ActionResult ApprovePayment(int id)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CallCenter.Context;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            SqlCommand com = new SqlCommand("UPDATE PaymentsModels SET Approve = 1 WHERE Id = @p0", conn);
            com.Parameters.AddWithValue("@p0", id);

            conn.Open();

            com.ExecuteNonQuery();

            return RedirectToAction("Payments");
        }
    }
}