using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class StudentModel
    {
        //[Display(Name = "Id")]
        public int studentId { get; set; }

        //[Required(ErrorMessage = "First name is required.")]
        [Display(Name = "Student Name")]
        public string studentName { get; set; }

        //[Required(ErrorMessage = "City is required.")]
        [Display(Name = "Student Age")]
        public int studentAge { get; set; }

        //[Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Student Class")]
        public int studentClass { get; set; }
    }
}