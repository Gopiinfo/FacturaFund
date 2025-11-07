using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class APIURL
    {
        public string AccountServiceURL { get; set; }
        public string AdminServiceURL { get; set; }
        public string CustomerServiceURL { get; set; }
    }

    public class ErrorLog
    {
        public string LoginId { get; set; }
        public string Url { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
    }

    public class FileStatus
    {
        public MemoryStream memoryStream { get; set; }
        public string contentType { get; set; }
        public string fileName { get; set; }
        public bool status { get; set; }
        public byte[] fileBytes { get; set; }
    }
}
