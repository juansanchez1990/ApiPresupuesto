namespace ApiPresupuesto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoriaPresupuesto")]
    public partial class CategoriaPresupuesto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public int? deptoId { get; set; }

        [StringLength(500)]
        public string img { get; set; }
    }
}
