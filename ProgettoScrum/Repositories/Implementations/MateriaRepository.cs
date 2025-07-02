using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgettoScrum.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace ProgettoScrum.Repositories.Implementations;

public class MateriaRepository
{
    public readonly string ConnectionString;
    public MateriaRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }
    public void Add(Materia materia)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        string query = "INSERT INTO Materie (IdMateria, Nome) VALUES (@IdMateria, @Nome)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@IdMateria", materia.IdMateria);
        command.Parameters.AddWithValue("@Nome", materia.Nome);
        command.ExecuteNonQuery();
    }

    public List<Materia> GetAll()
    {
        var materie = new List<Materia>();
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        string query = "SELECT IdMateria, Nome FROM Materie";
        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            materie.Add(new Materia
            {
                IdMateria = reader.GetInt32(0),
                Nome = reader.GetString(1)
            });
        }
        return materie;
    }

    public void Remove(int idMateria)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        string query = "DELETE FROM Materie WHERE IdMateria = @IdMateria";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@IdMateria", idMateria);
        command.ExecuteNonQuery();
    }

    public void Modify(Materia materia)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        string query = "UPDATE Materie SET Nome = @Nome WHERE IdMateria = @IdMateria";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@IdMateria", materia.IdMateria);
        command.Parameters.AddWithValue("@Nome", materia.Nome);
        command.ExecuteNonQuery();
    }
}

