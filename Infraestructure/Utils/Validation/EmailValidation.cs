using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infraestructure.Validations
{
    public class EmailValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationcontext)
        {
            if (value != null)
            {
                if (isValidEmail(value.ToString()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("El correo es inválido");
                }
            }
            else
            {
                return new ValidationResult("El valor es requerido.");
            }
        }

        private bool isValidEmail(string Email)
        {
            List<string> listaEmailsNOPermitidos = new List<string>
                (new string[] { "gmail.com" });
            bool flag = false;
            Regex validation = new Regex("^\\S+@\\S+\\.\\S+$");
            if (!validation.IsMatch(Email))
            {
                return flag;
            }

            Parallel.ForEach(listaEmailsNOPermitidos, correo =>
            {
                if (Email.Contains(correo))
                {
                    flag = true;
                }
            });
            return flag;
        }
    }
}
