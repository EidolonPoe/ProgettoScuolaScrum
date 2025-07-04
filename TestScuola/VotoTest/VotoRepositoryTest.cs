using ProgettoScrum;
using ProgettoScrum.Repositories.Interfaces;
using ProgettoScrum.Repositories.Dto;
using System.Collections.Generic;
using System.Linq;

public class VotoRepositoryTest : IVotoRepository
{
    private readonly List<Voto> _voti = new();
    private int _nextId = 1;

    public void Add(Voto voto)
    {
        voto.Id = _nextId++;
        _voti.Add(voto);
    }

    public List<Voto> GetAll()
    {
        return _voti.ToList();
    }

    public void Remove(int idVoto)
    {
        _voti.RemoveAll(v => v.Id == idVoto);
    }

    public void Modify(Voto voto)
    {
        var index = _voti.FindIndex(v => v.Id == voto.Id);
        if (index != -1)
        {
            _voti[index] = voto;
        }
    }

    public Voto? GetById(int id)
    {
        return _voti.FirstOrDefault(v => v.Id == id);
    }

    public List<VotoMateriaDto> GetVotiConMateriaPerStudente(int studenteId)
    {
        return _voti
            .Where(v => v.StudenteId == studenteId)
            .Select(v => new VotoMateriaDto
            {
                Id = v.Id,
                Valore = v.Valore,
                Data = v.Data,
                StudenteId = v.StudenteId,
                MateriaId = v.MateriaId,
                NomeMateria = $"Materia{v.MateriaId}" // Nome fake per test
            })
            .ToList();
    }
}
