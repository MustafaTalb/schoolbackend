using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Dtos.Address
{
    public class AddStreetDto
    {
        public required string Name { get; set; }
        public int AreaId { get; set; }
    }
}