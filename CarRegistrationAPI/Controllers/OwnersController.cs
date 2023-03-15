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
    public class OwnersController : ControllerBase
    {
        IOwnerRepository _repository;
        public OwnersController(IOwnerRepository repository)
        {
            _repository= repository;
        }

        // GET: api/Owners/?startindex=0&pagesize=15
        [HttpGet]
        public async Task<ActionResult<VirtualizeResponse<Owner>>> GetOwners([FromQuery] QueryParameters queryParameters)
        {
            return await _repository.GetAllAsync<Owner>(queryParameters);
        }

        // POST: api/Owners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            await _repository.AddAsync(owner);

            return CreatedAtAction(nameof(GetOwner), new { id = owner.Id }, owner);
        }


        // PUT: api/Owners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutOwner(int id, Owner owner) 
        {
            await _repository.UpdateAsync(owner);
            return NoContent();
        }

        // GET: api/Owners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            var owner = await _repository.GetOwnerAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }

        // DELETE: api/Owners/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteOwner(int id)
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
