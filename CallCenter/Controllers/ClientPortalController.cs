using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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

        public FileResult GenerateNotesRerport(string id)
        {
            var filePath = Server.MapPath("~/ReportingFolder/") + "\\Notes_Rerpot.xlsx";

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            string fullScriptPath = Server.MapPath("~/ReportingScripts/") + "\\notesreport.py";

            var textResult = run_cmd(fullScriptPath, id);

            byte[] fileBytes = System.IO.File.ReadAllBytes(@filePath);
            string fileName = "Notes_Rerpot.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}