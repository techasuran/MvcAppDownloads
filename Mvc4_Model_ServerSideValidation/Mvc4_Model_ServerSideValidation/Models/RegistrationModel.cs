using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Text.RegularExpressions;


namespace Mvc4_Model_ServerSideValidation.Models
{
    public class RegistrationModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public bool TermsAccepted { get; set; }
    }

    public class RegistrationMetaModel
    {
        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "UserName")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Country")]
        public Country Country { get; set; }

        [Display(Name = "City")]
        public City City { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        [Display(Name = "Address")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile No")]
        [Display(Name = "Mobile")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        public string MobileNo { get; set; }

        [MustBeTrue(ErrorMessage = "Please Accept the Terms & Conditions")]
        public bool TermsAccepted { get; set; }
    }

    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }

    }

    public class MustBeSelectedAttribute : ValidationAttribute, IClientValidatable // IClientValidatable for client side Validation
    {
        public override bool IsValid(object value)
        {
            if (value == null || (int)value == 0)
                return false;
            else
                return true;
        }
        // Implement IClientValidatable for client side Validation
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelClientValidationRule[] { new ModelClientValidationRule { ValidationType = "dropdown", ErrorMessage = this.ErrorMessage } };
        }
    }

    public class Country
    {
        [MustBeSelectedAttribute(ErrorMessage = "Please Select Country")]
        public int? ID { get; set; }
        public string Name { get; set; }

    }

    public class City
    {
        [MustBeSelectedAttribute(ErrorMessage = "Please Select City")]
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? Country { get; set; }
    }
}