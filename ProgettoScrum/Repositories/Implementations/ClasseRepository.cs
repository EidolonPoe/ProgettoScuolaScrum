using ProgettoScrum.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;

namespace ProgettoScrum.Repositories.Implementations
{
    public class ClasseRepository : IClasseRepository
    {
        private readonly string ConnectionString;

        public ClasseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Add(Classe classe)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "INSERT INTO Classi (Anno, Sezione) VALUES (@Anno, @Sezione)";
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Anno", classe.Anno);
            command.Parameters.AddWithValue("@Sezione", classe.Sezione);

            command.ExecuteNonQuery();
        }

        public List<Classe> GetAll()
        {
            var classi = new List<Classe>();
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            string query = "SELECT IdClasse, Anno, Sezione FROM Classi";
            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                classi.Add(new Classe
                {
                    IdClasse = reader.GetInt32(0),
                    Anno = reader.GetInt32(1),
                    Sezione = reader.GetString(2)
                });
            }
            return classi;
        }

        public void Remove(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            string query = "DELETE FROM Classi WHERE IdClasse = @IdClasse";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IdClasse", id);
            command.ExecuteNonQuery();
        }

        public void Modify(Classe classe)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            string query = "UPDATE Classi SET Anno = @Anno, Sezione = @Sezione WHERE IdClasse = @IdClasse";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Anno", classe.Anno);
            command.Parameters.AddWithValue("@Sezione", classe.Sezione);
            command.Parameters.AddWithValue("@IdClasse", classe.IdClasse);
            command.ExecuteNonQuery();
        }
    }
}
