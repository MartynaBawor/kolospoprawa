namespace kolospoprawa.Models;

public class AnimalDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AnimalClass { get; set; } = string.Empty;
    public DateTime AdmissionDate { get; set; }
    public OwnerDto Owner { get; set; } = null!;
}
public class OwnerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}