using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.Candidate
{
    public class CandidateCreateDtos
    {
        public string FirstName {get; set;}

        public string LastName {get; set;}
        public string Email {get; set;}

        public string Phone {get; set;}
        public string CoverLetter {get; set;}

        public long JobId {get; set;}
        
    }
}