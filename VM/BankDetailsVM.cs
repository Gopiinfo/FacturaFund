using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public class BankDetailsVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "MOP is Required !")]
        public string ModeOfPayment { get; set; }

        public string UPIType { get; set; }
        public string UPIMobileNumber { get; set; }

        public string BankName { get; set; }
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string IFSCCode { get; set; }
        public string AccountRegisterMobile { get; set; }


        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
