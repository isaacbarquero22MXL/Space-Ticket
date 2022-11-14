using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Repository;
using ApplicationCore.Util;

namespace ApplicationCore.Services
{
    public class ServiceEvento : IServiceEvento
    {
        private IRepositoryEvento _RepositoryEvento = new RepositoryEvento();
        private IRepositoryZona _RepositoryZona = new RepositoryZona();
        private IRepositoryLugar _RepositoryLugar = new RepositoryLugar();

        public void DeleteEvento(string id)
        {
            _RepositoryEvento.DeleteEvento(id);
        }

        public bool Existe(string codigo)
        {
            return _RepositoryEvento.Existe(codigo);
        }

        public IEnumerable<Evento> GetAllEvents()
        {
            return _RepositoryEvento.GetAllEvents();
        }

        public Evento GetEventByID(string ID)
        {
            return _RepositoryEvento.GetEventByID((ID));
        }

        public Evento Save(Evento evento)
        {
            bool isNew = _RepositoryEvento.GetEventByID(evento.ID) == null ? true : false;
            Evento oEvento = _RepositoryEvento.Save(evento);
            oEvento.LugarEventoObject.Zona = _RepositoryZona.GetZonasByIDLugarEvento(oEvento.LugarEvento).ToArray(); // obtener zonas del Evento

            if (isNew)  // valida si el evento es nuevo, crea los asientos, si se actualiza, se salta el proceso y retorna el evento
            {
                // Creamos los asientos para todas la Zonas del Lugar del Evento
                foreach (var item in oEvento.LugarEventoObject.Zona) // Obtenemos la zonas del lugar del evento creado
                {
                    for (int i = 1; i <= item.CantFilas; i++) // Obtenemos la cantidad de filas
                    {
                        for (int j = 1; j <= item.CantAsientos; j++) // Obtenemos la cantidad de asientos por fila
                        {
                            Lugar oLugar = new Lugar() // Creamos el asiento o lugar
                            {
                                NumeroAsiento = j.ToString(),
                                Fila = NumeroALetra.GeLetterBynumber(i),
                                IDZona = item.ID,
                                Estado = TypeEstadoAsiento.DISPONIBLE.ToString(),
                                IDEvento = oEvento.ID
                            };

                            _RepositoryLugar.Save(oLugar); // Salvamos el lugar
                        }
                    }
                }
            }
            return oEvento;
        }
    }
}
