//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExpensesmanagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supervisor
    {
        public int SupervisorId { get; set; }
        public string Supervisorname { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> ReceiptNo { get; set; }
        public string StatusofReceipt { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Expens Expens { get; set; }
        public virtual Status Status { get; set; }
    }
}
