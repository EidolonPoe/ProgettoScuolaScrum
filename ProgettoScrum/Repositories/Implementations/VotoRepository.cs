using Microsoft.Data.SqlClient;
using ProgettoScrum.Repositories.Interfaces;

namespace ProgettoScrum.Repositories.Implementations
{
    public class VotoRepository : IVotoRepository
    {
        public readonly string ConnectionString;
        public VotoRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Add(Voto voto)
        {
            //using var connection = new SqlConnection(ConnectionString);
            //connection.Open();
            //string query = "INSERT INTO Voti (Id, Valore, StudenteId, MateriaId, Data) VALUES (@Id, @Valore, @StudenteId, @MateriaId, @Data)";
            //using var command = new SqlCommand(query, connection);
            //command.Parameters.AddWithValue("@Id", voto.Id);
            //command.Parameters.AddWithValue("@Valore", voto.Valore);
            //command.Parameters.AddWithValue("@StudenteId", voto.StudenteId);
            //command.Parameters.AddWithValue("@MateriaId", voto.MateriaId);
            //command.Parameters.AddWithValue("@Data", voto.Data);
            //command.ExecuteNonQuery();

            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                string query = "INSERT INTO Voti (Valore, StudenteId, MateriaId, Data) VALUES (@Valore, @StudenteId, @MateriaId, @Data)";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Valore", voto.Valore);
                command.Parameters.AddWithValue("@StudenteId", voto.StudenteId);
                command.Parameters.AddWithValue("@MateriaId", voto.MateriaId);
                command.Parameters.AddWithValue("@Data", voto.Data);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

                throw new Exception("Errore durante l'inserimento del voto.", ex);
            }
        }

        public List<Voto> GetAll()
        {
            //var voti = new List<Voto>();
            //using var connection = new SqlConnection(ConnectionString);
            //connection.Open();
            //string query = "SELECT Id, Valore, StudenteId, MateriaId, Data FROM Voti";
            //using var command = new SqlCommand(query, connection);
            //using var reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    voti.Add(new Voto
            //    {
            //        Id = reader.GetInt32(0),
            //        Valore = reader.GetFloat(1),
            //        StudenteId = reader.GetInt32(2),
            //        MateriaId = reader.GetInt32(3),
            //        Data = reader.GetDateTime(4)
            //    });
            //}
            //return voti;

            var voti = new List<Voto>();
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                string query = "SELECT Id, Valore, StudenteId, MateriaId, Data FROM Voti";
                using var command = new SqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    voti.Add(new Voto
                    {
                        Id = reader.GetInt32(0),
                        Valore = reader.GetFloat(1),
                        StudenteId = reader.GetInt32(2),
                        MateriaId = reader.GetInt32(3),
                        Data = reader.GetDateTime(4)
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Errore durante il recupero dei voti.", ex);
            }
            return voti;
        }

        public void Remove(int id)
        {
            //using var connection = new SqlConnection(ConnectionString);
            //connection.Open();
            //string query = "DELETE FROM Voti WHERE Valore = @Valore";
            //using var command = new SqlCommand(query, connection);
            //command.Parameters.AddWithValue("@Valore", valore);
            //command.ExecuteNonQuery();

            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                string query = "DELETE FROM Voti WHERE Id = @Id";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Errore durante la cancellazione del voto.", ex);
            }
        }

        public void Modify(Voto voto)
        {
            //using var connection = new SqlConnection(ConnectionString);
            //connection.Open();
            //string query = "UPDATE Voti SET Valore = @Valore, StudenteId = @StudenteId, MateriaId = @MateriaId, Data = @Data WHERE Id = @Id";
            //using var command = new SqlCommand(query, connection);
            //command.Parameters.AddWithValue("@Id", voto.Id);
            //command.Parameters.AddWithValue("@Valore", voto.Valore);
            //command.Parameters.AddWithValue("@StudenteId", voto.StudenteId);
            //command.Parameters.AddWithValue("@MateriaId", voto.MateriaId);
            //command.Parameters.AddWithValue("@Data", voto.Data);
            //command.ExecuteNonQuery();


            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                string query = "UPDATE Voti SET Valore = @Valore, StudenteId = @StudenteId, MateriaId = @MateriaId, Data = @Data WHERE Id = @Id";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", voto.Id);
                command.Parameters.AddWithValue("@Valore", voto.Valore);
                command.Parameters.AddWithValue("@StudenteId", voto.StudenteId);
                command.Parameters.AddWithValue("@MateriaId", voto.MateriaId);
                command.Parameters.AddWithValue("@Data", voto.Data);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Errore durante la modifica del voto.", ex);
            }
        }

        public Voto? GetById(int id)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                string query = @"SELECT Id, Valore, StudenteId, MateriaId, Data 
                         FROM Voti 
                         WHERE Id = @Id";

                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Voto
                    {
                        Id = reader.GetInt32(0),
                        Valore = (float)reader.GetDouble(1), 
                        StudenteId = reader.GetInt32(2),
                        MateriaId = reader.GetInt32(3),
                        Data = reader.GetDateTime(4)
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Errore durante il recupero del voto con ID {id}.", ex);
            }



        }
    }
}
