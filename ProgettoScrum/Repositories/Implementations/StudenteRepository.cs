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
        //using var connection = new SqlConnection(ConnectionString);
        //connection.Open();
        //string query = "INSERT INTO Studenti (Nome, Cognome, DataNascita, IdClasse) VALUES (@Nome, @Cognome, @DataNascita, @IdClasse)";
        //using var command = new SqlCommand(query, connection);
        //command.Parameters.AddWithValue("@Nome", studente.Nome);
        //command.Parameters.AddWithValue("@Cognome", studente.Cognome);
        //command.Parameters.AddWithValue("@DataNascita", studente.DataNascita);
        //command.Parameters.AddWithValue("@IdClasse", studente.IdClasse);
        //command.ExecuteNonQuery();
        try
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
        catch (SqlException ex)
        {
            throw new Exception("Errore durante l'aggiunta dello studente.", ex);
        }
    }

    public List<Studente> GetAll()
    {
        //var studenti = new List<Studente>();
        //using var connection = new SqlConnection(ConnectionString);
        //connection.Open();
        //string query = "SELECT Id, Nome, Cognome, DataNascita, IdClasse FROM Studenti";
        //using var command = new SqlCommand(query, connection);
        //using var reader = command.ExecuteReader();
        //while (reader.Read())
        //{
        //    studenti.Add(new Studente
        //    {
        //        Id = reader.GetInt32(0),
        //        Nome = reader.GetString(1),
        //        Cognome = reader.GetString(2),
        //        DataNascita = reader.GetDateTime(3),
        //        IdClasse = reader.GetInt32(4)
        //    });
        //}
        //return studenti;

        var studenti = new List<Studente>();

        try
        {
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
        }
        catch (SqlException ex)
        {
            throw new Exception("Errore durante il recupero degli studenti.", ex);
        }

        if (studenti.Count == 0)
            Console.WriteLine("Nessuno studente trovato.");

        return studenti;
    }

    public void Remove(int id)
    {
        //using var connection = new SqlConnection(ConnectionString);
        //connection.Open();
        //string query = "DELETE FROM Studenti WHERE Id = @Id";
        //using var command = new SqlCommand(query, connection);
        //command.Parameters.AddWithValue("@Id", id);
        //command.ExecuteNonQuery();


        try
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "DELETE FROM Studenti WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                Console.WriteLine($"Nessuno studente con Id {id} trovato da eliminare.");
        }
        catch (SqlException ex)
        {
            throw new Exception($"Errore durante la rimozione dello studente con Id {id}.", ex);
        }
    }

    public void Modify(Studente studente)
    {
        //using var connection = new SqlConnection(ConnectionString);
        //connection.Open();
        //string query = "UPDATE Studenti SET Nome = @Nome, Cognome = @Cognome, DataNascita = @DataNascita, IdClasse = @IdClasse WHERE Id = @Id";
        //using var command = new SqlCommand(query, connection);
        //command.Parameters.AddWithValue("@Nome", studente.Nome);
        //command.Parameters.AddWithValue("@Cognome", studente.Cognome);
        //command.Parameters.AddWithValue("@DataNascita", studente.DataNascita);
        //command.Parameters.AddWithValue("@IdClasse", studente.IdClasse);
        //command.Parameters.AddWithValue("@Id", studente.Id);
        //command.ExecuteNonQuery();

        try
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

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                Console.WriteLine($"Modifica non riuscita. Studente con Id {studente.Id} non trovato.");
        }
        catch (SqlException ex)
        {
            throw new Exception($"Errore durante la modifica dello studente con Id {studente.Id}.", ex);
        }
    }

    public Studente? GetById(int id)
    {
        try
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "SELECT Id, Nome, Cognome, DataNascita, IdClasse FROM Studenti WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Studente
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Cognome = reader.GetString(2),
                    DataNascita = reader.GetDateTime(3),
                    IdClasse = reader.GetInt32(4)
                };
            }
            else
            {
                Console.WriteLine($"Studente con Id {id} non trovato.");
                return null;
            }
        }
        catch (SqlException ex)
        {
            throw new Exception($"Errore durante il recupero dello studente con Id {id}.", ex);
        }
    }
}
