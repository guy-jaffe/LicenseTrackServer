namespace LicenseTrackServer.DTO
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string City { get; set; }
        public string FileExtension { get; set; }
        public bool IsManager { get; set; }



        public UsersDTO() { }
        public UsersDTO(Models.User modelUser)
        {
            this.Id = modelUser.Id;
            this.Email = modelUser.Email;
            this.FirstName = modelUser.FirstName;
            this.LastName = modelUser.LastName;
            this.PasswordHash = modelUser.Pass;
            this.City = modelUser.City;
            this.FileExtension = modelUser.FileExtension;
            this.IsManager = modelUser.IsManager;
        }

        public Models.User GetModels()
        {
            Models.User modelUser = new Models.User()
            {
                Id = this.Id,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Pass = this.PasswordHash,
                City = this.City,
                FileExtension = this.FileExtension,
                IsManager = this.IsManager
            };

            return modelUser;
        }
    }
}
