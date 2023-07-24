using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Dtos.Medical
{
    public class AddIllnessDto
    {
        public required string Name { get; set; }
        public IllnessType Type { get; set; }
    }
}