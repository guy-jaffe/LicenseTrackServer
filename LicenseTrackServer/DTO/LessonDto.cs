namespace LicenseTrackServer.DTO
{
    public class LessonDto
    {
        public int Id { get; set; }
        public DateTime LessonDate { get; set; }
        public TimeSpan LessonTime { get; set; }
        public string LessonType { get; set; }
        public int StudentId { get; set; }
        public int InstructorId { get; set; }
        public string Comments { get; set; }
    }
}
