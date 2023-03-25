namespace PerfumeStores.Core.DTOs
{
    public class LoginDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public static int? CustomerID { get; set; } = null;
        public static bool IsAdmin { get; set; } = false;
    }
}
