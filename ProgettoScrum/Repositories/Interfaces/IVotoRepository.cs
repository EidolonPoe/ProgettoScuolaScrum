﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgettoScrum.Repositories.Dto;

namespace ProgettoScrum.Repositories.Interfaces
{
    public interface IVotoRepository
    {

        void Add(Voto voto);
        List<Voto> GetAll();
        void Remove(int idVoto);
        void Modify(Voto voto);
        Voto? GetById(int id);
        List<VotoMateriaDto> GetVotiConMateriaPerStudente(int studenteId);


    }
}
