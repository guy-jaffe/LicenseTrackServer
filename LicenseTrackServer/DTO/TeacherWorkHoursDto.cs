namespace LicenseTrackServer.DTO
{
    public class TeacherWorkHoursDto
    {
        public int TeacherId { get; set; }
        public DateTime DayDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
