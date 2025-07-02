using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoScrum.Repositories.Interfaces
{
    public interface IMateriaRepository
    {
        void Add(Materia materia);
        List<Materia> GetAll();
        void Remove(int idMateria);
        void Modify(Materia materia);
    }
}
