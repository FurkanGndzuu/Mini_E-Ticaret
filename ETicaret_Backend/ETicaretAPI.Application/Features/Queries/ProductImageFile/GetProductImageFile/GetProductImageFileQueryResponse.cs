﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImageFile
{
    public class GetProductImageFileQueryResponse
    {
      
        public string? Path { get; set; }
        public string? FileName { get; set; }
        public bool? showCase { get; set; }
        public string? id { get; set; }
    }
}
