﻿namespace LicenseTrackServer.DTO
{
    public class StudentDto:UsersDTO
    {
        public string Street { get; set; }
        public DateOnly? LicenseAcquisitionDate { get; set; }
        public int? LicenseStatus { get; set; } // Consider using an enum for better clarity

        public StudentDto() { }
        public StudentDto(Models.Student modelStudent, string rootPath):base(modelStudent.IdNavigation, rootPath)
        {
            this.Street = modelStudent.Street;
            this.LicenseAcquisitionDate = modelStudent.LicenseAcquisitionDate;
            this.LicenseStatus = modelStudent.LicenseStatus;
        }

        public Models.Student GetModels()
        {
            Models.Student modelStudent = new Models.Student()
            {
                Id = this.Id,
                Street = this.Street,
                LicenseAcquisitionDate = this.LicenseAcquisitionDate,
                LicenseStatus = this.LicenseStatus,
                IdNavigation = new Models.User()
                {
                    Id = this.Id,
                    City = this.City,
                    Email = this.Email,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    FileExtension = this.FileExtension,
                    IsManager = this.IsManager,
                    Pass = this.PasswordHash,
                    PhoneNum = this.PhoneNum
                }
            };

            return modelStudent;
        }
    }
}
