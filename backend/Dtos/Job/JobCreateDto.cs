using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Dtos.Job
{
    public class JobCreateDto
    {
        public string Title{get; set;}
        public JobLevel Level{get; set;}

        public long CompanyId{get; set;}
        
    }
}