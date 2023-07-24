using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Dtos.Medical
{
    public class AddVaccineDto
    {
        public required string Name { get; set; }
        public required VaccineType Type { get; set; }
    }
}