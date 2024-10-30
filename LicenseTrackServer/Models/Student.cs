using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LicenseTrackServer.Models;

[Table("students")]
public partial class Student
{
    [Key]
    public int Id { get; set; }

    [Column("Lesson_count")]
    public int? LessonCount { get; set; }

    [StringLength(50)]
    public string? Street { get; set; }

    [Column("License_acquisition_date")]
    public DateOnly? LicenseAcquisitionDate { get; set; }

    [Column("License_status")]
    public int? LicenseStatus { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
