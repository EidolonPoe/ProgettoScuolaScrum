using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoScrum
{
    public class Studente
    {
        public int Id { get; set; }

        public string Nome { get; set; }


        public string Cognome { get; set; }

        public int DataNascita { get; set; }

        public List<Voto> Voti { get; set; }

        public  int  IdClasse { get; set; }

    }
}
