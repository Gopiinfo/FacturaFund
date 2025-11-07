using BE;
using DL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DL.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<List<Customer>> GetAllCustomer()
        {
            try
            {
                List<ParamInfo> parameters = new List<ParamInfo>();
                List<Customer> res = await DataHelper.GetRecords<Customer>(Constant.SP.GetCustomerDetails, parameters, "CustomerRepository.GetAllCustomer");

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<long> CustomerInsert(DatatableInput datatable)
        {
            try
            {
                Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
                dic.Add("CustomerDetails", datatable.CustomerDataTable);
                dic.Add("LoanDetails", datatable.LoanDataTable);
                dic.Add("BankDetails", datatable.BankDataTable);
                dic.Add("DocumentDetails", datatable.DocumentDataTable);

                long res = await DataHelper.ExecuteMultiDataTableWithLongOutput(Constant.SP.CustomerWizardCreation, dic, "CustomerRepository.CustomerInsert");

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ErrorLog(ErrorLog log)
        {
            try
            {
                DataHelper.LogErrorinDB(log.LoginId, log.Url, log.ErrorMessage, log.StackTrace);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
