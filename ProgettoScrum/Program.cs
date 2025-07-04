//using ProgettoScrum.Repositories.Implementations;
//using System.Net.WebSockets;
//using ProgettoScrum.Repositories.Interfaces;

using ProgettoScrum;
using ProgettoScrum.Repositories.Implementations;

public class Program
{
    static void Main(string[] args)
    {

        var connectionString = "Server = localhost\\SQLEXPRESS; Database = DbTest; Trusted_Connection = True;Encrypt=False;";

        var classeRepo = new ClasseRepository(connectionString);
        var studenteRepo = new StudenteRepository(connectionString);
        var materiaRepo = new MateriaRepository(connectionString);
        var votoRepo = new VotoRepository(connectionString);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== MENU PRINCIPALE =====");
            Console.WriteLine("1. Gestione Classi");
            Console.WriteLine("2. Gestione Materie");
            Console.WriteLine("0. Esci");
            Console.Write("Scelta: ");

            var scelta = Console.ReadLine();
            try
            {
                switch (scelta)
                {
                    case "1":
                        GestisciClassi(classeRepo, studenteRepo, votoRepo, materiaRepo);
                        break;
                    case "2":
                        GestisciMaterie(materiaRepo);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Scelta non valida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }

            Console.WriteLine("Premi un tasto per continuare...");
            Console.ReadKey();
        }
    }

    static void GestisciClassi(ClasseRepository classeRepo, StudenteRepository studenteRepo, VotoRepository votoRepo, MateriaRepository materiaRepo)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== GESTIONE CLASSI =====");
            Console.WriteLine("1. Visualizza Classi");
            Console.WriteLine("2. Aggiungi Classe");
            Console.WriteLine("3. Modifica Classe");
            Console.WriteLine("4. Elimina Classe");
            Console.WriteLine("5. Seleziona Classe per Gestione Studenti");
            Console.WriteLine("0. Torna al menu principale");
            Console.Write("Scelta: ");

            var scelta = Console.ReadLine();
            try
            {
                switch (scelta)
                {
                    case "1":
                        var classi = classeRepo.GetAll();
                        if (classi.Count == 0)
                        {
                            Console.WriteLine(" Nessuna classe trovata nel sistema.");
                        }
                        else
                        {
                            foreach (var c in classi)
                                Console.WriteLine($"Id: {c.IdClasse}, Anno: {c.Anno} Sezione: {c.Sezione}");
                        }
                        break;

                    case "2":
                        Console.Write("Anno: ");
                        var anno = int.Parse(Console.ReadLine());
                        Console.Write("Sezione: ");
                        var sezione = Console.ReadLine();
                        classeRepo.Add(new Classe { Anno = anno, Sezione = sezione });
                        Console.WriteLine("Classe aggiunta con successo.");
                        break;
                    case "3":
                        Console.Write("Id Classe da modificare: ");
                        var idMod = int.Parse(Console.ReadLine());
                        Console.Write("Nuovo Anno: ");
                        var annoMod = int.Parse(Console.ReadLine());
                        Console.Write("Nuova Sezione: ");
                        var sezMod = Console.ReadLine();
                        classeRepo.Modify(new Classe { IdClasse = idMod, Anno = annoMod, Sezione = sezMod });
                        Console.WriteLine("Classe modificata.");
                        break;
                    case "4":
                        Console.Write("Id Classe da eliminare: ");
                        var idElim = int.Parse(Console.ReadLine());
                        Console.WriteLine("ATTENZIONE: L'eliminazione della classe comporterà la cancellazione di tutti gli studenti e voti associati. Continuare? (s/n)");
                        var conferma = Console.ReadLine();
                        if (conferma?.ToLower() == "s")
                        {
                            classeRepo.Remove(idElim);
                            Console.WriteLine("Classe eliminata.");
                        }
                        else
                        {
                            Console.WriteLine("Eliminazione annullata.");
                        }
                        break;
                    case "5":
                        Console.Write("Id Classe da selezionare: ");
                        var idSel = int.Parse(Console.ReadLine());
                        var classe = classeRepo.GetById(idSel);
                        if (classe == null)
                        {
                            Console.WriteLine("Classe non trovata.");
                            break;
                        }
                        GestisciStudenti(studenteRepo, votoRepo, materiaRepo, classe);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Scelta non valida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }

            Console.WriteLine("Premi un tasto per continuare...");
            Console.ReadKey();
        }
    }

    static void GestisciMaterie(MateriaRepository materiaRepo)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== GESTIONE MATERIE =====");
            Console.WriteLine("1. Visualizza Materie");
            Console.WriteLine("2. Aggiungi Materia");
            Console.WriteLine("3. Modifica Materia");
            Console.WriteLine("4. Elimina Materia");
            Console.WriteLine("0. Torna al menu principale");
            Console.Write("Scelta: ");

            var scelta = Console.ReadLine();
            try
            {
                switch (scelta)
                {
                    case "1":
                        var materie = materiaRepo.GetAll();
                        if(materie.Count == 0)
                        {
                            Console.WriteLine("Non sono ancora presenti materie nel sistema");
                        }
                        else
                        {
                            foreach (var m in materie)
                                Console.WriteLine($"Id: {m.IdMateria}, Nome: {m.Nome}");

                        }
                        break;
                    case "2":
                        Console.Write("Nome nuova materia: ");
                        var nome = Console.ReadLine();
                        materiaRepo.Add(new Materia { Nome = nome });
                        Console.WriteLine("Materia aggiunta con successo.");
                        break;
                    case "3":
                        Console.Write("Id materia da modificare: ");
                        var idMod = int.Parse(Console.ReadLine());
                        Console.Write("Nuovo nome: ");
                        var nuovoNome = Console.ReadLine();
                        materiaRepo.Modify(new Materia { IdMateria = idMod, Nome = nuovoNome });
                        Console.WriteLine("Materia modificata.");
                        break;
                    case "4":
                        Console.Write("Id materia da eliminare: ");
                        var idElim = int.Parse(Console.ReadLine());
                        Console.WriteLine("ATTENZIONE: L'eliminazione della materia comporterà la cancellazione dei voti associati. Continuare? (s/n)");
                        var conferma = Console.ReadLine();
                        if (conferma?.ToLower() == "s")
                        {
                            materiaRepo.Remove(idElim);
                            Console.WriteLine("Materia eliminata.");
                        }
                        else
                        {
                            Console.WriteLine("Eliminazione annullata.");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Scelta non valida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }

            Console.WriteLine("Premi un tasto per continuare...");
            Console.ReadKey();
        }
    }

    static void GestisciStudenti(StudenteRepository studenteRepo, VotoRepository votoRepo, MateriaRepository materiaRepo, Classe classe)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"===== GESTIONE STUDENTI - Classe {classe.Anno} {classe.Sezione} =====");
            Console.WriteLine("1. Visualizza Studenti");
            Console.WriteLine("2. Aggiungi Studente");
            Console.WriteLine("3. Modifica Studente");
            Console.WriteLine("4. Elimina Studente");
            Console.WriteLine("5. Seleziona Studente per Gestione Voti");
            Console.WriteLine("0. Torna indietro");
            Console.Write("Scelta: ");

            var scelta = Console.ReadLine();
            try
            {
                switch (scelta)
                {
                    case "1":
                        var studenti = studenteRepo.GetByClasse(classe.IdClasse);
                        if (studenti.Count == 0)
                        {
                            Console.WriteLine(" Nessuno studente presente in questa classe.");
                        }
                        else
                        {
                            foreach (var s in studenti)
                                Console.WriteLine($"Id: {s.Id}, Nome: {s.Nome} {s.Cognome}");
                        }
                        break;
                    case "2":
                        Console.Write("Nome: ");
                        var nome = Console.ReadLine();
                        Console.Write("Cognome: ");
                        var cognome = Console.ReadLine();
                        Console.Write("Data di nascita (aaaa-mm-gg): ");
                        var data = DateTime.Parse(Console.ReadLine());
                        studenteRepo.Add(new Studente { Nome = nome, Cognome = cognome, DataNascita = data, IdClasse = classe.IdClasse });
                        Console.WriteLine("Studente aggiunto.");
                        break;
                    case "3":
                        Console.Write("Id studente da modificare: ");
                        var idMod = int.Parse(Console.ReadLine());
                        Console.Write("Nuovo nome: ");
                        var nomeMod = Console.ReadLine();
                        Console.Write("Nuovo cognome: ");
                        var cognomeMod = Console.ReadLine();
                        Console.Write("Nuova data di nascita (aaaa-mm-gg): ");
                        var dataMod = DateTime.Parse(Console.ReadLine());
                        studenteRepo.Modify(new Studente { Id = idMod, Nome = nomeMod, Cognome = cognomeMod, DataNascita = dataMod, IdClasse = classe.IdClasse });
                        Console.WriteLine("Studente modificato.");
                        break;
                    case "4":
                        Console.Write("Id studente da eliminare: ");
                        var idElim = int.Parse(Console.ReadLine());
                        Console.WriteLine("ATTENZIONE: L'eliminazione dello studente comporterà la cancellazione dei voti associati. Continuare? (s/n)");
                        var conferma = Console.ReadLine();
                        if (conferma?.ToLower() == "s")
                        {
                            studenteRepo.Remove(idElim);
                            Console.WriteLine("Studente eliminato.");
                        }
                        else
                        {
                            Console.WriteLine("Eliminazione annullata.");
                        }
                        break;
                    case "5":
                        Console.Write("Id studente da selezionare: ");
                        var idSel = int.Parse(Console.ReadLine());
                        var studente = studenteRepo.GetById(idSel);
                        if (studente == null || studente.IdClasse != classe.IdClasse)
                        {
                            Console.WriteLine("Studente non trovato nella classe selezionata.");
                            break;
                        }
                        GestisciVoti(votoRepo, materiaRepo, studente);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Scelta non valida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }

            Console.WriteLine("Premi un tasto per continuare...");
            Console.ReadKey();
        }
    }

    static void GestisciVoti(VotoRepository votoRepo, MateriaRepository materiaRepo, Studente studente)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"===== GESTIONE VOTI - Studente {studente.Nome} {studente.Cognome} =====");
            Console.WriteLine("1. Visualizza Voti");
            Console.WriteLine("2. Aggiungi Voto");
            Console.WriteLine("3. Modifica Voto");
            Console.WriteLine("4. Elimina Voto");
            Console.WriteLine("0. Torna indietro");
            Console.Write("Scelta: ");

            var scelta = Console.ReadLine();
            try
            {
                switch (scelta)
                {
                    case "1":
                        var voti = votoRepo.GetVotiConMateriaPerStudente(studente.Id);
                        if (voti.Count == 0)
                        {
                            Console.WriteLine(" Nessun voto assegnato a questo studente.");
                        }
                        else
                        {

                            foreach (var v in voti)
                                Console.WriteLine($"Id: {v.Id}, Valore: {v.Valore}, Materia: {v.NomeMateria}, Data: {v.Data.ToShortDateString()}");

                        }
                        break;
                    case "2":
                        var materie = materiaRepo.GetAll();
                        if (materie.Count == 0)
                        {
                            Console.WriteLine("Non ci sono materie disponibili. Devi prima creare almeno una materia.");
                            Console.WriteLine("Vuoi creare una materia ora? (S/N)");
                            string risposta = Console.ReadLine().ToUpper();
                            if (risposta == "S")
                            {
                                GestisciMaterie(materiaRepo);
                            }
                            return;
                        }
                        else
                        {
                            foreach (var m in materie)
                                Console.WriteLine($"Id: {m.IdMateria}, Nome: {m.Nome}");
                            Console.Write("Id materia: ");
                            var idMateria = int.Parse(Console.ReadLine());
                            Console.Write("Valore voto: ");
                            var valore = float.Parse(Console.ReadLine());
                            Console.Write("Data (aaaa-mm-gg): ");
                            var data = DateTime.Parse(Console.ReadLine());
                            votoRepo.Add(new Voto { MateriaId = idMateria, Valore = valore, Data = data, StudenteId = studente.Id });
                            Console.WriteLine("Voto aggiunto.");

                        }
                        
                        break;
                    case "3":
                        Console.Write("Id voto da modificare: ");
                        var idMod = int.Parse(Console.ReadLine());
                        Console.Write("Nuovo valore: ");
                        var nuovoValore = float.Parse(Console.ReadLine());
                        Console.Write("Nuova data (aaaa-mm-gg): ");
                        var nuovaData = DateTime.Parse(Console.ReadLine());
                        votoRepo.Modify(new Voto { Id = idMod, Valore = nuovoValore, Data = nuovaData, StudenteId = studente.Id });
                        Console.WriteLine("Voto modificato.");
                        break;
                    case "4":
                        Console.Write("Id voto da eliminare: ");
                        var idElim = int.Parse(Console.ReadLine());
                        votoRepo.Remove(idElim);
                        Console.WriteLine("Voto eliminato.");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Scelta non valida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }

            Console.WriteLine("Premi un tasto per continuare...");
            Console.ReadKey();
        }
    }

}
