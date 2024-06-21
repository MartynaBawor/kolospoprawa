using kolospoprawa.Models;
namespace kolospoprawa.Repositories;

public interface IAnimalsRepository
{
    Task<bool> DoesAnimalExist(int id);
    Task<bool> DoesOwnerExist(int id);
    Task<bool> DoesProcedureExist(int id);
    Task<AnimalDTO> GetAnimal(int id);
    Task AddNewAnimalWithProcedures(NewAnimalWithProcedures newAnimalWithProcedures);
}