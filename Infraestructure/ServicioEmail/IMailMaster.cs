using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.ServicioEmail
{
    public interface IMailMaster
    {
        string EnviarMensaje(string asunto, string cuerpo, string destinatario, string resultado);

        void EnviarMensaje_SinRetorno(string asunto, string cuerpo, string destinatario);

        void EnviarMensajeConArchivosAdjuntos(string asunto, string cuerpo, string destinatario, Attachment PDF, Attachment XML);

        void InicializarComponentesYEnviar(string asunto, string cuerpo, string destinatario);

        void InicializarComponentesYEnviar(string asunto, string cuerpo, string destinatario, Attachment PDF, Attachment XML);

        void EnviarFactura(string destinatario, List<Attachment> ListaAdjuntos, Factura oFactura);
    }
}
