namespace ApiPresupuesto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Presupuesto_Header
    {
        public int id { get; set; }

        [Column(TypeName = "money")]
        public decimal? Limite { get; set; }

        public int? Id_Categoria_Presupuesto { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Desde { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Hasta { get; set; }

        [StringLength(10)]
        public string Activo { get; set; }

        [Column(TypeName = "money")]
        public decimal? MontoDisponible { get; set; }
    }
}
