using ProgettoScrum;
using ProgettoScrum.Repositories.Interfaces;

public class StudenteRepositoryTest : IStudenteRepository
{
    private readonly List<Studente> _studenti = new();
    private int _nextId = 1;

    public void Add(Studente studente)
    {
        studente.Id = _nextId++;
        _studenti.Add(studente);
    }

    public List<Studente> GetAll()
    {
        return new List<Studente>(_studenti);
    }

    public void Remove(int id)
    {
        var studente = _studenti.FirstOrDefault(s => s.Id == id);
        if (studente != null)
            _studenti.Remove(studente);
    }

    public void Modify(Studente studente)
    {
        var existing = _studenti.FirstOrDefault(s => s.Id == studente.Id);
        if (existing != null)
        {
            existing.Nome = studente.Nome;
            existing.Cognome = studente.Cognome;
            existing.DataNascita = studente.DataNascita;
            existing.IdClasse = studente.IdClasse;
        }
    }

    public Studente? GetById(int idStudente)
    {
        return _studenti.FirstOrDefault(s => s.Id == idStudente);
    }
}
