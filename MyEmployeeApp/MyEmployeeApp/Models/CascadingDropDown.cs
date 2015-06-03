using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;

namespace MyEmployeeApp.Models
{
    public class State
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }
    }
}