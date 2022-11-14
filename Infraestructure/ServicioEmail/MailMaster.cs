using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infraestructure.ServicioEmail
{
    public class MailMaster : IMailMaster
    {
        public void EnviarFactura(string destinatario, List<Attachment> ListaAdjuntos, Factura oFactura)
        {
            try
            {
       
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.To.Add(destinatario);
                msg.Subject = "Facturación " + oFactura.ID;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = true;

                // rutas de las imagenes para la plantilla HTML
                string logoImage = HttpContext.Current.Server.MapPath(@"~/Images/SpaceTicket_Logo.png");
                string semicircle = HttpContext.Current.Server.MapPath(@"~/Images/semicircle.png");
                string semicircle180 = HttpContext.Current.Server.MapPath(@"~/Images/semicircle180.png");

                // se lee la plantilla y se asigna al body del mensaje
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/template.cshtml")))
                {
                    msg.Body = reader.ReadToEnd();
                }

                // Se genera el QR para la plantilla de EMAIL
                String QR = QuickResponse.GenerateQRCodeAPI(oFactura.ID);

                // se setean los "parametros" de la plantilla de email
                msg.Body = msg.Body.Replace("{Cliente}", oFactura.Cliente.Nombre + " " + oFactura.Cliente.Apellido1 + " " + oFactura.Cliente.Apellido2);
                msg.Body = msg.Body.Replace("{CodigoCliente}", oFactura.Cliente.CodigoCliente);
                msg.Body = msg.Body.Replace("{QR}", QR);
                msg.Body = msg.Body.Replace("{fecha}", oFactura.Evento.Fecha.ToShortDateString());
                msg.Body = msg.Body.Replace("{hora}", oFactura.Evento.Hora);
                msg.Body = msg.Body.Replace("{event}", oFactura.Evento.Imagen);
                msg.Body = msg.Body.Replace("{EVTID}", oFactura.Evento.ID);
                msg.Body = msg.Body.Replace("{EVTName}", oFactura.Evento.Descripcion);

                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(msg.Body, null, "text/html");


                // se crean los recursos enlazados con las rutas de las imagenes
                LinkedResource linkedResource = new LinkedResource(logoImage, "image/jpeg");
                linkedResource.ContentId = "imgLogo";
                linkedResource.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                LinkedResource linkedResource3 = new LinkedResource(semicircle, "image/jpeg");
                linkedResource3.ContentId = "imgSemi";
                linkedResource3.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                LinkedResource linkedResource4 = new LinkedResource(semicircle180, "image/jpeg");
                linkedResource4.ContentId = "imgSemi180";
                linkedResource4.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                alternateView.LinkedResources.Add(linkedResource);
                alternateView.LinkedResources.Add(linkedResource3);
                alternateView.LinkedResources.Add(linkedResource4);

                msg.AlternateViews.Add(alternateView);
                
                // Se agrega los attachments
                foreach(var attchament in ListaAdjuntos)
                {
                    msg.Attachments.Add(attchament);
                }

                msg.From = new System.Net.Mail.MailAddress("SoporteSpaceTicket@gmail.com");

                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                cliente.Credentials = new System.Net.NetworkCredential("SoporteSpaceTicket@gmail.com", "qfgjlodxwvvdshow");

                cliente.Port = 587;
                cliente.EnableSsl = true;
                cliente.Host = "smtp.gmail.com";
                cliente.Send(msg);

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                throw;
            }
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
