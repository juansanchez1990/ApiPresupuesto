namespace ApiPresupuesto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Premios
    {
        [StringLength(50)]
        public string NombrePremio { get; set; }

        public int id { get; set; }

        [Column(TypeName = "money")]
        public decimal? Monto { get; set; }

        public int? Cantidad { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaEntrega { get; set; }

        public int? idCampania { get; set; }

        public int Entregados { get; set; }

        public int Supermercado { get; set; }

        public int Id_Categoria_Presupuesto { get; set; }
        public int? Id_Presupuesto_Header { get; set; }
    }
}
