using RCBE.Models;
using RCBE.DTO;
using RCBE.Helper;
using Microsoft.AspNetCore.Mvc;
namespace RCBE.Interface
{
    public interface IUserService
    {

        //  Dang ky User
        Task<UserDTO?> ResgisterUser(UserRegister userRegister, string hahsedPassword);

        // User origin
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<ApiResponse<UserDTO>> GetUsersWithPaginationAsync(int currentPage, int pageSize);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        //Task<ApiResponse<string>> DeleteUserAsync(Guid id);

        Task<User?> UpdateUserAsync( Guid id ,UpdateUser update);

        //Task UpdateRole(string roleName);

        //userDTO
        Task<UserDTO?> GetUserDTOAsync(Guid id);
        
        //User related role
        Task<UserRole?> CreateUserRoleAsync(UserRole role);
        Task<User?> GetUserByEmailAsync(string email);


        // lay role
        Task<Role?> getRoleIdByRoleName(string roleName);

        Task<ApiResponse<String>> ActiveUser(string email, string activeCode);

        Task<ApiResponse<UserDTO>> ChangePassword(Guid userId, ChangePasswordModel changePasswordModel);


        Task<ApiResponse<string>> CheckEmailExists(string email);

        Task<ApiResponse<string>> SendResetLink(string email,string resetToken);

        Task<ApiResponse<string>> CheckResetToken(string email,string resetToken);

        Task<ApiResponse<string>> ResetPassword(ResetPasswordModel resetPasswordModel);


        //Task<ApiResponse<UserDTO>> SearchUserKeyword(string filter, int currentPage, int pageSize);
    }
}
