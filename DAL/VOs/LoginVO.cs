using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.VOs
{
    public enum Role
    {
        None = 1,
        User,
        Admin
    }
    public record class LoginVO
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
    }
    public record class UserRoleVO
    {
        public Role Role { get; set; }
    }
}
