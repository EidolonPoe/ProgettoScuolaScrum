using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoScrum.Repositories.Interfaces
{
    public interface IClasseRepository
    {
        void Add(Classe classe);
        List<Classe> GetAll();
        void Remove(int id);
        void Modify(Classe classe);
    }
}
