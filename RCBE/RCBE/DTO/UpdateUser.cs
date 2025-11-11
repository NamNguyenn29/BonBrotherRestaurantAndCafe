namespace RCBE.DTO
{
    public class UpdateUser
    {
        public string? FullName { get; set; }
        public required string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Phone { get; set; }
    }
}
