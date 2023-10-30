using AutoMapper;
using Learning.API.Models.DTO;
using Learning.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Learning.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = await walkRepository.GetAllAsync();
            var WalksDto = mapper.Map<List<Models.DTO.walk>>(walks);
            return Ok(WalksDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walkDomain = await walkRepository.GetAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<Models.DTO.walk>(walkDomain);
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            //convert DTO to Domain object
            var walkDomain = new Models.Domain.walk
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };
            //Pass details to repository
            walkDomain = await walkRepository.AddAsync(walkDomain);

            //convert back to DTO
            var walkDto = new Models.DTO.walk
            {
                Name = walkDomain.Name,
                Length = walkDomain.Length,
                RegionId = walkDomain.RegionId,
                Id = walkDomain.Id,
                WalkDifficultyId = walkDomain.Id
            };
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDto.Id }, walkDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            //convert DTO to Domain object
            var walkDomain = new Models.Domain.walk
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            //Pass details to repository
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);
            //If null then NotFound
            if (walkDomain == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTo
            var walkDto = new Models.DTO.walk
            {
                Name = walkDomain.Name,
                Length = walkDomain.Length,
                RegionId = walkDomain.RegionId,
                Id = walkDomain.Id,
                WalkDifficultyId = walkDomain.Id
            };
            //Return Ok response
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //Get region from database
            var walk = await walkRepository.DeleteAsync(id);

            //If Null Notfound
            if (walk == null)
            {
                return NotFound();
            }
            //mapper
            var walkDto = mapper.Map<Models.DTO.walk>(walk);
            return Ok(walkDto);
        }
    }
}
