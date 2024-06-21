namespace kolospoprawa.Models;

public class NewAnimalDTO
{
    
    public string Name { get; set; } = string.Empty;
    public string AnimalClass { get; set; } = string.Empty;
    public DateTime AdmissionDate { get; set; }
    public int OwnerId { get; set; }
}