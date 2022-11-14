using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIST_SpaceTicket.ViewModel
{
    public class ViewModelDetalleFactura
    {
        public Evento Evento { get; set; }
        public Zona Zona { get; set; }
        public List<Boleto> ListaBoleto { get; set; }
        public List<Lugar> ListaLugares { get; set; }

        [Required]
        public int IDTarjeta { get; set; }
        public double CostoServicios { get => 0.02;}

        public double ValorBoleto()
        {
            return (double)this.Zona.Precio - ValorCostoAdministrativos();
        }

        public double ValorCostoAdministrativos()
        {
            return (double)this.Zona.Precio * CostoServicios;
        }

        public double TotalCompra()
        {
            return (double)this.Zona.Precio * ListaLugares.Count;
        }
    }
}