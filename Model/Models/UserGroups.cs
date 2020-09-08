using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class UserGroups 
    {
        [Key]
        public long Id { get; set; }

        public long GroupId { get; set; }
        public long UserId { get; set; }
    }
}
