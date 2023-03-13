namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string ldUser { get; set; }
        public string Role { get; set; }
        public string ldCard { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public string ldCompany { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordExpires { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }

}
