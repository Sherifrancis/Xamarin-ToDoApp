﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Models
{
    public class MonthModel: BaseModel
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }
}
