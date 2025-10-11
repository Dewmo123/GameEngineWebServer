using DAL.VOs;

namespace BLL.DTOs
{
    public record class LoginUserDTO
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public List<Role>? Roles { get; set; }
    }
    public record class LoginDTO
    {
        public string? UserId { get; set; }
        public string? Password { get; set; }
    }
    public record class CreateUserDTO
    {
        public string? UserId { get; set; }
        public string? Password { get; set; }
    }
}