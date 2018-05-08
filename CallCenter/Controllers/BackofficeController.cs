using CallCenter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallCenter.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BackofficeController : Controller
    {
        private Context db = new Context();

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
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CallCenter.Context;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            var PayModel = new PaymentsModels();
            var AccModel = new WorkPlatformModels();

            conn.Open();

            PayModel = db.PaymentsModels.Find(id);
            AccModel = db.WorkPlatformModels.Find(PayModel.AccountNumber);
            db.InvoiceModels.Where(m => m.AccountNumber == PayModel.AccountNumber).Where(m => m.Invoice == PayModel.Invoice).FirstOrDefault();
            SqlCommand com = new SqlCommand("SELECT Due FROM InvoiceModels WHERE AccountNumber = @p0 and Invoice = @p1", conn);
            com.Parameters.AddWithValue("@p0", PayModel.AccountNumber);
            com.Parameters.AddWithValue("@p1", PayModel.Invoice);

            string invouce_due = com.ExecuteScalar().ToString();

            string NewInvoiceStatus = "OPEN";

            if (Int32.Parse(invouce_due) == Int32.Parse(PayModel.Amount))
            {
                NewInvoiceStatus = "CLOSED";
            }

            int NewInvoiceDue = Int32.Parse(invouce_due) - Int32.Parse(PayModel.Amount);

            SqlCommand com2 = new SqlCommand("UPDATE InvoiceModels SET Due = @p0, Status = @p1 WHERE AccountNumber = @p2 and Invoice = @p3", conn);
            com2.Parameters.AddWithValue("@p0", NewInvoiceDue);
            com2.Parameters.AddWithValue("@p1", NewInvoiceStatus);
            com2.Parameters.AddWithValue("@p2", PayModel.AccountNumber);
            com2.Parameters.AddWithValue("@p3", PayModel.Invoice);

            com2.ExecuteNonQuery();

            string NewAccountStatus = AccModel.Status;

            if (Int32.Parse(invouce_due) == Int32.Parse(PayModel.Amount))
            {
                NewAccountStatus = "CLOSE";
            }

            float NewTotalDue = AccModel.TotalDue - Int32.Parse(PayModel.Amount);
            float NewTotalReceived = AccModel.TotalReceived + Int32.Parse(PayModel.Amount);

            SqlCommand com4 = new SqlCommand("UPDATE WorkPlatformModels SET TotalReceived = @p0, TotalDue = @p1, Status = @p2 WHERE Id = @p3", conn);
            com4.Parameters.AddWithValue("@p0", NewTotalReceived);
            com4.Parameters.AddWithValue("@p1", NewTotalDue);
            com4.Parameters.AddWithValue("@p2", NewAccountStatus);
            com4.Parameters.AddWithValue("@p3", PayModel.AccountNumber);

            com4.ExecuteNonQuery();

            SqlCommand com5 = new SqlCommand("UPDATE PaymentsModels SET PosteFlag = 1 WHERE Id = @p0", conn);
            com5.Parameters.AddWithValue("@p0", id);
        
            com5.ExecuteNonQuery();

            return RedirectToAction("PaymentRequest");
        }
    }
}