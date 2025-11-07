using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public class CustomerDetailsVM
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "FirstName is Required !")]
        [StringLength(20, ErrorMessage = "FirstName length can't be more than 20.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is Required !")]
        [StringLength(20, ErrorMessage = "LastName length can't be more than 20.")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "DateOfBirth is Required !")]
        public string DateOfBirth { get; set; }


        [Required(ErrorMessage = "PANNumber is Required !")]
        [StringLength(10, ErrorMessage = "PANNumber length can't be more than 10.", MinimumLength = 10)]
        public string PANNumber { get; set; }


        [Required(ErrorMessage = "Gender is Required !")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "MaritalStatus is Required !")]
        public string MaritalStatus { get; set; }


        [Required(ErrorMessage = "Mobile1 is Required !")]
        [StringLength(10, ErrorMessage = "Mobile1 length can't be more than 10.", MinimumLength = 10)]
        public string Mobile1 { get; set; }


        [Required(ErrorMessage = "Mobile2 is Required !")]
        [StringLength(10, ErrorMessage = "Mobile2 length can't be more than 10.", MinimumLength = 10)]
        public string Mobile2 { get; set; }

        public string Mobile3 { get; set; }


        [Required(ErrorMessage = "Address is Required !")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Pincode is Required !")]
        [StringLength(6, ErrorMessage = "Pincode length can't be more than 6.", MinimumLength = 6)]
        public string Pincode { get; set; }


        [Required(ErrorMessage = "Email is Required !")]
        [StringLength(50, ErrorMessage = "Email length can't be more than 50.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Please Enter valid Email ! ")]
        public string Email { get; set; }


        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
