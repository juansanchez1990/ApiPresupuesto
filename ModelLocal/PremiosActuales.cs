using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiPresupuesto.ModelLocal
{
    public class PremiosActuales
    {
        
        public int id { get; set; }
        public string NombrePremio { get; set; }

        public int Cantidad { get; set; }

        public int Entregados { get; set; }

        public int Id_Categoria_Presupuesto { get; set; }

        public int Id_Presupuesto_Header { get; set; }

        public int idCampania { get; set; }

        public Decimal Monto { get; set; }

        public string NombreCampania { get; set; }





    }
}