using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml.Serialization;
using MyEmployeeApp.Models;
using System.Web.Script.Serialization;
using System.Xml;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MyEmployeeApp.Controllers
{
    public class HomeController : Controller
    {
        public List<Employee> lstEmployee = new List<Employee>();

        public ActionResult AddEmployee()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Result = string.Empty;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(Employee objEmployee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Employee> lstEmployee = new List<Employee>();
                    lstEmployee = LoadXMLData();
                    if (lstEmployee.Count > 0)
                    {
                        objEmployee.EmployeeId = lstEmployee[lstEmployee.Count - 1].EmployeeId + 1;
                        lstEmployee.Add(objEmployee);
                    }
                    else
                    {
                        objEmployee.EmployeeId = 1;
                        lstEmployee.Add(objEmployee);
                    }

                    SaveToXML(lstEmployee);
                    ViewBag.Result = "Successfully Saved.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Result = ex.Message.ToString();
            }
            return View(new Employee());
        }

        public JsonResult GetEmployeeData()
        {
            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee = LoadXMLData();

            return Json(lstEmployee, JsonRequestBehavior.AllowGet);
        }

        public List<Employee> LoadXMLData()
        {
            List<Employee> lstData = new List<Employee>();

            using (var reader = new StreamReader(Server.MapPath("/EmployeeData/EmployeeInfoFile.xml")))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<Employee>), new XmlRootAttribute("ArrayOfEmployee"));
                lstData = (List<Employee>)deserializer.Deserialize(reader);
            }

            return lstData;
        }

        public void SaveToXML(List<Employee> objEmp)
        {
            XmlSerializer s = new XmlSerializer(typeof(List<Employee>));
            using (TextWriter sw = new StreamWriter(Server.MapPath("/EmployeeData/EmployeeInfoFile.xml")))
            {
                s.Serialize(sw, objEmp);
            }
        }
        
        /// <summary>
        /// Get City List by State Id
        /// </summary>
        /// <param name="StateId"></param>
        /// <returns>City List</returns>
        public ActionResult GetCityByStateId(int StateId)
        {
            List<City> lstCity = new List<City>();
            lstCity = GetAllCityNames().Where(m => m.StateId == StateId).ToList();
            SelectList objCity = new SelectList(lstCity, "CityId", "Select City", 0);
            return Json(lstCity, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get All States
        /// </summary>
        /// <returns>Collection for the State</returns>
        public JsonResult GetAllStates()
        {
            List<State> lstStates = new List<State>();
            lstStates.Add(new State { StateId = 0, StateName = "Select State"});
            lstStates.Add(new State { StateId = 1, StateName = "Bangalore" });
            lstStates.Add(new State { StateId = 2, StateName = "Chennai" });            
            lstStates.Add(new State { StateId = 3, StateName = "Delhi" });
            lstStates.Add(new State { StateId = 4, StateName = "Gandhi Nagar" });
            lstStates.Add(new State { StateId = 5, StateName = "Hyderabad" });
            lstStates.Add(new State { StateId = 6, StateName = "Kolkata" });
            lstStates.Add(new State { StateId = 7, StateName = "Mumbai" });
            lstStates.Add(new State { StateId = 8, StateName = "Pune" });
            lstStates.Add(new State { StateId = 9, StateName = "Trivendram" });
            lstStates.Add(new State { StateId = 10, StateName = "Vijayawada" });

            return Json(lstStates, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get All City
        /// </summary>
        /// <returns>Collection for the City</returns>
        public List<City> GetAllCityNames()
        {
            List<City> lstCity = new List<City>();
            lstCity.Add(new City { CityId = 1, StateId = 1, CityName = "BangaloreCity1" });
            lstCity.Add(new City { CityId = 2, StateId = 1, CityName = "BangaloreCity2" });
            lstCity.Add(new City { CityId = 3, StateId = 2, CityName = "ChennaiCity1" });
            lstCity.Add(new City { CityId = 4, StateId = 2, CityName = "ChennaiCity2" });
            lstCity.Add(new City { CityId = 5, StateId = 3, CityName = "DelhiCity1" });
            lstCity.Add(new City { CityId = 6, StateId = 3, CityName = "DelhiCity2" });
            lstCity.Add(new City { CityId = 7, StateId = 4, CityName = "GandhiNagarCity1" });
            lstCity.Add(new City { CityId = 8, StateId = 4, CityName = "GandhiNagarCity2" });
            lstCity.Add(new City { CityId = 9, StateId = 5, CityName = "HyderabadCity1" });
            lstCity.Add(new City { CityId = 10, StateId = 5, CityName = "HyderabadCity2" });
            lstCity.Add(new City { CityId = 11, StateId = 6, CityName = "KolkataCity1" });
            lstCity.Add(new City { CityId = 12, StateId = 6, CityName = "KolkataCity2" });
            lstCity.Add(new City { CityId = 13, StateId = 7, CityName = "MumbaiCity1" });
            lstCity.Add(new City { CityId = 14, StateId = 7, CityName = "MumbaiCity2" });
            lstCity.Add(new City { CityId = 15, StateId = 8, CityName = "PuneCity1" });
            lstCity.Add(new City { CityId = 16, StateId = 8, CityName = "PuneCity2" });
            lstCity.Add(new City { CityId = 17, StateId = 9, CityName = "TrivendramCity1" });
            lstCity.Add(new City { CityId = 18, StateId = 9, CityName = "TrivendramCity2" });
            lstCity.Add(new City { CityId = 19, StateId = 10, CityName = "VijayawadaCity1" });
            lstCity.Add(new City { CityId = 20, StateId = 10, CityName = "VijayawadaCity2" });

            return lstCity;
        }
    }
}
