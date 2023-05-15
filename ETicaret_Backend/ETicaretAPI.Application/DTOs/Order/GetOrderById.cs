﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Order
{
    public class GetOrderById
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Adress { get; set; }
        public string? Description { get; set; }
        public object BasketItems { get; set; }
        public bool Completed { get; set; }
    }
}
