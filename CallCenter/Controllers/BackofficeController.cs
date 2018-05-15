using CallCenter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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

        private string run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\Sergiu\AppData\Local\Programs\Python\Python36-32\python.exe";
            start.CreateNoWindow = true;
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    //Console.Write(result);
                    process.WaitForExit();
                    return result;
                }
            }
        }

        [HttpGet]
        public ActionResult Trust()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Trust(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string path = Server.MapPath("~/TrustFiles/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                ViewBag.Message = "File uploaded successfully.";

                string fullFilePath = path + Path.GetFileName(postedFile.FileName);
                string fullScriptPath = Server.MapPath("~/TrustScrips/") + "\\trust.py";

                var textResult = run_cmd(fullScriptPath, fullFilePath);

                string[] results = textResult.Split(null);

                ViewBag.Message2 = results[0];
                ViewBag.Message3 = results[1];

                //if (System.IO.File.Exists(path + Path.GetFileName(postedFile.FileName)))
                //{
                //    System.IO.File.Delete(path + Path.GetFileName(postedFile.FileName));
                //}
            }

            return View("Trust");
        }

        [HttpGet]
        public ActionResult NewBusiness()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewBusiness(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string path = Server.MapPath("~/NewBusinessFiles/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                ViewBag.Message = "File uploaded successfully.";

                string fullFilePath = path + Path.GetFileName(postedFile.FileName);
                string fullScriptPath = Server.MapPath("~/NewBusinessScripts/") + "\\newbusiness.py";

                var textResult = run_cmd(fullScriptPath, fullFilePath);

                string[] results = textResult.Split(null);

                ViewBag.Message2 = results[0];
                ViewBag.Message3 = results[1];

                //if (System.IO.File.Exists(path + Path.GetFileName(postedFile.FileName)))
                //{
                //    System.IO.File.Delete(path + Path.GetFileName(postedFile.FileName));
                //}
            }

            return View("NewBusiness");
        }

        public ActionResult PaymentRequest()
        {
            return View(db.PaymentsModels.ToList().OrderByDescending(s => s.Id));
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

            if (AccModel.TotalDue == PayModel.Amount)
            {
                NewAccountStatus = "CLOSED";
            }

            float NewTotalDue = AccModel.TotalDue - PayModel.Amount;
            float NewTotalReceived = AccModel.TotalReceived + PayModel.Amount;

            var result = db.InvoiceModels.Where(m => m.AccountNumber == PayModel.AccountNumber).Where(m => m.Invoice == PayModel.Invoice).FirstOrDefault();
            if (result != null)
            {
                result.Due = NewInvoiceDue;
                result.Status = NewInvoiceStatus;
                result.PostedFlag = true;
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