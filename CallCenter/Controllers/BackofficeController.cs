using CallCenter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallCenter.Controllers
{
    [Authorize(Roles = "Backoffice")]
    public class BackofficeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            return View(db.PaymentsModels.ToList());
        }

        public ActionResult PaymentPost(int id)
        {
            var PayModel = db.PaymentsModels.Find(id); ;
            var AccModel = db.WorkPlatformModels.Find(PayModel.AccountNumber);
            string NewInvoiceStatus = "OPEN";

            float invouce_due = db.InvoiceModels.Where(m => m.AccountNumber == PayModel.AccountNumber).Where(m => m.Invoice == PayModel.Invoice).FirstOrDefault().Due;

            if (invouce_due == PayModel.Amount)
            {
                NewInvoiceStatus = "CLOSED";
            }

            float NewInvoiceDue = invouce_due - PayModel.Amount;

            string NewAccountStatus = AccModel.Status;

            if (invouce_due == PayModel.Amount)
            {
                NewAccountStatus = "CLOSE";
            }

            float NewTotalDue = AccModel.TotalDue - PayModel.Amount;
            float NewTotalReceived = AccModel.TotalReceived + PayModel.Amount;

            var result = db.InvoiceModels.Where(m => m.AccountNumber == PayModel.AccountNumber).Where(m => m.Invoice == PayModel.Invoice).FirstOrDefault();
            if (result != null)
            {
                result.Due = NewInvoiceDue;
                result.Status = NewInvoiceStatus;
                db.SaveChanges();
            }

            var result1 = db.WorkPlatformModels.Where(m => m.Id == PayModel.AccountNumber).FirstOrDefault();
            if (result1 != null)
            {
                result1.TotalReceived = NewTotalReceived;
                result1.TotalDue = NewTotalDue;
                result1.Status = NewAccountStatus;
                db.SaveChanges();
            }

            var result2 = db.PaymentsModels.Where(m => m.Id == id).FirstOrDefault();
            if (result2 != null)
            {
                result2.PostedFlag = true;
                db.SaveChanges();
            }

            return RedirectToAction("PaymentRequest");
        }
    }
}