using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Dtos.Candidate;
using Microsoft.AspNetCore.Http.HttpResults;
using backend.Entities;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private ApplicationDbContext _context {get;}

        private IMapper _mapper {get;}

        public CandidateController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> CreateCandidate([FromForm] CandidateCreateDtos dto, IFormFile pdfFile)
        {
            //First => save pdf to Server
            //Then => save url into our entity

            var fiveMegaByte = 5 * 1024 *1024;
            var pdfMimeType = "application/pdf";

            if(pdfFile.Length > fiveMegaByte || pdfFile.ContentType != pdfMimeType)
            {
                return BadRequest("file is not valid");
            }

            var resumeUrl = Guid.NewGuid().ToString() + ".pdf";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs", resumeUrl);
            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }
            var newCandidate = _mapper.Map<Candidate>(dto);
            newCandidate.ResumeUrl = resumeUrl;
            await _context.Candidates.AddAsync(newCandidate);
            await _context.SaveChangesAsync();

            return Ok("candidate details is saved successfully");
        }

        [HttpGet]
        [Route("Get")]

        public async Task<ActionResult<IEnumerable<CandidateGetDtos>>>GetCandidates()
        {
            var candidate = await _context.Candidates.Include(c => c.Job).OrderByDescending(q => q.CreatedAt).ToListAsync();
            var convertedCandidates = _mapper.Map<IEnumerable<CandidateGetDtos>>(candidate);

            return Ok(convertedCandidates);
        }

        [HttpGet]
        [Route("download/{url}")]

        public IActionResult DownloadPdfFile(string url)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs", url);
            if(!System.IO.File.Exists(filePath))
            {
                return NotFound("file not found");
            }

            var pdfBytes =System.IO.File.ReadAllBytes(filePath);
            var file = File(pdfBytes, "application/pdf", url);
            return file;
        }
    }
}