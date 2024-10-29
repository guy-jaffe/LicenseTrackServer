using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LicenseTrackServer.Models;

[Table("lessons")]
public partial class Lesson
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("lessonDate")]
    public DateOnly? LessonDate { get; set; }

    [Column("lessonTime")]
    public TimeOnly? LessonTime { get; set; }

    [Column("lessonType")]
    [StringLength(50)]
    public string? LessonType { get; set; }

    [Column("student_id")]
    public int? StudentId { get; set; }

    [Column("instructor_id")]
    public int? InstructorId { get; set; }

    [Column("comments", TypeName = "text")]
    public string? Comments { get; set; }

    [ForeignKey("InstructorId")]
    [InverseProperty("Lessons")]
    public virtual Teacher? Instructor { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Lessons")]
    public virtual Student? Student { get; set; }
}
