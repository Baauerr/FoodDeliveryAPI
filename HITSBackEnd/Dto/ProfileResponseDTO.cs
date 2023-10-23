﻿using HITSBackEnd.baseClasses;
using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.Dto
{
    public class ProfileResponseDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
