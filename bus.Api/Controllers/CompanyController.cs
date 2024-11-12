using bus.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bus.Api.Controllers
{
    [ApiController]
    [Route("/api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CompanyController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _dataContext.Companies.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var company = await _dataContext.Companies.FirstOrDefaultAsync(x => x.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Company company)
        {
            _dataContext.Companies.Add(company);
            await _dataContext.SaveChangesAsync();
            return Ok(company);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Company company)
        {
            _dataContext.Companies.Update(company);
            await _dataContext.SaveChangesAsync();
            return Ok(company);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await _dataContext.Companies.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
