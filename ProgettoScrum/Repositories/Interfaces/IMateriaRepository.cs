using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoScrum.Repositories.Interfaces
{
    public interface IMateriaRepository
    {
        void Add(Classe classe);
        List<Classe> GetAll();
        void Remove(int idClasse);
        void Modify(Classe classe);
    }
}
