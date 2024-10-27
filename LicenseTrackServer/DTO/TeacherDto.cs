namespace LicenseTrackServer.DTO
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public bool ManualCar { get; set; }
        public string VehicleType { get; set; }
        public string TeachingArea { get; set; }
        public bool ConfirmationStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
