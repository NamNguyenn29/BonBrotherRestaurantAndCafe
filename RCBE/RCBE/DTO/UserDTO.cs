namespace RCBE.DTO
{
    using RCBE.Models;
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public required string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public  bool IsActived { get; set; }
        public DateTime CreatedDate { get; set; }
        public required List<String> RoleList { get; set; }


    }
}


