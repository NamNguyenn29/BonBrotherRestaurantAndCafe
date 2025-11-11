namespace RCBE.DTO
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}
