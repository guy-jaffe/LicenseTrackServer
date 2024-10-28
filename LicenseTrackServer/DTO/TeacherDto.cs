namespace LicenseTrackServer.DTO
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public bool? ManualCar { get; set; }
        public string VehicleType { get; set; }
        public string TeachingArea { get; set; }
        public bool? ConfirmationStatus { get; set; }


        public TeacherDto() { }
        public TeacherDto(Models.Teacher modelTeacher)
        {
            this.Id = modelTeacher.Id;
            this.SchoolName = modelTeacher.SchoolName;
            this.ManualCar = modelTeacher.ManualCar;
            this.VehicleType = modelTeacher.VehicleType;
            this.TeachingArea = modelTeacher.TeachingArea;
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
                TeachingArea = this.TeachingArea,
                ConfirmationStatus = this.ConfirmationStatus
            };

            return modelTeacher;
        }
    }
}
