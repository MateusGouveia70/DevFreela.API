﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Models
{
    public class UpdateUserModel
    {
        public int Id { get; set; }
        public string ProjectName { get; private set; }
    }
}
