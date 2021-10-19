using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPresupuesto.Models
{
    public class PagoPendiente
    {
        

        [StringLength(50)]
        public string Nombre { get; set; }

        public int id { get; set; }


    }
}