﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.User
{
    public class GetAllUser
    {
        public int TotalUserCount { get; set; }
        public object? Users { get; set; }
    }
}