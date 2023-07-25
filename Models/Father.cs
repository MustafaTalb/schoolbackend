using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Models
{
    public class Father
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FatherName { get; set; }
        public required string MotherName { get; set; }
        public required string Career { get; set; }
        public required string PlaceOfResidence { get; set; }
        public required int TieNumber { get; set; }
        public required string TiePlace { get; set; }
        public required string PlaceOfBirth { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required Religion Religion { get; set; }
        public required EducationStatus EducationStatus { get; set; }
        public required string PhoneNumber { get; set; }

    }
}