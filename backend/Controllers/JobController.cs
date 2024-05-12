using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.DataContext;
using backend.Dtos.Job;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class JobController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private ApplicationDbContext _context {get;} = context;

        private IMapper _mapper {get;} = mapper;

        public async Task<IActionResult> CreateJob(JobCreateDto dto)
        {
            var newJob = _mapper.Map<Job>(dto);
            await _context.Jobs.AddAsync(newJob);
            await _context.SaveChangesAsync();

            return Ok("successfully created");
        }

        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetJob()
        {
            var newJob = await _context.Jobs.Include(job => job.Company).OrderByDescending(q => q.CreatedAt).ToListAsync();
            var convertedJob = _mapper.Map<IEnumerable<JobGetDto>>(newJob);

            return Ok(convertedJob);


        }

    }
}