using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoScrum.Repositories.Interfaces
{
    public interface IVotoRepositorycs
    {

        void Add(Voto voto);
        List<Voto> GetAll();
        void Remove(int idVoto);
        void Modify(Voto voto);


    }
}
