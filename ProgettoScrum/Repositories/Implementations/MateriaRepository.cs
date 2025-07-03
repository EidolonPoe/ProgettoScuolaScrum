using Microsoft.Data.SqlClient;
using ProgettoScrum.Repositories.Interfaces;

namespace ProgettoScrum.Repositories.Implementations;

public class MateriaRepository : IMateriaRepository
{
    public readonly string ConnectionString;
    public MateriaRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }
    //public void Add(Materia materia)
    //{
    //    //using var connection = new SqlConnection(ConnectionString);
    //    //connection.Open();
    //    //string query = "INSERT INTO Materie (IdMateria, Nome) VALUES (@IdMateria, @Nome)";
    //    //using var command = new SqlCommand(query, connection);
    //    //command.Parameters.AddWithValue("@IdMateria", materia.IdMateria);
    //    //command.Parameters.AddWithValue("@Nome", materia.Nome);
    //    //command.ExecuteNonQuery();
    //    try
    //    {
    //        using var connection = new SqlConnection(ConnectionString);
    //        connection.Open();

    //        string query = "INSERT INTO Materie (Nome) VALUES (@Nome)";
    //        using var command = new SqlCommand(query, connection);

    //        command.Parameters.AddWithValue("@Nome", materia.Nome);

    //        int rowsAffected = command.ExecuteNonQuery();
    //        if (rowsAffected == 0)
    //            throw new Exception("Inserimento non riuscito.");
    //    }
    //    catch (SqlException ex)
    //    {
    //        throw new Exception("Errore durante l'inserimento della materia nel database.", ex);
    //    }
    //}

    public int Add(Materia materia)
    {

        try
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            
            string queryCheck = "SELECT IdMateria FROM Materie WHERE Nome = @Nome";
            using var cmdCheck = new SqlCommand(queryCheck, connection);
            cmdCheck.Parameters.AddWithValue("@Nome", materia.Nome);
            var existingId = cmdCheck.ExecuteScalar();

            if (existingId != null)
            {
                
                return Convert.ToInt32(existingId);
            }
            else
            {
                
                string queryInsert = "INSERT INTO Materie (Nome) VALUES (@Nome); SELECT CAST(SCOPE_IDENTITY() AS int)";
                using var insertCmd = new SqlCommand(queryInsert, connection);
                insertCmd.Parameters.AddWithValue("@Nome", materia.Nome);
                var newId = insertCmd.ExecuteScalar();
                return Convert.ToInt32(newId);
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("Errore durante l'inserimento della materia.", ex);
        }
    }

    public List<Materia> GetAll()
    {
        //var materie = new List<Materia>();
        //using var connection = new SqlConnection(ConnectionString);
        //connection.Open();
        //string query = "SELECT IdMateria, Nome FROM Materie";
        //using var command = new SqlCommand(query, connection);
        //using var reader = command.ExecuteReader();
        //while (reader.Read())
        //{
        //    materie.Add(new Materia
        //    {
        //        IdMateria = reader.GetInt32(0),
        //        Nome = reader.GetString(1)
        //    });
        //}
        //return materie;
        var materie = new List<Materia>();
        try
        {
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
        }
        catch (SqlException ex)
        {
            throw new Exception("Errore durante il recupero delle materie dal database.", ex);
        }

        return materie;
    }

    public void Remove(int idMateria)
    {
        //using var connection = new SqlConnection(ConnectionString);
        //connection.Open();
        //string query = "DELETE FROM Materie WHERE IdMateria = @IdMateria";
        //using var command = new SqlCommand(query, connection);
        //command.Parameters.AddWithValue("@IdMateria", idMateria);
        //command.ExecuteNonQuery();
        try
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "DELETE FROM Materie WHERE IdMateria = @IdMateria";
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@IdMateria", idMateria);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                throw new Exception($"Rimozione non riuscita. Materia con Id {idMateria} non trovata.");
        }
        catch (SqlException ex)
        {
            throw new Exception($"Errore durante la rimozione della materia con ID {idMateria}.", ex);
        }
    }

    public void Modify(Materia materia)
    {
        //using var connection = new SqlConnection(ConnectionString);
        //connection.Open();
        //string query = "UPDATE Materie SET Nome = @Nome WHERE IdMateria = @IdMateria";
        //using var command = new SqlCommand(query, connection);
        //command.Parameters.AddWithValue("@IdMateria", materia.IdMateria);
        //command.Parameters.AddWithValue("@Nome", materia.Nome);
        //command.ExecuteNonQuery();
        try
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "UPDATE Materie SET Nome = @Nome WHERE IdMateria = @IdMateria";
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@IdMateria", materia.IdMateria);
            command.Parameters.AddWithValue("@Nome", materia.Nome);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                throw new Exception($"Modifica non riuscita. Materia con Id {materia.IdMateria} non trovata.");
        }
        catch (SqlException ex)
        {
            throw new Exception($"Errore durante la modifica della materia con ID {materia.IdMateria}.", ex);
        }
    }
}

