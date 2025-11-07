using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VM;

namespace BE
{
    public class CustomerDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PANNumber { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public string CeatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }

    public class CustomerWizardInput
    {
        public CustomerDetailsVM CustomerDetails { get; set; }
        public LoanDetailsVM LoanDetails { get; set; }
        public BankDetailsVM BankDetails { get; set; }
        public DocumentDetails DocumentDetails { get; set; }
    }

    public class CustomerWizardInputList
    {
        public List<CustomerDetailsVM> CustomerDetailsList { get; set; }
        public List<LoanDetailsVM> LoanDetailsList { get; set; }
        public List<BankDetailsVM> BankDetailsList { get; set; }
        public List<DocumentDetails> DocumentDetailsList { get; set; }
    }

    public class DatatableInput
    {
        public DataTable CustomerDataTable { get; set; }
        public DataTable BankDataTable { get; set; }
        public DataTable LoanDataTable { get; set; }
        public DataTable DocumentDataTable { get; set; }
    }
}
