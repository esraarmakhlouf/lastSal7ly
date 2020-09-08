using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class ChangePassword
    {
        [Remote("ValidatePassword", "Account",ErrorMessage = "Please enter a valid password.")]
        [Required(ErrorMessage = "Current Password Required")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,}$", ErrorMessage = "PasswordRequirements")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password Required")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,}$", ErrorMessage = "PasswordRequirements")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        [DataType(DataType.Password)]        
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,}$", ErrorMessage = "PasswordRequirements")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="New Password and Confirm Password Must be Matched")]
        public string ConfirmNewPassword { get; set; } 
    }
}
