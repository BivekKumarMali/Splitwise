﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

    }
}