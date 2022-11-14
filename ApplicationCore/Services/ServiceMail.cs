using Infraestructure.Models.Catalogo;
using Infraestructure.ServicioEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMail : IServiceMail
    {
        private IMailMaster _MailMaster = new MailMaster();
        public void EnviarFactura(string destinatario, List<Attachment> ListaAdjuntos, Factura oFactura)
        {
            _MailMaster.EnviarFactura(destinatario, ListaAdjuntos, oFactura);
        }

        public string EnviarMensaje(string asunto, string cuerpo, string destinatario, string resultado)
        {
            throw new NotImplementedException();
        }

        public void EnviarMensajeConArchivosAdjuntos(string asunto, string cuerpo, string destinatario, Attachment PDF, Attachment XML)
        {
            throw new NotImplementedException();
        }

        public void EnviarMensaje_SinRetorno(string asunto, string cuerpo, string destinatario)
        {
            throw new NotImplementedException();
        }

        public void InicializarComponentesYEnviar(string asunto, string cuerpo, string destinatario)
        {
            throw new NotImplementedException();
        }

        public void InicializarComponentesYEnviar(string asunto, string cuerpo, string destinatario, Attachment PDF, Attachment XML)
        {
            throw new NotImplementedException();
        }
    }
}
