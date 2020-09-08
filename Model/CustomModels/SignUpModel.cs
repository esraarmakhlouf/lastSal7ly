using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string Name { get; set; }

        [RegularExpression(@"^\+?\d+$", ErrorMessage = "Numbers Only")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Required")]
        public long NationalityId { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "NotValidEmailAddress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password", ErrorMessage = "Must Password and Comfarm Password be same")]
        public string ComPassword { get; set; }

        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public long CityId { get; set; }
        public long? DistrictId { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }
    }

    public class ShopSearchModel
    {
        public int CurrentPage { get; set; } = 1;
        public int PageLength { get; set; } = 20;
        public string SortBy { get; set; } = "Name";

        public string SearchTerm { get; set; } = "";
        public HashSet<long> SelectedCategories { get; set; } = new HashSet<long>();
        public HashSet<long> SelectedSubCategories { get; set; } = new HashSet<long>();
        public HashSet<long> SelectedVendors { get; set; } = new HashSet<long>();
        public HashSet<long> SelectedColors { get; set; } = new HashSet<long>();
        public HashSet<long> SelectedSizes { get; set; } = new HashSet<long>();
        public HashSet<long> SelectedMaterials { get; set; } = new HashSet<long>();
        public HashSet<PriceRangeModel> SelectedPriceRanges { get; set; } = new HashSet<PriceRangeModel>();
    }

    public class PriceRangeModel
    {
        public double MinPrice { get; set; } = 0;
        public double MaxPrice { get; set; } = 0;
    }
}
