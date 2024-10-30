namespace LicenseTrackServer.DTO
{
    public class StudentDto
    {
        public int Id { get; set; }
        public int? LessonCount { get; set; }
        public string Street { get; set; }
        public DateOnly? LicenseAcquisitionDate { get; set; }
        public int? LicenseStatus { get; set; } // Consider using an enum for better clarity

        public StudentDto() { }
        public StudentDto(Models.Student modelStudent)
        {
            this.Id = modelStudent.Id;
            this.LessonCount = modelStudent.LessonCount;
            this.Street = modelStudent.Street;
            this.LicenseAcquisitionDate = modelStudent.LicenseAcquisitionDate;
            this.LicenseStatus = modelStudent.LicenseStatus;
        }

        public Models.Student GetModels()
        {
            Models.Student modelStudent = new Models.Student()
            {
                Id = this.Id,
                LessonCount = this.LessonCount,
                Street = this.Street,
                LicenseAcquisitionDate = this.LicenseAcquisitionDate,
                LicenseStatus = this.LicenseStatus
            };

            return modelStudent;
        }
    }
}
