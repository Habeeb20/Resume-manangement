using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Dtos.Job
{
    public class JobGetDto
    {
        public long ID{get; set;}

        public string Name{get; set;}

        public JobLevel Level{get; set;}

        public string CompanyName{get; set;}

        public long CompanyId{get; set;}

        public DateTime CreatedAt{get; set;} = DateTime.Now;
        
    }
}