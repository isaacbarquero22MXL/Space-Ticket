﻿using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryTarjeta
    {
        IEnumerable<TipoTarjeta> GetTipoTarjeta();
        TipoTarjeta GetTipoTarjetaByID(int id);
        void DeleteTipoTarjeta(int id);
        TipoTarjeta Save(TipoTarjeta TipoTarjeta);
    }
}
