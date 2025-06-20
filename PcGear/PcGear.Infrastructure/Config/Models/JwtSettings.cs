namespace PcGear.Infrastructure.Config.Models
{
    public class JWTSettings
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; } = "Backend";
        public string Audience { get; set; } = "Frontend";
        public int ExpiryInMinutes { get; set; } = 1440;
    }
}