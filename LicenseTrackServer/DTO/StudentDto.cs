namespace LicenseTrackServer.DTO
{
    public class StudentDto
    {
        public int Id { get; set; }
        public int LessonCount { get; set; }
        public string Street { get; set; }
        public DateTime LicenseAcquisitionDate { get; set; }
        public string LicenseStatus { get; set; } // Consider using an enum for better clarity
    }
}
