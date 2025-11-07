using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VM
{
    public class DocumentDetailsVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "Bank Pass Book is Required !")]
        public IFormFile BankPassBook { get; set; }
        public string BankPassBookFileName { get; set; }


        [Required(ErrorMessage = "Aadhaar is Required !")]
        public IFormFile Aadhaar { get; set; }
        public string AadhaarFileName { get; set; }



        [Required(ErrorMessage = "Singnature is Required !")]
        public IFormFile Singnature { get; set; }
        public string SingnatureFileName { get; set; }

        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
