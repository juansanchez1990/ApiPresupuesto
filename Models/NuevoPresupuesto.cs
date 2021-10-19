using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiPresupuesto.Models
{
    public class NuevoPresupuesto
    {

  

        public decimal? Monto { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFinal { get; set; }

        public string Usuario { get; set; }



        public int? Id_Categoria_Presupuesto { get; set; }

       

      
    }
}