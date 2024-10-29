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
    [Column("teacher_id")]
    public int TeacherId { get; set; }

    [Key]
    [Column("dayDate")]
    public DateOnly DayDate { get; set; }

    [Key]
    [Column("start_time")]
    public TimeOnly StartTime { get; set; }

    [Column("end_time")]
    public TimeOnly? EndTime { get; set; }

    [ForeignKey("TeacherId")]
    [InverseProperty("TeacherWorkHours")]
    public virtual Teacher Teacher { get; set; } = null!;
}
