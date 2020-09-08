using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ScreenPermissions 
    {
        [Key]
        public long Id { get; set; }

        public long ScreenId { get; set; }
        public long PermissionId { get; set; }
    }
}
