using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using VM;

namespace BL.Customer
{
    public static class CustomerBL
    {

        public static DatatableInput ConvertToDataTable(CustomerWizardInput input)
        {
            CustomerWizardInputList list = ConvertToList(input);

            DatatableInput datatable = new DatatableInput();


            datatable.CustomerDataTable = ListToDataTableConverter.ConvertToDataTable(list.CustomerDetailsList);
            datatable.CustomerDataTable.Columns.Remove("Id");
            datatable.CustomerDataTable.Columns.Remove("CreatedBy");
            datatable.CustomerDataTable.Columns.Remove("CreatedDate");
            datatable.CustomerDataTable.Columns.Remove("UpdatedBy");
            datatable.CustomerDataTable.Columns.Remove("UpdatedDate");
            datatable.LoanDataTable = ListToDataTableConverter.ConvertToDataTable(list.LoanDetailsList);
            datatable.LoanDataTable.Columns.Remove("Id");
            datatable.LoanDataTable.Columns.Remove("CreatedBy");
            datatable.LoanDataTable.Columns.Remove("CreatedDate");
            datatable.LoanDataTable.Columns.Remove("UpdatedBy");
            datatable.LoanDataTable.Columns.Remove("UpdatedDate");
            datatable.BankDataTable = ListToDataTableConverter.ConvertToDataTable(list.BankDetailsList);
            datatable.BankDataTable.Columns.Remove("Id");
            datatable.BankDataTable.Columns.Remove("CreatedBy");
            datatable.BankDataTable.Columns.Remove("CreatedDate");
            datatable.BankDataTable.Columns.Remove("UpdatedBy");
            datatable.BankDataTable.Columns.Remove("UpdatedDate");
            datatable.DocumentDataTable = ListToDataTableConverter.ConvertToDataTable(list.DocumentDetailsList);
            datatable.DocumentDataTable.Columns.Remove("Id");
            datatable.DocumentDataTable.Columns.Remove("CreatedBy");
            datatable.DocumentDataTable.Columns.Remove("CreatedDate");
            datatable.DocumentDataTable.Columns.Remove("UpdatedBy");
            datatable.DocumentDataTable.Columns.Remove("UpdatedDate");

            return datatable;
        }


        public static CustomerWizardInputList ConvertToList(CustomerWizardInput input)
        {
            CustomerWizardInputList list = new CustomerWizardInputList();
            list.CustomerDetailsList = new List<CustomerDetailsVM>();
            list.LoanDetailsList = new List<LoanDetailsVM>();
            list.BankDetailsList = new List<BankDetailsVM>();
            list.DocumentDetailsList = new List<DocumentDetails>();

            list.CustomerDetailsList.Add(new CustomerDetailsVM()
            {
                FirstName = input.CustomerDetails.FirstName,
                LastName = input.CustomerDetails.LastName,
                Address = input.CustomerDetails.Address,
                DateOfBirth = input.CustomerDetails.DateOfBirth,
                Email = input.CustomerDetails.Email,
                Gender = input.CustomerDetails.Gender,
                MaritalStatus = input.CustomerDetails.MaritalStatus,
                Mobile1 = input.CustomerDetails.Mobile1,
                Mobile2 = input.CustomerDetails.Mobile2,
                Mobile3 = input.CustomerDetails.Mobile3,
                PANNumber = input.CustomerDetails.PANNumber,
                Pincode = input.CustomerDetails.Pincode
            });

            list.LoanDetailsList.Add(new LoanDetailsVM()
            {
                EMIAmount = input.LoanDetails.EMIAmount,
                Installments = input.LoanDetails.Installments,
                IssueDate = input.LoanDetails.IssueDate,
                LoanAmount = input.LoanDetails.LoanAmount,
                TenureBy = input.LoanDetails.TenureBy,
            });

            list.BankDetailsList.Add(new BankDetailsVM()
            {
                ModeOfPayment = input.BankDetails.ModeOfPayment,
                UPIType = input.BankDetails.UPIType,
                UPIMobileNumber = input.BankDetails.UPIMobileNumber,
                AccountHolderName = input.BankDetails.AccountHolderName,
                AccountNumber = input.BankDetails.AccountNumber,
                AccountRegisterMobile = input.BankDetails.AccountRegisterMobile,
                AccountType = input.BankDetails.AccountType,
                BankName = input.BankDetails.BankName,
                IFSCCode = input.BankDetails.IFSCCode
            });

            list.DocumentDetailsList.Add(new DocumentDetails()
            {
                Aadhaar = input.DocumentDetails.Aadhaar,
                BankPassBook = input.DocumentDetails.BankPassBook,
                Singnature = input.DocumentDetails.Singnature
            });

            return list;
        }

    }
}
