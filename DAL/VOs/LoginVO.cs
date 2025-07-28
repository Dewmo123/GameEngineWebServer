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
        public int Id;
        public string? UserId;
    }
    public record class UserRoleVO
    {
        public Role Role;
    }
}
