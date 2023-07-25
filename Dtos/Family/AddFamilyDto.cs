using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Dtos.Family
{
    public class AddFamilyDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required int FatherId { get; set; }
        public required int MotherId { get; set; }
    }
}