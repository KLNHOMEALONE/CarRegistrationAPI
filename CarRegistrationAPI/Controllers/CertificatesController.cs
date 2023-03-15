using CarRegistrationAPI.Entities;
using CarRegistrationAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CertificatesController : ControllerBase
    {
        ICertificateRepository _repository;
        public CertificatesController(ICertificateRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Certificates/?startindex=0&pagesize=15
        [HttpGet]
        public async Task<ActionResult<VirtualizeResponse<Certificate>>> GetCertificates([FromQuery] QueryParameters queryParameters)
        {
            return await _repository.GetAllAsync<Certificate>(queryParameters);
        }

        // POST: api/Certificates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Owner>> PostOwner(Certificate certificate)
        {
            await _repository.AddAsync(certificate);

            return CreatedAtAction(nameof(GetCertificate), new { id = certificate.Id }, certificate);
        }


        // PUT: api/Certificates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutCertificate(int id, Certificate certificate)
        {
            await _repository.UpdateAsync(certificate);
            return NoContent();
        }

        // GET: api/Certificates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Certificate>> GetCertificate(int id)
        {
            var owner = await _repository.GetCertificateAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }

        // DELETE: api/Certificate/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            var owner = await _repository.GetAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
