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
    public int Id { get; set; }

    public DateOnly? LessonDate { get; set; }

    public TimeOnly? LessonTime { get; set; }

    [StringLength(50)]
    public string? LessonType { get; set; }

    [Column("Student_id")]
    public int? StudentId { get; set; }

    [Column("Instructor_id")]
    public int? InstructorId { get; set; }

    [Column(TypeName = "text")]
    public string? Comments { get; set; }

    [ForeignKey("InstructorId")]
    [InverseProperty("Lessons")]
    public virtual Teacher? Instructor { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Lessons")]
    public virtual Student? Student { get; set; }
}
