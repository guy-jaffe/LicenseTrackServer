namespace LicenseTrackServer.DTO
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string City { get; set; }
        public string FileExtension { get; set; }
        public bool IsManager { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
