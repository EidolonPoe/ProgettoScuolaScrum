using ProgettoScrum.Repositories.Implementations;
using System.Net.WebSockets;
using ProgettoScrum.Repositories.Interfaces;

public class Program
{
    string ConnectionString;
    var repository = new ClasseRepository(ConnectionString);

    while(true)
    {
        Console.WriteLine("\n--- MENU ---");
        Console.WriteLine("1. Aggiungi una classe");
        Console.WriteLine("2. Visualizza tutte le classi");
        Console.WriteLine("3. Rimuovi una classe");
        Console.WriteLine("4. Modifica una classe");
        Console.WriteLine("5. Esci");

        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.WriteLine("Inserisci il nome della classe:");
                var nomeClasse = Console.ReadLine();
                var nuovaClasse = new Classe { Nome = nomeClasse };
                repository.Add(nuovaClasse);
                Console.WriteLine("Classe aggiunta con successo.");
                break;
            case "2":
                var classi = repository.GetAll();
                Console.WriteLine("Elenco delle classi:");
                foreach (var classe in classi)
                {
                    Console.WriteLine($"ID: {classe.Id}, Nome: {classe.Nome}");
                }
                break;
            case "3":
                Console.WriteLine("Inserisci l'ID della classe da rimuovere:");
                if (int.TryParse(Console.ReadLine(), out int idDaRimuovere))
                {
                    repository.Remove(idDaRimuovere);
                    Console.WriteLine("Classe rimossa con successo.");
                }
                else
                {
                    Console.WriteLine("ID non valido.");
                }
                break;
            case "4":
                Console.WriteLine("Inserisci l'ID della classe da modificare:");
                if (int.TryParse(Console.ReadLine(), out int idDaModificare))
                {
                    var classeDaModificare = repository.GetAll().FirstOrDefault(c => c.Id == idDaModificare);
                    if (classeDaModificare != null)
                    {
                        Console.WriteLine("Inserisci il nuovo nome della classe:");
                        classeDaModificare.Nome = Console.ReadLine();
                        repository.Modify(classeDaModificare);
                        Console.WriteLine("Classe modificata con successo.");
                    }
                    else
                    {
                        Console.WriteLine("Classe non trovata.");
                    }
                }
                else
                {
                    Console.WriteLine("ID non valido.");
                }
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Opzione non valida. Riprova.");
                break;
}

}