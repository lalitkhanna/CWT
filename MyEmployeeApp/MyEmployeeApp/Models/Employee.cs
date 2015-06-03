using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace MyEmployeeApp.Models
{
    public class Employee
    {
        [Display(Name="Employee Id")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage="Employee Name Required")]
        [Display(Name="Employee Name")]
        public string EmpName { get; set; }

        [Required(ErrorMessage = "Designation Required")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Display(Name = "State")]
        public int State { get; set; }

        public string StateName { get; set; }

        [Display(Name = "City")]
        public int City { get; set; }

        public string CityName { get; set; }
    }
}