﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }


    }
}
