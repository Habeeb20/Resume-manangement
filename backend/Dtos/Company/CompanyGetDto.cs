using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Dtos.Company
{
    public class CompanyGetDto
    {
        public long ID{get; set;}
        public string Name{get; set;}

        public CompanySize size{get; set;}

        public DateTime CreatedAt{get; set;} = DateTime.Now;
        
    }
}