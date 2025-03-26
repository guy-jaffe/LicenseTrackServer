using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LicenseTrackServer.Models;

[Table("users")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Email { get; set; } = null!;

    [Column("First_name")]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Column("Last_name")]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(50)]
    public string Pass { get; set; } = null!;

    [StringLength(50)]
    public string? City { get; set; }

    [Column("File_extension")]
    [StringLength(50)]
    public string? FileExtension { get; set; }

    public bool IsManager { get; set; }

    [StringLength(15)]
    public string? PhoneNum { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Student? Student { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Teacher? Teacher { get; set; }
}
