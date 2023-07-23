using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Dtos.Student
{
    public class AddStudentDto
    {
        public required string FirstName { get; set; }
        public required bool IsMale { get; set; } = true;
        public required DateTime DateOfBirth { get; set; }
        public required string PlaceOfBirth { get; set; }
        public Religion Religion { get; set; }
        public required DateTime JoinDate { get; set; } = DateTime.Now;
        public DateTime? LeaveDate { get; set; }
        public required int Landline { get; set; }
        public required bool IsActive { get; set; }
        public required int AddressId { get; set; }

    }
}