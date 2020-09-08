using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
   public class SignUpModelForAPI
    {
        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\+?\d+$", ErrorMessage = "Numbers Only")]
        public string Mobile { get; set; }

        //[Index(ErrorMessage = "Duplicate Email Occured")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "NotValidEmailAddress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
  
        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }

    }
}
