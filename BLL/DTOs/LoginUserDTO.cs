using DAL.VOs;

namespace BLL.DTOs
{
    public record class LoginUserDTO
    {
        public int id { get; set; }
        public string? user_id { get; set; }
        public List<Role> Roles { get; set; }
    }
    public record class LoginDTO
    {
        public string? UserId { get; set; }
        public string? Password { get; set; }

    }
}
