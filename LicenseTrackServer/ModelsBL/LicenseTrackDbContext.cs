﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LicenseTrackServer.Models;

public partial class LicenseTrackDbContext : DbContext
{
    public User? GetUser(string email)
    {
        return this.Users.Where(u => u.Email == email)
            .FirstOrDefault();
    }

    public Student? GetStudent(int id)
    {
        return this.Students.Where(u => u.Id == id).Include(u => u.IdNavigation)
            .FirstOrDefault();
    }

    public Teacher? GetTeacher(int id)
    {
        return this.Teachers.Where(u => u.Id == id).Include(u => u.IdNavigation)
            .FirstOrDefault();
    }

    public List<Teacher> GetTeachers(string city)
    {
        return Teachers.Where(t => t.TeachingArea == city).Include(t => t.IdNavigation).ToList();
    }

    public List<Teacher> GetPendingTeachers()
    {
        return Teachers.Where(t => t.ConfirmationStatus == 0).Include(t => t.IdNavigation).ToList();
    }

    public List<Teacher> GetAllTeachers()
    {
        return Teachers.Include(t => t.IdNavigation).ToList();
    }

    public List<Student> GetAllStudents()
    {
        return Students.Include(t => t.IdNavigation).ToList();
    }

}

