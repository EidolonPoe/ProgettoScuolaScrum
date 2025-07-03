using ProgettoScrum;
using ProgettoScrum.Repositories.Interfaces;

public class ClasseRepositoryTest : IClasseRepository
{
    private readonly List<Classe> _classi = new();

    public void Add(Classe classe)
    {
        _classi.Add(classe);
    }

    public List<Classe> GetAll()
    {
        return new List<Classe>(_classi);
    }

    public void Modify(Classe classe)
    {
        var existing = _classi.FirstOrDefault(c => c.IdClasse == classe.IdClasse);
        if (existing != null)
        {
            existing.Anno = classe.Anno;
            existing.Sezione = classe.Sezione;
        }
    }

    public void Remove(int idClasse)
    {
        var classe = _classi.FirstOrDefault(c => c.IdClasse == idClasse);
        if (classe != null)
        {
            _classi.Remove(classe);
        }
    }
}
