using DAL.VOs;

namespace BLL.DTOs
{
    public record class LoginUserDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<Role> Roles { get; set; } = new();
    }
    public record class LoginDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public record class CreateUserDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
