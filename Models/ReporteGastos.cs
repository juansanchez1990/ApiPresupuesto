using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiPresupuesto.Models
{
    public class ReporteGastos
    {

        public decimal SumaTotal { get; set; }

        public decimal Limite { get; set; }

        [StringLength(50)]
        public string Mes { get; set; }
    }
}