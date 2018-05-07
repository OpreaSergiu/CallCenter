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
using Microsoft.AspNet.Identity;

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

                Check = true
            };

            if(model.Account is null)
            {
                model.Check = false;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string actioncode, string status, string note, int? id = 0)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CallCenter.Context;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand insert = new SqlCommand("INSERT INTO NotesModels(AccountNumber, ActionCode, Status, Note, SeqNumber, NoteDate, UserCode, Desk) values(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7)", conn);
            insert.Parameters.AddWithValue("@p0", id);
            insert.Parameters.AddWithValue("@p1", actioncode);
            insert.Parameters.AddWithValue("@p2", status);
            insert.Parameters.AddWithValue("@p3", note);

            SqlCommand com = new SqlCommand("SELECT MAX(SeqNumber)+1 FROM NotesModels WHERE AccountNumber = @p0", conn);
            com.Parameters.AddWithValue("@p0", id);

            conn.Open();

            string seq_number = com.ExecuteScalar().ToString();

            insert.Parameters.AddWithValue("@p4", seq_number);

            var currentDate = DateTime.Now.ToString();

            insert.Parameters.AddWithValue("@p5", currentDate);

            string user_name = User.Identity.GetUserName();

            SqlCommand com2 = new SqlCommand("SELECT Desk FROM UserDeskModels WHERE UserEmail = @p0", conn);
            com2.Parameters.AddWithValue("@p0", user_name);

            string user_desk = com2.ExecuteScalar().ToString();

            insert.Parameters.AddWithValue("@p6", user_desk);
            insert.Parameters.AddWithValue("@p7", user_desk);

            insert.ExecuteNonQuery();

            string redirectUrl = "/WorkPlatform/Index/" + id;
            return Redirect(redirectUrl);
        }
    }
}