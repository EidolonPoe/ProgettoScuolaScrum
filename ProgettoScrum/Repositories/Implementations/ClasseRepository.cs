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
            //using var connection = new SqlConnection(ConnectionString);
            //connection.Open();

            //string query = "INSERT INTO Classi (Anno, Sezione) VALUES (@Anno, @Sezione)";
            //using var command = new SqlCommand(query, connection);

            //command.Parameters.AddWithValue("@Anno", classe.Anno);
            //command.Parameters.AddWithValue("@Sezione", classe.Sezione);

            //command.ExecuteNonQuery();

            if (classe == null)
                throw new ArgumentNullException(nameof(classe), "La classe non può essere nulla.");

            if (ExistsByAnnoSezione(classe.Anno, classe.Sezione))
                throw new InvalidOperationException("Classe già presente con stesso Anno e Sezione.");

            if (classe == null)
                throw new ArgumentNullException(nameof(classe), "La classe non può essere nulla.");

            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                string query = "INSERT INTO Classi (Anno, Sezione) VALUES (@Anno, @Sezione)";
                using var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Anno", classe.Anno);
                command.Parameters.AddWithValue("@Sezione", classe.Sezione ?? throw new ArgumentException("La sezione non può essere nulla."));

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    throw new Exception("Inserimento non riuscito.");
            }
            catch (SqlException ex)
            {
                throw new Exception("Errore durante l'inserimento della classe nel database.", ex);
            }
        }


        private bool ExistsByAnnoSezione(int anno, string sezione)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "SELECT COUNT(1) FROM Classi WHERE Anno = @Anno AND Sezione = @Sezione";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Anno", anno);
            cmd.Parameters.AddWithValue("@Sezione", sezione);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public List<Classe> GetAll()
        {
            //var classi = new List<Classe>();
            //using var connection = new SqlConnection(ConnectionString);
            //connection.Open();
            //string query = "SELECT IdClasse, Anno, Sezione FROM Classi";
            //using var command = new SqlCommand(query, connection);
            //using var reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    classi.Add(new Classe
            //    {
            //        IdClasse = reader.GetInt32(0),
            //        Anno = reader.GetInt32(1),
            //        Sezione = reader.GetString(2)
            //    });
            //}
            //return classi;


            var classi = new List<Classe>();
            try
            {
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
            }
            catch (SqlException ex)
            {
             
                throw new Exception("Errore durante il recupero delle classi dal database.", ex);
            }
            return classi;
        }

        public void Remove(int id)
        {
            //using var connection = new SqlConnection(ConnectionString);
            //connection.Open();
            //string query = "DELETE FROM Classi WHERE IdClasse = @IdClasse";
            //using var command = new SqlCommand(query, connection);
            //command.Parameters.AddWithValue("@IdClasse", id);
            //command.ExecuteNonQuery();
            if (id <= 0)
                throw new ArgumentException("IdClasse non valido.", nameof(id));

            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                string query = "DELETE FROM Classi WHERE IdClasse = @IdClasse";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdClasse", id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                    throw new Exception($"Rimozione non riuscita. Classe con Id {id} non trovata.");
            }
            catch (SqlException ex)
            {
                throw new Exception($"Errore durante la rimozione della classe con ID {id}.", ex);
            }
        }

        public void Modify(Classe classe)
        {
            //using var connection = new SqlConnection(ConnectionString);
            //connection.Open();
            //string query = "UPDATE Classi SET Anno = @Anno, Sezione = @Sezione WHERE IdClasse = @IdClasse";
            //using var command = new SqlCommand(query, connection);
            //command.Parameters.AddWithValue("@Anno", classe.Anno);
            //command.Parameters.AddWithValue("@Sezione", classe.Sezione);
            //command.Parameters.AddWithValue("@IdClasse", classe.IdClasse);
            //command.ExecuteNonQuery();

            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                string query = "UPDATE Classi SET Anno = @Anno, Sezione = @Sezione WHERE IdClasse = @IdClasse";
                using var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Anno", classe.Anno);
                command.Parameters.AddWithValue("@Sezione", classe.Sezione);
                command.Parameters.AddWithValue("@IdClasse", classe.IdClasse);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                    throw new Exception($"Modifica non riuscita. Classe con Id {classe.IdClasse} non trovata.");
            }
            catch (SqlException ex)
            {
                throw new Exception($"Errore durante la modifica della classe con ID {classe.IdClasse}.", ex);
            }
        }

        public Classe? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("IdClasse non valido.", nameof(id));

            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                string query = "SELECT IdClasse, Anno, Sezione FROM Classi WHERE IdClasse = @IdClasse";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdClasse", id);

                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Classe
                    {
                        IdClasse = reader.GetInt32(0),
                        Anno = reader.GetInt32(1),
                        Sezione = reader.GetString(2)
                    };
                }
                else
                {
                    
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Errore durante il recupero della classe con ID {id}.", ex);
            }
        }

        public bool ExistsByNome(string nomeClasse)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "SELECT COUNT(1) FROM Classi WHERE Nome = @Nome";

            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Nome", nomeClasse);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

    }
}
