using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public class LoanDetailsVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "LoanAmount is Required !")]
        //[RegularExpression(@"\d+{1,6}", ErrorMessage = "Invalid LoanAmount !")]
        public string LoanAmount { get; set; }


        [Required(ErrorMessage = "TenureBy is Required !")]
        public string TenureBy { get; set; }


        [Required(ErrorMessage = "Installments is Required !")]
        //[RegularExpression(@"\d+{1,2}", ErrorMessage = "Invalid Installments !")]
        public string Installments { get; set; }


        [Required(ErrorMessage = "EMIAmount is Required !")]
        //[RegularExpression(@"\d+{1,5}", ErrorMessage = "Invalid EMIAmount !")]
        public string EMIAmount { get; set; }


        [Required(ErrorMessage = "IssueDate is Required !")]
        public string IssueDate { get; set; }


        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
