﻿
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels {
    public class EmployeeModel {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public IFormFile? Photo { get; set; }        
    }
}
