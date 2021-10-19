namespace ApiPresupuesto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Presupuesto_Detalle
    {
        public int id { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        [Column(TypeName = "money")]
        public decimal? Monto { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fecha { get; set; }

        [StringLength(50)]
        public string Usuario { get; set; }

        public int? Id_Presupuesto_Header { get; set; }

        [StringLength(50)]
        public string TipoPago { get; set; }

        [StringLength(100)]
        public string Observaciones { get; set; }

        public int? IdConcepto { get; set; }

        public int? Id_CategoriaConcepto { get; set; }
    }
}
