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

        public void Add(List<Studente> studenti)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                foreach (var studente in studenti)
                {
                    string query = @"
                INSERT INTO Studenti (Nome, Cognome, IdClasse)
                VALUES (@Nome, @Cognome, @IdClasse)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", studente.Nome);
                        command.Parameters.AddWithValue("@Cognome", studente.Cognome);
                        command.Parameters.AddWithValue("@IdClasse", studente.IdClasse);

                        try
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine($"Inserito: {studente.Nome} {studente.Cognome}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Errore inserendo {studente.Nome} {studente.Cognome}: {ex.Message}");
                        }
                    }
                }
            }
        }

    }

    }
}
