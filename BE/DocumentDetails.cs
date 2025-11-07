using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class DocumentDetails
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string BankPassBook { get; set; }
        public string Aadhaar { get; set; }
        public string Singnature { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
