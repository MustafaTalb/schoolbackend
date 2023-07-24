using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Models
{
    public class Illness
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public IllnessType Type { get; set; }
    }
}