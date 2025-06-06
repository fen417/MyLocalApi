namespace MyLocalApi.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "student"; // роль по умолчанию
        public string? GroupName { get; set; }
    }
}
