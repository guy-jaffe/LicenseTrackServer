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
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    [StringLength(250)]
    public string Email { get; set; } = null!;

    [Column("first_name")]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Column("pass")]
    [StringLength(50)]
    public string Pass { get; set; } = null!;

    [Column("city")]
    [StringLength(50)]
    public string? City { get; set; }

    [Column("file_extension")]
    [StringLength(50)]
    public string? FileExtension { get; set; }

    public bool IsManager { get; set; }
}
