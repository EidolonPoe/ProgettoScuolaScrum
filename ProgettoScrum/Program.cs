//using ProgettoScrum.Repositories.Implementations;
//using System.Net.WebSockets;
//using ProgettoScrum.Repositories.Interfaces;

using ProgettoScrum;
using ProgettoScrum.Repositories.Implementations;
using ProgettoScrum.Repositories.Interfaces;

public class Program
{
    static void Main(string[] args)
    {
        string connectionString = "your_connection_string_here";
        var classeRepository = new ClasseRepository(connectionString);
        var studenteRepository = new StudenteRepository(connectionString);
        var materiaRepository = new MateriaRepository(connectionString);
        var votoRepository = new VotoRepository(connectionString);

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1 Gestisci Classe");
            Console.WriteLine("2. Gestisci Studenti");
            Console.WriteLine("3. Gestisci Materie");
            Console.WriteLine("4. Gestisci Voti");
            Console.WriteLine("5. Esci");
            Console.WriteLine("Inserisci un'opzione:");

            var input = Console.ReadLine();
            switch (input)
            {

                case "1":
                    GestisciClassi(classeRepository);
                    break;
                case "2":
                    GestisciStudenti(studenteRepository);
                    break;
                case "3":
                    GestisciMaterie(materiaRepository);
                    break;
                case "4":
                    GestisciVoti(votoRepository);
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Opzione non valida. Riprova.");
                    break;
            }
        }
        Console.WriteLine("Arrivederci!");
    }

    // Esempio di funzione per la gestione delle classi
    static void GestisciClassi(ClasseRepository repository)
    {
        while (true)
        {
            Console.WriteLine("\n--- Gestione Classe ---");
            Console.WriteLine("1. Aggiungi una classe");
            Console.WriteLine("2. Visualizza tutte le classi");
            Console.WriteLine("3. Rimuovi una classe");
            Console.WriteLine("4. Modifica una classe");
            Console.WriteLine("5. Torna al menu principale");
            Console.WriteLine("Inserisci un'opzione:");

            var classeInput = Console.ReadLine();
            switch (classeInput)
            {
                case "1":
                    Console.WriteLine("Inserisci l'anno della classe:");
                    int anno = int.Parse(Console.ReadLine());
                    Console.WriteLine("Inserisci la sezione della classe:");
                    string sezione = Console.ReadLine();
                    var nuovaClasse = new Classe { Anno = anno, Sezione = sezione, Studenti = new List<Studente>() };
                    repository.Add(nuovaClasse);
                    Console.WriteLine("Classe aggiunta con successo.");
                    break;
                case "2":
                    var classi = repository.GetAll();
                    Console.WriteLine("Elenco delle classi:");
                    foreach (var classe in classi)
                    {
                        Console.WriteLine($"ID: {classe.IdClasse}, Anno: {classe.Anno}, Sezione: {classe.Sezione}");
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
                        var classeDaModificare = repository.GetAll().FirstOrDefault(c => c.IdClasse == idDaModificare);
                        if (classeDaModificare != null)
                        {
                            Console.WriteLine("Inserisci il nuovo anno:");
                            classeDaModificare.Anno = int.Parse(Console.ReadLine());
                            Console.WriteLine("Inserisci la nuova sezione:");
                            classeDaModificare.Sezione = Console.ReadLine();
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
    }

    static void GestisciStudenti(StudenteRepository repository)
    {
        while (true)
        {
            Console.WriteLine("\n--- Gestione Studenti ---");
            Console.WriteLine("1. Aggiungi uno studente");
            Console.WriteLine("2. Visualizza tutti gli studenti");
            Console.WriteLine("3. Rimuovi uno studente");
            Console.WriteLine("4. Modifica uno studente");
            Console.WriteLine("5. Torna al menu principale");
            Console.WriteLine("Inserisci un'opzione:");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("Nome:");
                    string nome = Console.ReadLine();
                    Console.WriteLine("Cognome:");
                    string cognome = Console.ReadLine();
                    Console.WriteLine("Data di nascita (yyyy-MM-dd):");
                    DateTime dataNascita = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("IdClasse:");
                    int idClasse = int.Parse(Console.ReadLine());
                    var nuovoStudente = new Studente { Nome = nome, Cognome = cognome, DataNascita = dataNascita, IdClasse = idClasse, Voti = new List<Voto>() };
                    repository.Add(nuovoStudente);
                    Console.WriteLine("Studente aggiunto con successo.");
                    break;
                case "2":
                    var studenti = repository.GetAll();
                    Console.WriteLine("Elenco degli studenti:");
                    foreach (var studente in studenti)
                    {
                        Console.WriteLine($"ID: {studente.Id}, Nome: {studente.Nome}, Cognome: {studente.Cognome}, DataNascita: {studente.DataNascita:yyyy-MM-dd}, IdClasse: {studente.IdClasse}");
                    }
                    break;
                case "3":
                    Console.WriteLine("Inserisci l'ID dello studente da rimuovere:");
                    if (int.TryParse(Console.ReadLine(), out int idDaRimuovere))
                    {
                        repository.Remove(idDaRimuovere);
                        Console.WriteLine("Studente rimosso con successo.");
                    }
                    else
                    {
                        Console.WriteLine("ID non valido.");
                    }
                    break;
                case "4":
                    Console.WriteLine("Inserisci l'ID dello studente da modificare:");
                    if (int.TryParse(Console.ReadLine(), out int idDaModificare))
                    {
                        var studenteDaModificare = repository.GetById(idDaModificare);
                        if (studenteDaModificare != null)
                        {
                            Console.WriteLine("Nuovo nome:");
                            studenteDaModificare.Nome = Console.ReadLine();
                            Console.WriteLine("Nuovo cognome:");
                            studenteDaModificare.Cognome = Console.ReadLine();
                            Console.WriteLine("Nuova data di nascita (yyyy-MM-dd):");
                            studenteDaModificare.DataNascita = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Nuovo IdClasse:");
                            studenteDaModificare.IdClasse = int.Parse(Console.ReadLine());
                            repository.Modify(studenteDaModificare);
                            Console.WriteLine("Studente modificato con successo.");
                        }
                        else
                        {
                            Console.WriteLine("Studente non trovato.");
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
    }

    static void GestisciMaterie(MateriaRepository repository)
    {
        while (true)
        {
            Console.WriteLine("\n--- Gestione Materie ---");
            Console.WriteLine("1. Aggiungi una materia");
            Console.WriteLine("2. Visualizza tutte le materie");
            Console.WriteLine("3. Rimuovi una materia");
            Console.WriteLine("4. Modifica una materia");
            Console.WriteLine("5. Torna al menu principale");
            Console.WriteLine("Inserisci un'opzione:");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("Nome materia:");
                    string nome = Console.ReadLine();
                    var nuovaMateria = new Materia { Nome = nome };
                    repository.Add(nuovaMateria);
                    Console.WriteLine("Materia aggiunta con successo.");
                    break;
                case "2":
                    var materie = repository.GetAll();
                    Console.WriteLine("Elenco delle materie:");
                    foreach (var materia in materie)
                    {
                        Console.WriteLine($"ID: {materia.IdMateria}, Nome: {materia.Nome}");
                    }
                    break;
                case "3":
                    Console.WriteLine("Inserisci l'ID della materia da rimuovere:");
                    if (int.TryParse(Console.ReadLine(), out int idDaRimuovere))
                    {
                        repository.Remove(idDaRimuovere);
                        Console.WriteLine("Materia rimossa con successo.");
                    }
                    else
                    {
                        Console.WriteLine("ID non valido.");
                    }
                    break;
                case "4":
                    Console.WriteLine("Inserisci l'ID della materia da modificare:");
                    if (int.TryParse(Console.ReadLine(), out int idDaModificare))
                    {
                        var materieList = repository.GetAll();
                        var materiaDaModificare = materieList.FirstOrDefault(m => m.IdMateria == idDaModificare);
                        if (materiaDaModificare != null)
                        {
                            Console.WriteLine("Nuovo nome della materia:");
                            materiaDaModificare.Nome = Console.ReadLine();
                            repository.Modify(materiaDaModificare);
                            Console.WriteLine("Materia modificata con successo.");
                        }
                        else
                        {
                            Console.WriteLine("Materia non trovata.");
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
    }

    static void GestisciVoti(VotoRepository repository)
    {
        while (true)
        {
            Console.WriteLine("\n--- Gestione Voti ---");
            Console.WriteLine("1. Aggiungi un voto");
            Console.WriteLine("2. Visualizza tutti i voti");
            Console.WriteLine("3. Rimuovi un voto");
            Console.WriteLine("4. Modifica un voto");
            Console.WriteLine("5. Torna al menu principale");
            Console.WriteLine("Inserisci un'opzione:");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("Valore (decimale):");
                    float valore = float.Parse(Console.ReadLine());
                    Console.WriteLine("StudenteId:");
                    int studenteId = int.Parse(Console.ReadLine());
                    Console.WriteLine("MateriaId:");
                    int materiaId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Data (yyyy-MM-dd):");
                    DateTime data = DateTime.Parse(Console.ReadLine());
                    var nuovoVoto = new Voto { Valore = valore, StudenteId = studenteId, MateriaId = materiaId, Data = data };
                    repository.Add(nuovoVoto);
                    Console.WriteLine("Voto aggiunto con successo.");
                    break;
                case "2":
                    var voti = repository.GetAll();
                    Console.WriteLine("Elenco dei voti:");
                    foreach (var voto in voti)
                    {
                        Console.WriteLine($"ID: {voto.Id}, Valore: {voto.Valore}, StudenteId: {voto.StudenteId}, MateriaId: {voto.MateriaId}, Data: {voto.Data:yyyy-MM-dd}");
                    }
                    break;
                case "3":
                    Console.WriteLine("Inserisci l'ID del voto da rimuovere:");
                    if (int.TryParse(Console.ReadLine(), out int idDaRimuovere))
                    {
                        repository.Remove(idDaRimuovere);
                        Console.WriteLine("Voto rimosso con successo.");
                    }
                    else
                    {
                        Console.WriteLine("ID non valido.");
                    }
                    break;
                case "4":
                    Console.WriteLine("Inserisci l'ID del voto da modificare:");
                    if (int.TryParse(Console.ReadLine(), out int idDaModificare))
                    {
                        var votoDaModificare = repository.GetById(idDaModificare);
                        if (votoDaModificare != null)
                        {
                            Console.WriteLine("Nuovo valore (decimale):");
                            votoDaModificare.Valore = float.Parse(Console.ReadLine());
                            Console.WriteLine("Nuovo StudenteId:");
                            votoDaModificare.StudenteId = int.Parse(Console.ReadLine());
                            Console.WriteLine("Nuovo MateriaId:");
                            votoDaModificare.MateriaId = int.Parse(Console.ReadLine());
                            Console.WriteLine("Nuova data (yyyy-MM-dd):");
                            votoDaModificare.Data = DateTime.Parse(Console.ReadLine());
                            repository.Modify(votoDaModificare);
                            Console.WriteLine("Voto modificato con successo.");
                        }
                        else
                        {
                            Console.WriteLine("Voto non trovato.");
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
    }
}