using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCBE.Models
{
    public class UserRole
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey(nameof(Role))]
        public Guid IdRole { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public Guid IdUser { get; set; }
        [Range(0, 3, ErrorMessage = "Status must be between 0 and 3.")]
        public int Status { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public UserRole (Guid Id, Guid IdRole, Guid IdUser, int Status, DateTime CreatedDate)
        {
            this.Id = Id;
            this.IdRole = IdRole;
            this.IdUser = IdUser;
            this.Status = Status;
            this.CreatedDate = CreatedDate;
        }


       
        public Role Role { get; set; }
        public User User { get; set; }
    }
}
