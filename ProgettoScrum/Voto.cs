using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoScrum;

public class Voto
{
    public int Id { get; set; }
    public float Valore { get; set; }
    public int StudenteId { get; set; }
    public int MateriaId { get; set; }
    public DateTime Data { get; set; }
}
