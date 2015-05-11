using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc4_Model_ServerSideValidation.Models;
using System.Text.RegularExpressions;

namespace Mvc4_Model_ServerSideValidation.Controllers
{
    public class HomeController : Controller
    {
        #region Private Methods
        void BindCountry()
        {
            List<Country> lstCountry = new List<Country>{new Country { ID = null, Name = "Select" },
                                                         new Country { ID = 1, Name = "India" },
                                                         new Country { ID = 2, Name = "USA" }
                                                        };

            ViewBag.Country = lstCountry;
        }

        //for server side
        void BindCity(int? mCountry)
        {
            try
            {
                if (mCountry != 0)
                {
                    //below code is only for demo, since city list will come from database based on country
                    //Hence remove below lines of code when you will query data from city table with in database based on country 
                    int index = 1;
                    List<City> lstCity = new List<City>{
                 
                     new City
                    {
                        Country = 0,
                        ID=null,   // using index++ for making unique ID for city
                        Name = "Select"
                    },
                        new City
                    {
                        Country = 1,
                        ID=index++,   // using index++ for making unique ID for city
                        Name = "Delhi"
                    },
                new City
                    {
                        Country = 1,
                        ID=index++,
                        Name = "Lucknow"
                    },
                new City
                    {
                        Country = 1,
                        ID=index++,
                        Name = "Noida"
                    },
                new City
                    {
                        Country = 1,
                        ID=index++,
                        Name = "Guragon"
                    },
                new City
                    {
                        Country = 1,
                        ID=index++,
                        Name = "Pune"
                    },
                
                new City
                    {
                        Country = 2,
                        ID=index++,
                        Name = "New-York"
                    },
                   new City
                    {
                        Country = 2,
                        ID=index++,
                        Name = "California"
                    },
                new City
                    {
                        Country = 2,
                        ID=index++,
                        Name = "Washington"
                    },
                new City
                    {
                        Country = 2,
                        ID=index++,
                        Name = "Vermont"
                    },
                   
            };

                    var city = from c in lstCity
                               where c.Country == mCountry || c.Country == 0
                               select c;

                    ViewBag.City = city;

                }
                else
                {
                    List<City> City = new List<City> { new City { ID = null, Name = "Select" } };
                    ViewBag.City = City;
                }
            }
            catch (Exception ex)
            {

            }


        }
        #endregion

        //for client side using jquery 
        //jQuery Library used
        public JsonResult CityList(int mCountry)
        {
            try
            {
                if (mCountry != 0)
                {
                    //below code is only for demo, since city list will come from database based on country
                    //Hence remove below lines of code when you will query data from city table with in database based on country 
                    int index = 1;
                    List<City> lstCity = new List<City>{
                    
                 new City
                    {
                        Country = 0,
                        ID=null,   // using index++ for making unique ID for city
                        Name = "Select"
                    },
                        new City
                    {
                        Country = 1,
                        ID=index++,   // using index++ for making unique ID for city
                        Name = "Delhi"
                    },
                new City
                    {
                        Country = 1,
                        ID=index++,
                        Name = "Lucknow"
                    },
                new City
                    {
                        Country = 1,
                        ID=index++,
                        Name = "Noida"
                    },
                new City
                    {
                        Country = 1,
                        ID=index++,
                        Name = "Guragon"
                    },
                new City
                    {
                        Country = 1,
                        ID=index++,
                        Name = "Pune"
                    },
                
                new City
                    {
                        Country = 2,
                        ID=index++,
                        Name = "New-York"
                    },
                   new City
                    {
                        Country = 2,
                        ID=index++,
                        Name = "California"
                    },
                new City
                    {
                        Country = 2,
                        ID=index++,
                        Name = "Washington"
                    },
                new City
                    {
                        Country = 2,
                        ID=index++,
                        Name = "Vermont"
                    },
                   
            };

                    var city = from c in lstCity
                               where c.Country == mCountry || c.Country == 0
                               select c;

                    //  return Json(city, JsonRequestBehavior.AllowGet);
                    return Json(new SelectList(city.ToArray(), "ID", "Name"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(null);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Demo()
        {
            Response.Redirect("http://www.dotnet-tricks.com/Tutorial/mvclist");
            return View();
        }

        public ActionResult ExplicitServer()
        {
            BindCountry();
            BindCity(0);
            return View();
        }

        [HttpPost]
        public ActionResult ExplicitServer(RegistrationModel mRegister)
        {
            if (string.IsNullOrEmpty(mRegister.UserName))
            {
                ModelState.AddModelError("UserName", "Please enter your name");
            }
            if (!string.IsNullOrEmpty(mRegister.UserName))
            {
                Regex emailRegex = new Regex(".+@.+\\..+");

                if (!emailRegex.IsMatch(mRegister.UserName))
                    ModelState.AddModelError("UserName", "Please enter correct email address");
            }

            if (string.IsNullOrEmpty(mRegister.Password))
            {
                ModelState.AddModelError("Password", "Please enter password");
            }

            if (string.IsNullOrEmpty(mRegister.ConfirmPassword))
            {
                ModelState.AddModelError("ConfirmPassword", "Please enter confirm password");
            }

            if (string.IsNullOrEmpty(mRegister.ConfirmPassword) && string.IsNullOrEmpty(mRegister.ConfirmPassword))
            {
                if (mRegister.ConfirmPassword != mRegister.Password)
                    ModelState.AddModelError("ConfirmPassword", "Confirm password doesn't match");
            }

            if (mRegister.Country.ID == null || mRegister.Country.ID == 0)
            {
                ModelState.AddModelError("Country", "Please select Country");
            }
            if (mRegister.City.ID == null || mRegister.City.ID == 0)
            {
                ModelState.AddModelError("City", "Please select City");
            }

            if (string.IsNullOrEmpty(mRegister.Address))
            {
                ModelState.AddModelError("Address", "Please enter your address");
            }

            if (string.IsNullOrEmpty(mRegister.MobileNo))
            {
                ModelState.AddModelError("MobileNo", "Please enter your mobile no");
            }
            if (!mRegister.TermsAccepted)
            {
                ModelState.AddModelError("TermsAccepted", "You must accept the terms");
            }
            if (ModelState.IsValid)
            {
                return View("Completed");
            }
            else
            {
                ViewBag.Selpwd = mRegister.Password;
                ViewBag.Selconfirmpwd = mRegister.ConfirmPassword;
                BindCountry();

                if (mRegister.Country != null)
                {
                    BindCity(mRegister.Country.ID);
                }
                else
                    BindCity(null);

                return View();
            }

        }

        public ActionResult ServerMeta()
        {
            BindCountry();
            BindCity(0);
            return View();
        }
        [HttpPost]
        public ActionResult ServerMeta(RegistrationMetaModel mRegister)
        {
            if (ModelState.IsValid)
            {
                return View("Completed");
            }
            else
            {
                ViewBag.Selpwd = mRegister.Password;
                ViewBag.Selconfirmpwd = mRegister.ConfirmPassword;
                //ViewBag.SelCountry = mRegister.Country;
                BindCountry();
                //ViewBag.SelCity = mRegister.City;

                if (mRegister.Country != null)
                    BindCity(mRegister.Country.ID);
                else
                    BindCity(null);

                return View();
            }

        }


    }
}
