using kolospoprawa.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace kolospoprawa.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalsRepository _animalsRepository;
        public AnimalsController(IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimal(int id)
        {
            if (!await _animalsRepository.DoesAnimalExist(id))
                return NotFound($"Animal with given ID - {id} doesn't exist");
 
            var animal = await _animalsRepository.GetAnimal(id);
            return Ok(animal);
        }
    }
}