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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper,
            IRegionRepository regionRepository,
            IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
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
            //validate the incoming request 
            if (!(await ValidateAddWalkAsync(addWalkRequest)))
            {
                return BadRequest(ModelState);
            }
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
            //validate 
            if (!(await ValidateUpdateWalkAsync(updateWalkRequest)))
            {
                return BadRequest(ModelState);
            }
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

        #region Private Methods
        private async Task<bool> ValidateAddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            //if (addWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest),
            //       $"{nameof(addWalkRequest)} cannot be empty.");
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Name),
            //      $"{nameof(addWalkRequest.Name)} is required.");
            //}
            //if (addWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Length),
            //      $"{nameof(addWalkRequest.Length)} should be greater than zero.");
            //}
            var region = await regionRepository.GetAsync(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                  $"{nameof(addWalkRequest.RegionId)} is invalid.");
            }
            var walkDifficulty = await walkDifficultyRepository.GetAsync(addWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                  $"{nameof(addWalkRequest.WalkDifficultyId)} is invalid.");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync(Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //if (updateWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest),
            //       $"{nameof(updateWalkRequest)} cannot be empty.");
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Name),
            //      $"{nameof(updateWalkRequest.Name)} is required.");
            //}
            //if (updateWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Length),
            //      $"{nameof(updateWalkRequest.Length)} should be greater than zero.");
            //}
            var region = await regionRepository.GetAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId),
                  $"{nameof(updateWalkRequest.RegionId)} is invalid.");
            }
            var walkDifficulty = await walkDifficultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId),
                  $"{nameof(updateWalkRequest.WalkDifficultyId)} is invalid.");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
