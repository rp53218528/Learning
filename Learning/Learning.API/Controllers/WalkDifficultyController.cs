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

    }
}
