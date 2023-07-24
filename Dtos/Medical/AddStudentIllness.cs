using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Dtos.Medical
{
    public class AddStudentIllness
    {
        public required int StudentId { get; set; }
        public required int IllnessId { get; set; }
        public string? Notes { get; set; }
        public required bool IsCured { get; set; }
        public DateTime? DateOfTreatment { get; set; }
    }
}