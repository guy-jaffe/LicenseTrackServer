namespace LicenseTrackServer.DTO
{
    public class TeacherDto : UsersDTO
    {
        public string SchoolName { get; set; }
        public bool? ManualCar { get; set; }
        public string VehicleType { get; set; }
        public bool? ConfirmationStatus { get; set; }


        public TeacherDto() { }
        public TeacherDto(Models.Teacher modelTeacher, string rootPath) :base(modelTeacher.IdNavigation, rootPath)
        {
            this.SchoolName = modelTeacher.SchoolName;
            this.ManualCar = modelTeacher.ManualCar;
            this.VehicleType = modelTeacher.VehicleType;
            this.ConfirmationStatus = modelTeacher.ConfirmationStatus;
        }

        public Models.Teacher GetModels()
        {
            Models.Teacher modelTeacher = new Models.Teacher()
            {
                Id = this.Id,
                SchoolName = this.SchoolName,
                ManualCar = this.ManualCar,
                VehicleType = this.VehicleType,
                ConfirmationStatus = this.ConfirmationStatus,
                IdNavigation = new Models.User()
                {
                    Id = this.Id,
                    City = this.City,
                    Email = this.Email,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    FileExtension = this.FileExtension,
                    IsManager = this.IsManager,
                    Pass = this.PasswordHash
                }
            };

            return modelTeacher;
        }
    }
}
