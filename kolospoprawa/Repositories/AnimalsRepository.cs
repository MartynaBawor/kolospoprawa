﻿using kolospoprawa.Models;
using Microsoft.Data.SqlClient;

namespace kolospoprawa.Repositories;

public class AnimalsRepository : IAnimalsRepository
{
	private readonly IConfiguration _configuration;
	public AnimalsRepository(IConfiguration configuration)
	{
		_configuration = configuration;
	}
 
	public async Task<bool> DoesAnimalExist(int id)
	{
		var query = "SELECT 1 FROM Animal WHERE ID = @ID";
 
		await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
		await using SqlCommand command = new SqlCommand();
 
		command.Connection = connection;
		command.CommandText = query;
		command.Parameters.AddWithValue("@ID", id);
 
		await connection.OpenAsync();
 
		var res = await command.ExecuteScalarAsync();
 
		return res is not null;
	}
    
    public async Task<AnimalDTO> GetAnimal(int id)
    {
	    var query = @"SELECT 
							Animal.ID AS AnimalID,
							Animal.Name AS AnimalName,
							AdmissionDate,
							AnimalClass,
							Owner.ID as OwnerID,
							FirstName,
							LastName,
						FROM Animal
						JOIN Owner ON Owner.ID = Animal.OwnerID
						JOIN Animal_Class ON Animal_Class.ID = Animal.AnimalClassID
						WHERE Animal.ID = @ID";
	    
	    await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
	    await using SqlCommand command = new SqlCommand();

	    command.Connection = connection;
	    command.CommandText = query;
	    command.Parameters.AddWithValue("@ID", id);
	    
	    await connection.OpenAsync();

	    var reader = await command.ExecuteReaderAsync();

	    var animalIdOrdinal = reader.GetOrdinal("AnimalID");
	    var animalNameOrdinal = reader.GetOrdinal("AnimalName");
	    var animalclassOrdinal = reader.GetOrdinal("AnimalClass");
	    var admissionDateOrdinal = reader.GetOrdinal("AdmissionDate");
	    var ownerIdOrdinal = reader.GetOrdinal("OwnerID");
	    var firstNameOrdinal = reader.GetOrdinal("FirstName");
	    var lastNameOrdinal = reader.GetOrdinal("LastName");

	    AnimalDTO animalDto = null;

	    while (await reader.ReadAsync())
	    {
		    animalDto = new AnimalDTO()
		    {
			    Id = reader.GetInt32(animalIdOrdinal),
			    Name = reader.GetString(animalNameOrdinal),
			    AdmissionDate = reader.GetDateTime(admissionDateOrdinal),
			    Owner = new OwnerDto()
			    {
				    Id = reader.GetInt32(ownerIdOrdinal),
				    FirstName = reader.GetString(firstNameOrdinal),
				    LastName = reader.GetString(lastNameOrdinal),
			    }
		    };
	    }

	    if (animalDto is null) throw new Exception();
        
        return animalDto;
    }
}