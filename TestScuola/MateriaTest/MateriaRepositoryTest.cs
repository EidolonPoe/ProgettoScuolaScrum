using ProgettoScrum;
using ProgettoScrum.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TestScuola
{
    public class MateriaRepositoryTest : IMateriaRepository
    {
        private readonly List<Materia> _materie = new();
        private int _nextId = 1;

        public int Add(Materia materia)
        {
            materia.IdMateria = _nextId++;
            _materie.Add(materia);
            return materia.IdMateria;
        }

        public void Modify(Materia materia)
        {
            var existing = _materie.FirstOrDefault(m => m.IdMateria == materia.IdMateria);
            if (existing != null)
            {
                existing.Nome = materia.Nome;
            }
        }

        public void Remove(int idMateria)
        {
            var materia = _materie.FirstOrDefault(m => m.IdMateria == idMateria);
            if (materia != null)
            {
                _materie.Remove(materia);
            }
        }

        public List<Materia> GetAll()
        {
            // Ritorna una copia per evitare modifiche esterne
            return new List<Materia>(_materie);
        }
    }
}
