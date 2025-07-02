using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ProgettoScrum.Repositories.Interfaces;

namespace ProgettoScrum.Repositories.Implementations;

public class StudenteRepository : IStudenteRepository
{
    public readonly string ConnectionString;
    public StudenteRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }
    public void Add(Studente studente)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        string query = "INSERT INTO Studenti (Nome, Cognome, DataNascita, IdClasse) VALUES (@Nome, @Cognome, @DataNascita, @IdClasse)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Nome", studente.Nome);
        command.Parameters.AddWithValue("@Cognome", studente.Cognome);
        command.Parameters.AddWithValue("@DataNascita", studente.DataNascita);
        command.Parameters.AddWithValue("@IdClasse", studente.IdClasse);
        command.ExecuteNonQuery();
    }

    public List<Studente> GetAll()
    {
        var studenti = new List<Studente>();
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        string query = "SELECT Id, Nome, Cognome, DataNascita, IdClasse FROM Studenti";
        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            studenti.Add(new Studente
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Cognome = reader.GetString(2),
                DataNascita = reader.GetDateTime(3),
                IdClasse = reader.GetInt32(4)
            });
        }
        return studenti;
    }

    public void Remove(int id)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        string query = "DELETE FROM Studenti WHERE Id = @Id";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
    }

    public void Modify(Studente studente)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();
        string query = "UPDATE Studenti SET Nome = @Nome, Cognome = @Cognome, DataNascita = @DataNascita, IdClasse = @IdClasse WHERE Id = @Id";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Nome", studente.Nome);
        command.Parameters.AddWithValue("@Cognome", studente.Cognome);
        command.Parameters.AddWithValue("@DataNascita", studente.DataNascita);
        command.Parameters.AddWithValue("@IdClasse", studente.IdClasse);
        command.Parameters.AddWithValue("@Id", studente.Id);
        command.ExecuteNonQuery();
    }
}
