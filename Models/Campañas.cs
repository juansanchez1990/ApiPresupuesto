namespace ApiPresupuesto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Campañas
    {
        public int id { get; set; }

        [StringLength(50)]
        public string NombreCampania { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFinal { get; set; }

        public int? Id_Categoria_Presupuesto { get; set; }

     

        public int? Id_Presupuesto_Header { get; set; }
    }
}
