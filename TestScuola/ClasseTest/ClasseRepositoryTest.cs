using ProgettoScrum;
using ProgettoScrum.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

public class ClasseRepositoryTest : IClasseRepository
{
    private readonly List<Classe> _classi = new();
    private int _nextId = 1;  // Contatore per simulare Id auto-increment

    public void Add(Classe classe)
    {
        if (classe == null)
            throw new ArgumentNullException(nameof(classe));

        // Assegno un IdClasse unico se non è già settato o è <= 0
        if (classe.IdClasse <= 0)
        {
            classe.IdClasse = _nextId;
            _nextId++;
        }
        else
        {
            // Per sicurezza, aggiorno _nextId se necessario
            if (classe.IdClasse >= _nextId)
                _nextId = classe.IdClasse + 1;
        }

        _classi.Add(new Classe
        {
            IdClasse = classe.IdClasse,
            Anno = classe.Anno,
            Sezione = classe.Sezione
        });
    }

    public List<Classe> GetAll()
    {
        // Restituisco copia per sicurezza (evito modifiche esterne)
        return _classi.Select(c => new Classe
        {
            IdClasse = c.IdClasse,
            Anno = c.Anno,
            Sezione = c.Sezione
        }).ToList();
    }

    public void Modify(Classe classe)
    {
        var existing = _classi.FirstOrDefault(c => c.IdClasse == classe.IdClasse);
        if (existing != null)
        {
            existing.Anno = classe.Anno;
            existing.Sezione = classe.Sezione;
        }
        else
        {
            // opzionale: potresti anche lanciare un’eccezione se non trovato
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

    public Classe? GetById(int idClasse)
    {
        var classe = _classi.FirstOrDefault(c => c.IdClasse == idClasse);
        if (classe == null) return null;

        return new Classe
        {
            IdClasse = classe.IdClasse,
            Anno = classe.Anno,
            Sezione = classe.Sezione
        };
    }
}
