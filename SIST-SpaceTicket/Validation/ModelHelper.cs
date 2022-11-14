using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SIST_SpaceTicket.Validation
{
    public class ModelHelper
    {
        public static String GetModelError(ModelStateDictionary pModelStateDictionary)
        {
            StringBuilder errors = new StringBuilder();
            if (!pModelStateDictionary.IsValid)
            {
                var v = pModelStateDictionary.Values;
                foreach (var item in v)
                {
                    foreach (var error in item.Errors)
                    {
                        errors.Append($"{error.ErrorMessage}");
                    }
                }
                return $"Error de validacion {errors.ToString()}";
            }
            else
            {
                return "";
            }
        }
    }
}