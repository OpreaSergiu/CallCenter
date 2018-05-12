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
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? id = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string user_name = User.Identity.GetUserName();
            var user_desk = db.UserDeskModels.SingleOrDefault(b => b.UserEmail == user_name);

            var emptyModel = new WorkPlatformAccountModels()
            {
                Phones = db.PhoneModels.Where(m => m.AccountNumber == -1),

                Invoices = db.InvoiceModels.Where(m => m.AccountNumber == -1),

                Notes = db.NotesModels.Where(m => m.AccountNumber == -1),

                Check = false,

                Inventory = db.WorkPlatformModels.Where(m => m.Desk == user_desk.Desk)
            };

            var model = new WorkPlatformAccountModels()
            {
                Account = db.WorkPlatformModels.Find(id),

                Phones = db.PhoneModels.Where(m => m.AccountNumber == id),

                Address = db.AddressModels.Where(m => m.AccountNumber == id).SingleOrDefault(),

                Invoices = db.InvoiceModels.Where(m => m.AccountNumber == id),

                Notes = db.NotesModels.Where(m => m.AccountNumber == id).OrderByDescending(s => s.SeqNumber),

                Check = true,

                Inventory = db.WorkPlatformModels.Where(m => m.Desk == user_desk.Desk)
            };

            if(model.Account is null)
            {
                model.Check = false;
            }
            else
            {
                if ((model.Account.Desk == user_desk.Desk) | (User.IsInRole("Admin")))
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
        [HttpPost]
        public ActionResult Index(string actioncode, string status, string note, int id)
        {
            string user_name = User.Identity.GetUserName();
            var currentDate = DateTime.Now;

            int maxAge = db.NotesModels.Where(m => m.AccountNumber == id).Max(p => p.SeqNumber);

            var user_desk = db.UserDeskModels.SingleOrDefault(b => b.UserEmail == user_name);

            var result = db.WorkPlatformModels.SingleOrDefault(b => b.Id == id);
            if (result != null)
            {
                result.LastWorkDate = currentDate;
                result.Desk = user_desk.Desk;
                result.Status = status;
                db.SaveChanges();
            }

            var newNote = new NotesModels();

            newNote.AccountNumber = id;
            newNote.ActionCode = actioncode;
            newNote.Status = status;
            newNote.Note = note;
            newNote.SeqNumber = maxAge + 1;
            newNote.NoteDate = currentDate;
            newNote.UserCode = user_desk.Desk;
            newNote.Desk = user_desk.Desk;

            db.NotesModels.Add(newNote);
            db.SaveChanges();

            string redirectUrl = "/WorkPlatform/Index/" + id;
            return Redirect(redirectUrl);
        }

        public ActionResult ProcessPaymentRequest(int id)
        {
            var result = db.InvoiceModels.SingleOrDefault(b => b.Id == id);
            if (result != null)
            {
                result.PaymentRequestFlag = true;
                db.SaveChanges();
            }

            var newPayment = new PaymentsModels();
            var InvModel = db.InvoiceModels.Find(id);
            var AccModel = db.WorkPlatformModels.Find(InvModel.AccountNumber);
            var currentDate = DateTime.Now;

            newPayment.AccountNumber = AccModel.Id;
            newPayment.ClientID = AccModel.ClientID;
            newPayment.ClientReference = AccModel.ClientReference;
            newPayment.Invoice = InvModel.Invoice;
            newPayment.Amount = InvModel.Amount;
            newPayment.PaymentDate = currentDate;
            newPayment.Approve = false;
            newPayment.PostedFlag = false;

            db.PaymentsModels.Add(newPayment);
            db.SaveChanges();

            string redirectUrl = "/WorkPlatform/Index/" + AccModel.Id;

            return Redirect(redirectUrl);
        }
    }
}