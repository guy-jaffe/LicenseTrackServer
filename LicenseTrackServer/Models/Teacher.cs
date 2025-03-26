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
    public int Id { get; set; }

    [Column("School_name")]
    [StringLength(50)]
    public string? SchoolName { get; set; }

    [Column("Manual_car")]
    public bool? ManualCar { get; set; }

    [Column("Vehicle_type")]
    [StringLength(50)]
    public string? VehicleType { get; set; }

    [Column("Teaching_area")]
    [StringLength(50)]
    public string? TeachingArea { get; set; }

    public int? ConfirmationStatus { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Teacher")]
    public virtual User IdNavigation { get; set; } = null!;

    [InverseProperty("Instructor")]
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    [InverseProperty("Teacher")]
    public virtual ICollection<TeacherWorkHour> TeacherWorkHours { get; set; } = new List<TeacherWorkHour>();
}
