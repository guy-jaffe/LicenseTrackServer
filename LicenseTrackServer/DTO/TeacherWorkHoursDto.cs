namespace LicenseTrackServer.DTO
{
    public class TeacherWorkHoursDto
    {
        public int TeacherId { get; set; }
        public DateTime DayDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }


        public TeacherWorkHoursDto() { }
        public TeacherWorkHoursDto(Models.TeacherWorkHour modelTeacherWorkHour)
        {
            this.TeacherId = modelTeacherWorkHour.TeacherId;
            this.DayDate = modelTeacherWorkHour.DayDate;
            this.StartTime = modelTeacherWorkHour.StartTime;
            this.EndTime = modelTeacherWorkHour.EndTime;
        }

        public Models.TeacherWorkHour GetModels()
        {
            Models.TeacherWorkHour modelTeacherWorkHour = new Models.TeacherWorkHour()
            {
                TeacherId = this.TeacherId,
                DayDate = this.DayDate,
                StartTime = this.StartTime,
                EndTime = this.EndTime
            };

            return modelTeacherWorkHour;
        }
    }
}
