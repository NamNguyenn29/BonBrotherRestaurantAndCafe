
using System.ComponentModel.DataAnnotations;

namespace RCBE.Models
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Role name is required.")]
        [MaxLength(100, ErrorMessage = "Role name cannot exceed 100 characters.")]
        public string RoleName { get; set; }
        [Range(0, 1, ErrorMessage = "Status must be 0 (inactive) or 1 (active).")]
        public int Status { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}



