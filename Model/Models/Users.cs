using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Model
{
    public partial class Users : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string ArabicName { get; set; } = "";

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string EnglishName { get; set; } = "";


        [Required(ErrorMessage = "Required")]
        [Remote("CheckExsistUserName", "Users", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = "Already Exists User Name.")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        public string UserName { get; set; }

        [Remote("CheckExsistEmail", "Users", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = "Already Exists Email Address.")]
        [Required(ErrorMessage = "Email Required")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "NotValidEmailAddress")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,}$", ErrorMessage = "PasswordRequirements")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mobile Number Required")]
        [Remote("CheckExsistMobile", "Users", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = "Already Exists Mobile Number.")]
        [RegularExpression(@"^\+?\d+$", ErrorMessage = "Numbers Only")]
        public string Mobile { get; set; }

        //[Required(ErrorMessage = "JobTitle Required")]
        public long? JobTitleId { get; set; }
        [ForeignKey("JobTitleId")]
        public virtual JobTitle JobTitle { get; set; }

        public long? CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public long? DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
        public bool IsManager { get; set; } = false;
        public string Location { get; set; }
        public bool IsMaster { get; set; } = false;
        public string Token { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public bool OnLine { get; set; } = false;
        public bool IsCompanyManager { get; set; } = false;
        public string ImageName { get; set; }
        public virtual Technical Technical { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
