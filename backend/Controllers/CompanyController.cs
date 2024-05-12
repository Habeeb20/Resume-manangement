using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using backend.DataContext;
using backend.Dtos.Company;
using backend.Entities;
using System.Collections;
namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private ApplicationDbContext _context { get; } = context;

        private IMapper _mapper { get; } = mapper;

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateDto dto)
        {
            Company newCompany = _mapper.Map<Company>(dto);
            await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();

            return Ok("successfully created");
        }

        [HttpGet]
        [Route("Get")]

        public async Task<ActionResult<IEnumerable<CompanyGetDto>>> GetCompany()
        {
            var company = await _context.Companies.OrderByDescending(q => q.CreatedAt).ToListAsync();
            var convertedCompany = _mapper.Map<IEnumerable<CompanyGetDto>>(company);

            return Ok(convertedCompany);
        }
    }
    
}