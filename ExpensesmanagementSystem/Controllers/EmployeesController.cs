using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExpensesmanagementSystem.Models;
using System.IO;

namespace ExpensesmanagementSystem.Controllers
{
    [Authorize(Roles = "Supervisor,Employee")]
    public class EmployeesController : Controller
    {
        private Ems_DbEntities1 db = new Ems_DbEntities1();
        

        // GET: Employees
        //[Authorize(Roles ="Supervisor")]
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {

            Employee emp = (Employee)Session["userEmployee"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,EmployeeName,DateofBirth,Dateofjoin,DepartmentName,Location,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,EmployeeName,DateofBirth,Dateofjoin,DepartmentName,Location,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult EmployeeHome()
        {
            Employee emp = (Employee)Session["user"];
            TempData["empId"] = emp.EmployeeId;
            return View();
        }
        public ActionResult AddReceipt()
        {
            Employee emp = (Employee)Session["user"];
            TempData["empId"] = emp.EmployeeId;
            TempData.Keep();
           // Expens ex = new Expens();
            return View();
        }
        [HttpPost]
        public ActionResult AddReceipt(HttpPostedFileBase file, FormCollection fc)
        {
            Expens ex = new Expens();
            ex.ReceiptNo = Convert.ToInt32(fc["ReceiptNo"]);
            ex.Receiptamount = Convert.ToInt32(fc["Receiptamount"]);
            ex.Receiptdate = Convert.ToDateTime(fc["Receiptdate"]);
            ex.EmployeeId = Convert.ToInt32(TempData["empId"]);
            //ex.UploadReceipt = Convert.ToByte(fc["UploadReceipt"]);
            //ex.UploadReceipt = Convert.(fc["UploadReceipt"]);
            //if (file != null && file.ContentLength > 0)
            //    try
            //    {
            //        string path = Path.Combine(Server.MapPath("~/Images"),
            //                                   Path.GetFileName(file.FileName));
            //        file.SaveAs(path);
            //        ViewBag.Message = "File uploaded successfully";
            //        //ex.UploadReceipt = file;
            //        if (ModelState.IsValid)
            //        {
            //            db.Expenses.Add(ex);
            //            db.SaveChanges();
            //            return RedirectToAction("Index");
            //        }
            //    }
            //    catch (Exception exception)
            //    {
            //        ViewBag.Message = "ERROR:" + exception.Message.ToString();
            //    }
            //else
            //{
            //    ViewBag.Message = "You have not specified a file.";
            //}
            //return View();
            if (file != null)
            {
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Path.Combine(Server.MapPath("~/Images/" + ImageName));

                // save image in folder
                file.SaveAs(physicalPath);
                ex.UploadReceipt = file.ToString();
                db.Expenses.Add(ex);
                //Session["userStudent"] = ex;
                db.SaveChanges();
            }
            //Display records
            return RedirectToAction("ViewReceipt");

        


        //    Receipt newRecord = new Receipt();
        //newRecord.ReceiptNumber = Convert.ToInt32(form["ReceiptNumber"]);
        //    newRecord.ReceiptDate = DateTime.Parse(form["ReceiptDate"]);
        //    newRecord.EmployeeId = Convert.ToInt32(form["EmployeeId"]);
        //    newRecord.ReceiptAmount = Convert.ToInt32(form["ReceiptAmount"]);

        //    if (file != null)
        //    {
        //        string ImageName = System.IO.Path.GetFileName(file.FileName);
        //string physicalPath = Path.Combine(Server.MapPath("~/Image/" + ImageName));

        //// save image in folder
        //file.SaveAs(physicalPath);

        //        //save new record in database

        //        //newRecord.ReceiptNumber = Convert.ToInt32(Request.Form["ReceiptNumber"]);
        //        //newRecord.ReceiptDate = DateTime.Parse(Request.Form["ReceiptDate"]);
        //        //newRecord.EmployeeId= Convert.ToInt32(Request.Form["EmployeeId"]);
        //        newRecord.ReceiptUrl = ImageName;
        //        //newRecord.Category = Request.Form["Category"];
        //        //newRecord.ReceiptAmount = Convert.ToInt32(Request.Form["ReceiptAmount"]);
        //        db.Receipts.Add(newRecord);
        //        Session["userStudent"] = newRecord;
        //        db.SaveChanges();

        //    }
        //    //Display records
        //    return RedirectToAction("ViewReceipts");
        }
        public ActionResult ViewReceipt()
        {

            return View();
        }

        public ActionResult EditEmployee()
        {
            Employee emp = (Employee)Session["user"];
            TempData["empId"] = emp.EmployeeId;
            TempData["empDOJ"] = emp.Dateofjoin;
            TempData["empDep"] = emp.DepartmentName;
            TempData["empEmail"] = emp.Email;
            TempData.Keep();
            return View(emp);
        }
        [HttpPost]
        public ActionResult EditEmployee(FormCollection form)
        {
            Employee employee = new Employee();
            employee.EmployeeId = Convert.ToInt32(TempData["empId"]);
            employee.EmployeeName = form["EmployeeName"];
            employee.DateofBirth = Convert.ToDateTime(form["DateofBirth"]);
            employee.Dateofjoin = Convert.ToDateTime(TempData["empDOJ"]);
            employee.DepartmentName = TempData["empDep"].ToString();
            employee.Location = form["Location"].ToString();
            employee.Email = TempData["empEmail"].ToString();


            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                Session["user"] = employee;
                return RedirectToAction("EditEmployee");
            }
            return View(employee);
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
