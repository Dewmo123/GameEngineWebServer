namespace BLL.DTOs
{
    public record class LoginUserDTO
    {
        public string? Id { get; set; }
        public string? Password { get; set; }
    }
}
