using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Dtos.Addresses
{
    public class FullAddress
    {
        public required City City { get; set; }
        public required Area Area { get; set; }
        public required Street Street { get; set; }
        public required Address Address { get; set; }

    }
}