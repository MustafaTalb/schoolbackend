using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Dtos.Medical
{
    public class AddTakenVaccine
    {
        public required int StudentId { get; set; }
        public required int VaccineId { get; set; }
        public required DateTime ShotDate { get; set; }
        public string? Notes { get; set; }
    }
}