using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Dtos.Addresses
{
    public class AddAddressDto
    {
        public required string Name { get; set; }
        public string? Details { get; set; }
        public int StreetId { get; set; }
    }
}