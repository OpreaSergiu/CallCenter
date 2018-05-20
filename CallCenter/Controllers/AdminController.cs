using CallCenter.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CallCenter.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserLogs()
        {
            return View(db.LoginLogsModels.OrderByDescending(s => s.Id).ToList());
        }

        public ActionResult UsersDesk()
        {
            return View(db.UserDeskModels.OrderByDescending(s => s.Id).ToList());
        }

        public ActionResult EditDesk(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDeskModels userDeskModels = db.UserDeskModels.Find(id);
            if (userDeskModels == null)
            {
                return HttpNotFound();
            }
            return View(userDeskModels);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDesk([Bind(Include = "Id,UserEmail,Desk")] UserDeskModels userDeskModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDeskModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UsersDesk");
            }
            return View(userDeskModels);
        }

        public ActionResult UsersClientId()
        {
            return View(db.UserClientIdModels.OrderByDescending(s => s.Id).ToList());
        }

        public ActionResult EditClientId(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserClientIdModels userClientIdModels = db.UserClientIdModels.Find(id);
            if (userClientIdModels == null)
            {
                return HttpNotFound();
            }
            return View(userClientIdModels);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClientId([Bind(Include = "Id,UserEmail,ClientID")] UserClientIdModels userClientIdModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userClientIdModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UsersClientId");
            }
            return View(userClientIdModels);
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> MakeAdmin(string id)
        {
            if (UserManager.IsInRole(id, "Admin"))
                await UserManager.RemoveFromRolesAsync(id, "Admin");
            else
                await UserManager.AddToRoleAsync(id, "Admin");

            return RedirectToAction("UserRoles");
        }

        public async System.Threading.Tasks.Task<ActionResult> MakeBackoffice(string id)
        {
            if (UserManager.IsInRole(id, "Backoffice"))
                await UserManager.RemoveFromRolesAsync(id, "Backoffice");
            else
                await UserManager.AddToRoleAsync(id, "Backoffice");

            return RedirectToAction("UserRoles");
        }

        public async System.Threading.Tasks.Task<ActionResult> MakeClient(string id)
        {
            if (UserManager.IsInRole(id, "Client"))
                await UserManager.RemoveFromRolesAsync(id, "Client");
            else
                await UserManager.AddToRoleAsync(id, "Client");

            return RedirectToAction("UserRoles");
        }

        public async System.Threading.Tasks.Task<ActionResult> MakeUser(string id)
        {
            if (UserManager.IsInRole(id, "User"))
                await UserManager.RemoveFromRolesAsync(id, "User");
            else
                await UserManager.AddToRoleAsync(id, "User");

            return RedirectToAction("UserRoles");
        }

        public ActionResult SingelUserRoles(string id)
        {
            return View(db.Users.Find(id));
        }

        public ActionResult UserRoles()
        {
            return View(db.Users.ToList());
        }
    }
}