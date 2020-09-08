using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class GroupPermissions 
    {
        [Key]
        public long Id { get; set; }

        public long GroupId { get; set; }
        public long PermissionId { get; set; }
        public long ScreenId { get; set; }
    }
}
