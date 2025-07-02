using ProgettoScrum.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

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
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            string query = "INSERT INTO Voti (Id, Valore, StudenteId, MateriaId, Data) VALUES (@Id, @Valore, @StudenteId, @MateriaId, @Data)";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", voto.Id);
            command.Parameters.AddWithValue("@Valore", voto.Valore);
            command.Parameters.AddWithValue("@StudenteId", voto.StudenteId);
            command.Parameters.AddWithValue("@MateriaId", voto.MateriaId);
            command.Parameters.AddWithValue("@Data", voto.Data);
            command.ExecuteNonQuery();
        }

        public List<Voto> GetAll()
        {
            var voti = new List<Voto>();
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
            return voti;
        }

        public void Remove(int valore)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            string query = "DELETE FROM Voti WHERE Valore = @Valore";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Valore", valore);
            command.ExecuteNonQuery();
        }

        public void Modify(Voto voto)
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
    }
}
