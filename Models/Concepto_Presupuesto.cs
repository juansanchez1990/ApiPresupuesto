namespace ApiPresupuesto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Concepto_Presupuesto
    {
        public int id { get; set; }

        public int? idCategoria { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string NombreComercial { get; set; }

        [StringLength(200)]
        public string Observaciones { get; set; }
    }
}
