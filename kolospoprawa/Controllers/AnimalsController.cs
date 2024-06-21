using kolospoprawa.Models;
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
        [HttpPost]
        public async Task<IActionResult> AddAnimal(NewAnimalWithProcedures newAnimalWithProcedures)
        {
            if (!await _animalsRepository.DoesOwnerExist(newAnimalWithProcedures.OwnerId))
                return NotFound($"Owner with given ID - {newAnimalWithProcedures.OwnerId} doesn't exist");

            foreach (var procedure in newAnimalWithProcedures.Procedures)
            {
                if (!await _animalsRepository.DoesProcedureExist(procedure.ProcedureId))
                    return NotFound($"Procedure with given ID - {procedure.ProcedureId} doesn't exist");
            }

            await _animalsRepository.AddNewAnimalWithProcedures(newAnimalWithProcedures);

            return Created(Request.Path.Value ?? "api/animals", newAnimalWithProcedures);
        }
    }
}