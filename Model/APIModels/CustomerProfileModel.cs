using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.APIModels
{
    public class CustomerProfileModel : SignUpModelForAPI
    {
        [Required(ErrorMessage = "Required")]
        public long Id { get; set; }

        public long CityId { get; set; }
    }
}
