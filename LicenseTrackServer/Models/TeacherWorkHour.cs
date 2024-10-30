using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LicenseTrackServer.Models;

[PrimaryKey("TeacherId", "DayDate", "StartTime")]
[Table("teacher_work_hours")]
public partial class TeacherWorkHour
{
    [Key]
    [Column("Teacher_id")]
    public int TeacherId { get; set; }

    [Key]
    public DateOnly DayDate { get; set; }

    [Key]
    [Column("Start_time")]
    public TimeOnly StartTime { get; set; }

    [Column("End_time")]
    public TimeOnly? EndTime { get; set; }

    [ForeignKey("TeacherId")]
    [InverseProperty("TeacherWorkHours")]
    public virtual Teacher Teacher { get; set; } = null!;
}
