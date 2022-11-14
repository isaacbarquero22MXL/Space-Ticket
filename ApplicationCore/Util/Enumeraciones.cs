using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

[JsonConverter(typeof(StringEnumConverter))]
public enum TypeEstadoAsiento
{
    [Display(Name = "DISPONIBLE")]
    DISPONIBLE = 1,
    [Display(Name = "COMPRADO")]
    COMPRADO = 2,
    [Display(Name = "BLOQUEADO")]  //Vista en pantalla
    BLOQUEADO = 3                  //llamado en el aplicativo , valor del llamado
}