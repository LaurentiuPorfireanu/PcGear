﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcGear.Database.Entities
{
    public class Manufacturer:BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(200)]
        public string? Website { get; set; }


        public List<Product> Products { get; set; } = [];  
    }
}
