using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.VOs
{
    public enum Role
    {
        None,
        User,
        Admin
    }
    public record class LoginVO
    {
        public int id { get; set; }
        public string? user_id { get; set; }
    }
    public record class UserRoleVO
    {
        public Role Role;
    }
}
