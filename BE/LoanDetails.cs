using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class LoanDetails
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int LoanAmount { get; set; }
        public int Tenure { get; set; }
        public string TenureBy { get; set; }
        public int Installments { get; set; }
        public int EMIAmount { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
