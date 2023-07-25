using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Dtos.Student
{
    public class UpdateStudentDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public bool IsMale { get; set; } = true;
        public DateTime DateOfBirth { get; set; }
        public required string PlaceOfBirth { get; set; }
        public Religion Religion { get; set; }
        public required DateTime JoinDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public required int Landline { get; set; }
        public required bool IsActive { get; set; }
        public required int Height { get; set; }
        public required int Weight { get; set; }
        public required int AddressId { get; set; }
    }
}