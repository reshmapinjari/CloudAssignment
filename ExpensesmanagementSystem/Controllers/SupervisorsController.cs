using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExpensesmanagementSystem.Models;

namespace ExpensesmanagementSystem.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class SupervisorsController : Controller
    {
        private Ems_DbEntities1 db = new Ems_DbEntities1();

        // GET: Supervisors
        public ActionResult Index()
        {
            var supervisors = db.Supervisors.Include(s => s.Employee).Include(s => s.Expens).Include(s => s.Status);
            return View(supervisors.ToList());
        }

        // GET: Supervisors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisor supervisor = db.Supervisors.Find(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            return View(supervisor);
        }

        // GET: Supervisors/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            ViewBag.ReceiptNo = new SelectList(db.Expenses, "ReceiptNo", "StatusofReceipt");
            ViewBag.StatusofReceipt = new SelectList(db.Status, "StatusofReceipt", "StatusofReceipt");
            return View();
        }

        // POST: Supervisors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupervisorId,Supervisorname,Email,ApproveDate,EmployeeId,ReceiptNo,StatusofReceipt")] Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                db.Supervisors.Add(supervisor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", supervisor.EmployeeId);
            ViewBag.ReceiptNo = new SelectList(db.Expenses, "ReceiptNo", "StatusofReceipt", supervisor.ReceiptNo);
            ViewBag.StatusofReceipt = new SelectList(db.Status, "StatusofReceipt", "StatusofReceipt", supervisor.StatusofReceipt);
            return View(supervisor);
        }

        // GET: Supervisors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisor supervisor = db.Supervisors.Find(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", supervisor.EmployeeId);
            ViewBag.ReceiptNo = new SelectList(db.Expenses, "ReceiptNo", "StatusofReceipt", supervisor.ReceiptNo);
            ViewBag.StatusofReceipt = new SelectList(db.Status, "StatusofReceipt", "StatusofReceipt", supervisor.StatusofReceipt);
            return View(supervisor);
        }

        // POST: Supervisors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupervisorId,Supervisorname,Email,ApproveDate,EmployeeId,ReceiptNo,StatusofReceipt")] Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supervisor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", supervisor.EmployeeId);
            ViewBag.ReceiptNo = new SelectList(db.Expenses, "ReceiptNo", "StatusofReceipt", supervisor.ReceiptNo);
            ViewBag.StatusofReceipt = new SelectList(db.Status, "StatusofReceipt", "StatusofReceipt", supervisor.StatusofReceipt);
            return View(supervisor);
        }

        // GET: Supervisors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisor supervisor = db.Supervisors.Find(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            return View(supervisor);
        }

        // POST: Supervisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supervisor supervisor = db.Supervisors.Find(id);
            db.Supervisors.Remove(supervisor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult viewReceipts()
        {
            return View();
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
