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
    public class ExpensController : Controller
    {
        private Ems_DbEntities1 db = new Ems_DbEntities1();

        // GET: Expens
        public ActionResult Index()
        {
            var expenses = db.Expenses.Include(e => e.Employee).Include(e => e.Status);
            return View(expenses.ToList());
        }

        // GET: Expens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expens expens = db.Expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            return View(expens);
        }

        // GET: Expens/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            ViewBag.StatusofReceipt = new SelectList(db.Status, "StatusofReceipt", "StatusofReceipt");
            return View();
        }

        // POST: Expens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReceiptNo,Receiptdate,Receiptamount,UploadReceipt,EmployeeId,StatusofReceipt")] Expens expens)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", expens.EmployeeId);
            ViewBag.StatusofReceipt = new SelectList(db.Status, "StatusofReceipt", "StatusofReceipt", expens.StatusofReceipt);
            return View(expens);
        }

        // GET: Expens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expens expens = db.Expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", expens.EmployeeId);
            ViewBag.StatusofReceipt = new SelectList(db.Status, "StatusofReceipt", "StatusofReceipt", expens.StatusofReceipt);
            return View(expens);
        }

        // POST: Expens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReceiptNo,Receiptdate,Receiptamount,UploadReceipt,EmployeeId,StatusofReceipt")] Expens expens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", expens.EmployeeId);
            ViewBag.StatusofReceipt = new SelectList(db.Status, "StatusofReceipt", "StatusofReceipt", expens.StatusofReceipt);
            return View(expens);
        }

        // GET: Expens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expens expens = db.Expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            return View(expens);
        }

        // POST: Expens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expens expens = db.Expenses.Find(id);
            db.Expenses.Remove(expens);
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
