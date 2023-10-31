using AutoMapper;
using Learning.API.Models.DTO;
using Learning.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Learning.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulty()
        {
            var walkDifficultyDomain = await walkDifficultyRepository.GetAllAsync();
            var walkDifficultyDto = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficultyDomain);
            return Ok(walkDifficultyDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficulty == null)
            {
                return NotFound();
            }
            //convert Domain to DTo
            var walkDifficultyDto = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //validate
            //if (!ValidateAddWalkDifficultyAsync(addWalkDifficultyRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            //convert dto to domain model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code,
            };

            //call repository
            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            //convert domain to dto
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id,
            Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            //validate
            //if (!ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            //convert dto to domain model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };
            //call repository
            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }
            //convert domain to dto
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            //Return 
            return Ok(walkDifficultyDTO);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }
            //convert domain to dto
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            //Return 
            return Ok(walkDifficultyDTO);
        }

        #region Private Method
        private bool ValidateAddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest),
                   $"{nameof(addWalkDifficultyRequest)} cannot be empty.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code),
                  $"{nameof(addWalkDifficultyRequest.Code)} is required.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        private bool ValidateUpdateWalkDifficultyAsync(Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest),
                   $"{nameof(updateWalkDifficultyRequest)} cannot be empty.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code),
                  $"{nameof(updateWalkDifficultyRequest.Code)} is required.");
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
