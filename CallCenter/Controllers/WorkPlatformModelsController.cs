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
    public class WorkPlatformModelsController : Controller
    {
        private Context db = new Context();

        // GET: WorkPlatformModels
        public ActionResult Index()
        {
            return View(db.WorkPlatformModels.ToList());
        }

        // GET: WorkPlatformModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkPlatformModels workPlatformModels = db.WorkPlatformModels.Find(id);
            if (workPlatformModels == null)
            {
                return HttpNotFound();
            }
            return View(workPlatformModels);
        }

        // GET: WorkPlatformModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkPlatformModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientReference,ClientID,Name,AssignAmount,TotalReceived,OtherDue,TotalDue,Desk,Status,PalacementDate,LastWorkDate")] WorkPlatformModels workPlatformModels)
        {
            if (ModelState.IsValid)
            {
                db.WorkPlatformModels.Add(workPlatformModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workPlatformModels);
        }

        // GET: WorkPlatformModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkPlatformModels workPlatformModels = db.WorkPlatformModels.Find(id);
            if (workPlatformModels == null)
            {
                return HttpNotFound();
            }
            return View(workPlatformModels);
        }

        // POST: WorkPlatformModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientReference,ClientID,Name,AssignAmount,TotalReceived,OtherDue,TotalDue,Desk,Status,PalacementDate,LastWorkDate")] WorkPlatformModels workPlatformModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workPlatformModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workPlatformModels);
        }

        // GET: WorkPlatformModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkPlatformModels workPlatformModels = db.WorkPlatformModels.Find(id);
            if (workPlatformModels == null)
            {
                return HttpNotFound();
            }
            return View(workPlatformModels);
        }

        // POST: WorkPlatformModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkPlatformModels workPlatformModels = db.WorkPlatformModels.Find(id);
            db.WorkPlatformModels.Remove(workPlatformModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
