using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LicenseTrackServer.Models;

[Table("teachers")]
public partial class Teacher
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("school_name")]
    [StringLength(50)]
    public string? SchoolName { get; set; }

    [Column("manual_car")]
    public bool? ManualCar { get; set; }

    [Column("vehicle_type")]
    [StringLength(50)]
    public string? VehicleType { get; set; }

    [Column("teaching_area")]
    [StringLength(50)]
    public string? TeachingArea { get; set; }

    public bool? ConfirmationStatus { get; set; }

    [InverseProperty("Instructor")]
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    [InverseProperty("Teacher")]
    public virtual ICollection<TeacherWorkHour> TeacherWorkHours { get; set; } = new List<TeacherWorkHour>();
}
