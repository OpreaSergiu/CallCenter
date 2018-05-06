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
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace CallCenter.Controllers
{
    [Authorize(Roles = "User")]
    public class WorkPlatformController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(int? id = 0)
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

                Notes = (db.Database.SqlQuery<NotesModels>(query_notes, id)),

                Notes2 = new NotesModels()
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string actioncode, string status, string note, int? id = 0)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CallCenter.Context;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand insert = new SqlCommand("INSERT INTO NotesModels(AccountNumber, ActionCode, Status, Note) values(@p0, @p1, @p2, @p3)", conn);
            insert.Parameters.AddWithValue("@p0", id);
            insert.Parameters.AddWithValue("@p1", actioncode);
            insert.Parameters.AddWithValue("@p2", status);
            insert.Parameters.AddWithValue("@p3", note);
            try
            {
                conn.Open();
                insert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                conn.Close();
            }

            return Redirect("/WorkPlatform/Index/1");
        }
    }
}