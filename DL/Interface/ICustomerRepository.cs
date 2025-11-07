using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomer();
        Task<long> CustomerInsert(DatatableInput datatable);
        void ErrorLog(ErrorLog errorLog);
    }
}
