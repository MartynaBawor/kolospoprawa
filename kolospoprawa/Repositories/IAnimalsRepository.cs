using kolospoprawa.Models;
namespace kolospoprawa.Repositories;

public interface IAnimalsRepository
{
    Task<bool> DoesAnimalExist(int id);
    Task<AnimalDTO> GetAnimal(int id);
}