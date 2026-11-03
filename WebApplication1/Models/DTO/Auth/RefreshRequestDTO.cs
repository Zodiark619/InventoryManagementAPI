namespace WebApplication1.Models.DTO.Auth
{
    public class RefreshRequestDTO
    {
        public string RefreshToken { get; set; }
    }
    public class RefreshResponseDTO
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}
