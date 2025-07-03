using ProgettoScrum;
using ProgettoScrum.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TestScuola
{
    public class MateriaRepositoryTest //: IMateriaRepository
    {
        private readonly List<Materia> _materie = new();

        public void Add(Materia materia)
        {
            _materie.Add(materia);
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
            return new List<Materia>(_materie);
        }
    }
}
