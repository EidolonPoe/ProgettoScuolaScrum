using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoScrum.Repositories.Interfaces
{
    public interface IStudenteRepository
    {
        void Add(Studente studente);
        List<Studente> GetAll();
        void Remove(int id);
        void Modify(Studente studente);
    }
}
