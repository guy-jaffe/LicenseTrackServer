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
    [Column("id")]
    public int Id { get; set; }

    [Column("lesson_count")]
    public int? LessonCount { get; set; }

    [Column("street")]
    [StringLength(50)]
    public string? Street { get; set; }

    [Column("license_acquisition_date")]
    public DateTime LicenseAcquisitionDate { get; set; }

    [Column("license_status")]
    public int? LicenseStatus { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
