using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;
namespace backend.Entities
{
    public class Job :BaseEntity
    {
        public string Title {get; set;}
        public JobLevel Level{get; set;}

        //Relations

        public long CompanyId {get; set;}
        public Company Company {get; set;}

        public ICollection<Candidate> Candidates {get; set;}
        
    }
}