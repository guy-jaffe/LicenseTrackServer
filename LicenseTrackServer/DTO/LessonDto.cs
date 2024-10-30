namespace LicenseTrackServer.DTO
{
    public class LessonDto
    {
        public int Id { get; set; }
        public DateOnly? LessonDate { get; set; }
        public TimeOnly? LessonTime { get; set; }
        public string LessonType { get; set; }
        public int? StudentId { get; set; }
        public int? InstructorId { get; set; }
        public string? Comments { get; set; }

        public LessonDto() { }
        public LessonDto(Models.Lesson modelLesson)
        {
            this.Id = modelLesson.Id;
            this.LessonDate = modelLesson.LessonDate;
            this.LessonTime = modelLesson.LessonTime;
            this.LessonType = modelLesson.LessonType;
            this.StudentId = modelLesson.StudentId;
            this.InstructorId = modelLesson.InstructorId;
            this.Comments = modelLesson.Comments;
        }

        public Models.Lesson GetModels()
        {
            Models.Lesson modelLesson = new Models.Lesson()
            {
                Id = this.Id,
                LessonDate = this.LessonDate,
                LessonTime = this.LessonTime,
                LessonType = this.LessonType,
                StudentId = this.StudentId,
                InstructorId = this.InstructorId,
                Comments = this.Comments
            };

            return modelLesson;
        }
    }
}
