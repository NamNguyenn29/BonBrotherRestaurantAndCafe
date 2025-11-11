using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RCBE.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(100)]
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [MaxLength(15)]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(100)]
        public string Password { get; set; }
        [Range(0, 3, ErrorMessage = "Status must be between 0 and 3.")]
        public int Status { get; set; }
        [MaxLength(100)]
        public string? ActiveCode { get; set; }
        public bool IsActived { get; set; }
        [MaxLength(100)]
        public string ?ForgotPassCode { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public User(Guid id, string? fullName, string email, DateTime? dateOfBirth, string? phone, string password, int status, string activeCode, bool isActived, string? forgotPassCode, DateTime createdDate)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            Password = password;
            Status = status;
            ActiveCode = activeCode;
            IsActived = isActived;
            ForgotPassCode = forgotPassCode;
            CreatedDate = createdDate;
        }
    }
}
