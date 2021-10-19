namespace ApiPresupuesto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Periodos
    {
        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Desde { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Hasta { get; set; }
    }
}
