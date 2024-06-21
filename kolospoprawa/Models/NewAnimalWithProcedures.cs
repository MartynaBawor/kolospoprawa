﻿namespace kolospoprawa.Models;

public class NewAnimalWithProcedures
{
    public string Name { get; set; } = string.Empty;
    public string AnimalClass { get; set; } = string.Empty;
    public DateTime AdmissionDate { get; set; }
    public int OwnerId { get; set; }
    public IEnumerable<ProcedureWithDate> Procedures { get; set; } = new List<ProcedureWithDate>();
}

public class ProcedureWithDate
{
    public int ProcedureId { get; set; }
    public DateTime Date { get; set; }
}