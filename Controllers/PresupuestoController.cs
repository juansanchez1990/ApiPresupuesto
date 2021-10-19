using ApiPresupuesto.ModelLocal;
using ApiPresupuesto.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiPresupuesto.Controllers

{
    public class PresupuestoController : ApiController

    {
        PresupuestosModel data = new PresupuestosModel();
        // GET:     

        [HttpGet]
        [Route("api/Presupuesto/GetCategoriasPorId/{id}")]
        public IHttpActionResult Get(int Id)
        {
            //var Dataconceptos = new SqlCommand("SELECT * FROM dbo.Concepto_Presupuesto WHERE idCategoria = 11");
            var Dataconceptos = data.Concepto_Presupuesto.Where(i => i.idCategoria == Id).ToList();

            return Ok(Dataconceptos);
        }

        [HttpGet]
        [Route("api/Presupuesto/Concepto_PresupuestoPorId/{id}")]
        public IHttpActionResult GetConcepto_PresupuestoPorId(int id)
        {

            var result = data.Concepto_Presupuesto.Where(i => i.idCategoria == id).ToList();

            return Ok(result);
        }






        [HttpPost]
        [Route("api/Presupuesto/InsertPresupuesto")]
        public IHttpActionResult postPresupuestoNuevo(NuevoPresupuesto nuevoPresupuesto)
        {


            Presupuesto_Header presupuesto_Header = new Presupuesto_Header();
            var PresupuestoID = data.Presupuesto_Header.FirstOrDefault(i => i.Id_Categoria_Presupuesto == nuevoPresupuesto.Id_Categoria_Presupuesto);

            if (PresupuestoID != null && PresupuestoID.Activo == "Activo")
            {
                return BadRequest("Este presupuesto ya existe o aun esta activo");
            }

            else
            {
                presupuesto_Header.Limite = nuevoPresupuesto.Monto;
                presupuesto_Header.MontoDisponible = nuevoPresupuesto.Monto;
                presupuesto_Header.Id_Categoria_Presupuesto = nuevoPresupuesto.Id_Categoria_Presupuesto;
                presupuesto_Header.Desde = nuevoPresupuesto.FechaInicio;
                presupuesto_Header.Hasta = nuevoPresupuesto.FechaFinal;
                presupuesto_Header.Activo = "Activo";
                data.Presupuesto_Header.Add(presupuesto_Header);

                data.SaveChanges();
            }





            return Ok();



        }

        [HttpGet]
        [Route("api/Presupuesto/GetCategoriasPresupuestos")]
        public IHttpActionResult GetCategoriasPresupuestos()
        {

            var Dataconceptos = data.CategoriaPresupuesto.Select(i => i);

            return Ok(Dataconceptos);
        }




        //Funcion que Retorna el detalle del registro de los pagos
        [HttpGet]
        [Route("api/Presupuesto/GetDetallesPorID/{id}")]
        public IHttpActionResult GetDetallesPorID(int id)
        {

            var result = data.Presupuesto_Detalle.Where(i => i.Id_CategoriaConcepto == id).ToList();

            return Ok(result);
        }

        [HttpPost]
        [Route("api/Presupuesto/InsertInfluencer")]
        public IHttpActionResult AddInfluencer(Concepto_Presupuesto Influencer)
        {
            data.Concepto_Presupuesto.Add(Influencer);
          
            data.SaveChanges();
            return Ok();



        }


        [HttpPut]
        [Route("api/Presupuesto/PutConcepto/{id}")]
        public IHttpActionResult ActualizarConcepto(int id, Concepto_Presupuesto concepto)

        {


            var Concepto = data.Concepto_Presupuesto.FirstOrDefault(i => i.id == concepto.id);
            Concepto.idCategoria = concepto.idCategoria;
            Concepto.Nombre = concepto.Nombre;
            Concepto.NombreComercial = concepto.NombreComercial;
            Concepto.Observaciones = concepto.Observaciones;
            Concepto.idCategoria = concepto.idCategoria;



            data.SaveChanges();
            return Ok();
        }


        [HttpDelete]
        [Route("api/Presupuesto/DeleteInfluencer/{id}")]
        public IHttpActionResult DeleteConceptp(int id)
        {

            try
            {
                Concepto_Presupuesto concepto = data.Concepto_Presupuesto.Find(id);
                if (concepto == null)
                {
                    return NotFound();
                }
                //presupuesto.Activo = "Inactivo";
                data.Concepto_Presupuesto.Remove(concepto);
                data.SaveChanges();

                return Ok(concepto);


            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        //Funcion que Retorna el detalle del registro de los presupuestos
        [HttpGet]
        [Route("api/Presupuesto/GetPresupuestosDetalles")]
        public IHttpActionResult GetPresupuestosDetalle()
        {

            //var DataHeader = data.Presupuesto_Header.Select(i => i);


            var DataHeader = data.Presupuesto_Header.Join(data.CategoriaPresupuesto,
                
                header => header.Id_Categoria_Presupuesto,
                categoria => categoria.id,
                (header, categoria) => new {

                    header, categoria

                }
                ).ToList();




            return Ok(DataHeader);
        }



        //Funcion de sumar todos los montos de la tabla Presupuesto Detalle
        [HttpGet]
        [Route("api/Presupuesto/GetDetallesSuma")]
        public IHttpActionResult GetDetallesSuma(int id)
        {
            //var Dataconceptos = data.Presupuesto_Detalle.Where(i => i.Id_Presupuesto_Header == 11).ToList();

            var result = data.Presupuesto_Detalle.Where(i => i.Id_Presupuesto_Header == id).Sum(x => x.Monto);
            //var result = data.Presupuesto_Detalle.Sum(x => x.Monto);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/Presupuesto/GetDetallesSumaporId/{id}")]
        public IHttpActionResult GetDetallesSumaporId(int id)
        {

            var result = data.Presupuesto_Detalle.Where(i => i.Id_CategoriaConcepto == id).Sum(x => x.Monto);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/Presupuesto/GetPresupuestoSumaporId/{id}")]
        public IHttpActionResult GetPresupestoSumaporId(int id)
        {

            var result = data.Presupuesto_Header.Where(i => i.Id_Categoria_Presupuesto == id && i.Activo == "Activo").Sum(x => x.Limite);

            return Ok(result);
        }

        //Funcion para trasladar presupuesto
        [HttpPut]
        [Route("api/Presupuesto/ActualizarMontos/{idSuma}/{idResta}/{monto}/{Nombre}")]
        public IHttpActionResult ActualizarMontos(int idSuma, int idResta, int monto, string Nombre)          
        {
            var PresupuestoSumar = data.Presupuesto_Header.FirstOrDefault(i => i.id == idSuma);
            var PresupuestoRestar = data.Presupuesto_Header.FirstOrDefault(i => i.id == idResta);
            var CategoriaPresupuestoSuma = data.CategoriaPresupuesto.FirstOrDefault(i => i.id == PresupuestoSumar.Id_Categoria_Presupuesto);
            var CategoriaPresupuestoResta = data.CategoriaPresupuesto.FirstOrDefault(i => i.id == PresupuestoRestar.Id_Categoria_Presupuesto);
            if (PresupuestoRestar.Limite > monto && PresupuestoRestar.MontoDisponible >= monto)
            {

                Presupuesto_Detalle presupuestoDetalle = new Presupuesto_Detalle();
                presupuestoDetalle.Monto = monto;
                presupuestoDetalle.Descripcion = CategoriaPresupuestoSuma.Nombre;
                presupuestoDetalle.Observaciones = "Se trasladó la cantidad de" + " "+ monto + " "  + "al presupuesto" + " " + CategoriaPresupuestoSuma.Nombre;
                presupuestoDetalle.Id_Presupuesto_Header = CategoriaPresupuestoResta.id;
                presupuestoDetalle.TipoPago = "Transferencia";
                presupuestoDetalle.Usuario = Nombre;
                presupuestoDetalle.Fecha = DateTime.Now;
                data.Presupuesto_Detalle.Add(presupuestoDetalle);


                PresupuestoRestar.MontoDisponible = PresupuestoRestar.MontoDisponible - monto;
                PresupuestoSumar.Limite = PresupuestoSumar.Limite + monto;
                PresupuestoSumar.MontoDisponible = PresupuestoSumar.MontoDisponible + monto;
                data.SaveChanges();

            }
            else
            {
                return BadRequest("El monto a trasladar es menor al limite del presupuesto");
            }
            return Ok();
        }



        [HttpGet]
        [Route("api/Presupuesto/EnviarPresupuestoPorID/{id}")]
        public IHttpActionResult IfMontoMayorAPresupuesto(int id) {

            var idHeader = data.Presupuesto_Header.FirstOrDefault(i => i.Id_Categoria_Presupuesto == id);
            var result = data.Presupuesto_Header.Where(i => i.Id_Categoria_Presupuesto == id && i.Activo == "Activo");

            return Ok(result);

        }

        [HttpGet]
        [Route("api/Presupuesto/GetPagosPendientes/{id}")]
        public IHttpActionResult PagosPendientes(int id)
        {
            var result = data.Database.SqlQuery<PagoPendiente>("exec PagosPendientes " + id);
            return Ok(result); ;


        }

        [HttpGet]
        [Route("api/Presupuesto/GetReporteGastos/{id}/{FechaDesde}/{FechaHasta}")]
        public IHttpActionResult ReporteGastos(int id, string FechaDesde, string FechaHasta)
        {
            if (id == 21)
            {
                var result = data.Database.SqlQuery<ReporteGastos>("exec [dbo].[ReporteCampania] " + id + ",'" + FechaDesde + "'" + ",'" + FechaHasta + "'").ToList();
                return Ok(result);
            }
            else
            {
                var result = data.Database.SqlQuery<ReporteGastos>("exec [dbo].[ReportePagos] " + id + ",'" + FechaDesde + "'" + ",'" + FechaHasta + "'").ToList();
                return Ok(result);
            }

            #region SQLEntity
            //var result = (from DetallePago in data.Presupuesto_Detalle
            //              join ph in data.Presupuesto_Header on DetallePago.Id_Presupuesto_Header equals ph.id
            //              where DetallePago.Id_CategoriaConcepto == id && DetallePago.Fecha >= FechaDesde && DetallePago.Fecha <= FechaHasta



            //              select new
            //              {

            //                  Monto = DetallePago.Monto,
            //                  Fecha = DetallePago.Fecha,
            //                  Limite = ph.Limite,




            //              }


            //          ).ToList();
            #endregion


  


        }

        [HttpGet]
        [Route("api/Presupuesto/DetallePagosReporte/{id}/{FechaDesde}/{FechaHasta}")]
        public IHttpActionResult DetallePagosReporte(int id, string FechaDesde, string FechaHasta)
        {
            if (id == 21)
            {
                var result = data.Database.SqlQuery<Premios>("exec [dbo].[DetalleEntregaPremios] " + id + ",'" + FechaDesde + "'" + ",'" + FechaHasta + "'").ToList();

                return Ok(result); ;

            }
            else
            {
                var result = data.Database.SqlQuery<Presupuesto_Detalle>("exec [dbo].[DetallePagosMes] " + id + ",'" + FechaDesde + "'" + ",'" + FechaHasta + "'").ToList();
                return Ok(result); ;

            }

            #region SQLEntity
            //var result = (from DetallePago in data.Presupuesto_Detalle
            //              where DetallePago.Id_CategoriaConcepto == id && DetallePago.Fecha >= FechaDesde && DetallePago.Fecha <= FechaHasta


            //              select new
            //              {
            //                  Description = DetallePago.Descripcion,
            //                  Monto = DetallePago.Monto,
            //                  Fecha= DetallePago.Fecha,



            //              }

            //            ).ToList();
            #endregion


        }

        [HttpGet]
        [Route("api/Presupuesto/GetPremiosCampania")]   
        public IHttpActionResult GetPremiosCampania()
        {
            
            var result = data.Database.SqlQuery<PremiosActuales>("exec PremiosCampanias " );
            return Ok(result); ;


        }


        [HttpPut]
        [Route("api/Presupuesto/ActualizarEntregados/{id}")]
        public IHttpActionResult ActualizarPremiosEntregados(int id, Premios Premio)

        {
            var premio = data.Premios.FirstOrDefault(i => i.id == id);
            DateTime startDate = DateTime.Now;
            var PresupuestoHeader = data.Presupuesto_Header.FirstOrDefault(i => i.id == Premio.Id_Presupuesto_Header && i.Hasta >= startDate);

            if (premio.Entregados==0)
            {
                premio.Entregados = Premio.Entregados;
          
            }

            else
            {
                premio.Entregados = premio.Entregados + Premio.Entregados;
            }      
            premio.idCampania = Premio.idCampania;
            premio.Id_Categoria_Presupuesto = Premio.Id_Categoria_Presupuesto;
            premio.Monto = Premio.Monto;
            premio.NombrePremio = Premio.NombrePremio;
            premio.Cantidad = Premio.Cantidad;
            premio.id = Premio.id;
            premio.Id_Presupuesto_Header = Premio.Id_Presupuesto_Header;
            premio.Supermercado = Premio.Supermercado;
            premio.FechaEntrega = startDate;
            var MontoUnitario = premio.Monto / premio.Cantidad;
            var CantidadDebitar = MontoUnitario * premio.Entregados;
            PresupuestoHeader.MontoDisponible = PresupuestoHeader.MontoDisponible - CantidadDebitar;



            data.SaveChanges();
            return Ok();
        }


        [HttpPost]
        [Route("api/Presupuesto/InsertCampania")]
        public IHttpActionResult postCampania(Campañas Campaña)
        {
                    
            var IdPresupuestoHeader = data.Presupuesto_Header.FirstOrDefault(i => i.Id_Categoria_Presupuesto == Campaña.Id_Categoria_Presupuesto && i.Activo == "Activo");
            if (IdPresupuestoHeader == null)
            {
                return BadRequest("El presupuesto para esta campaña aun no se ha creado, por favor establezca un presupuesto para este Concepto");
            }
            else
            {
             
                Campañas campaña = new Campañas();
                campaña.NombreCampania = Campaña.NombreCampania;
                campaña.FechaInicio = Campaña.FechaInicio;
                campaña.FechaFinal = Campaña.FechaFinal;
                campaña.Id_Categoria_Presupuesto = Campaña.Id_Categoria_Presupuesto;
                campaña.Id_Presupuesto_Header = IdPresupuestoHeader.id;
                data.Campañas.Add(campaña);

            }


            data.SaveChanges();
            return Ok();
        }




        [HttpPost]
        [Route("api/Presupuesto/InsertPremios")]
        public IHttpActionResult postPremios( List<Premios> PremiosCampania)
        {
            var MontoActual = data.Premios.Sum(x => x.Monto);
            if (MontoActual == null)
            {
                MontoActual = 0;
            }
           
         
                foreach (Premios nuevoPremio in PremiosCampania)
                {

                DateTime startDate = DateTime.Now;
                var idCampania = data.Campañas.FirstOrDefault(i => i.Id_Categoria_Presupuesto == nuevoPremio.Id_Categoria_Presupuesto && startDate <= i.FechaFinal);
                var IdPresupuestoHeader = data.Presupuesto_Header.FirstOrDefault(i => i.Id_Categoria_Presupuesto == nuevoPremio.Id_Categoria_Presupuesto && (i.Activo == "Activo"||startDate>=i.Hasta));

                Premios premio = new Premios();
       
                    premio.NombrePremio = nuevoPremio.NombrePremio;
                    premio.Monto = nuevoPremio.Monto * nuevoPremio.Cantidad;
                    premio.Cantidad = nuevoPremio.Cantidad;
                    premio.idCampania = idCampania.id;
                    premio.Id_Presupuesto_Header = IdPresupuestoHeader.id;
                    premio.Id_Categoria_Presupuesto = nuevoPremio.Id_Categoria_Presupuesto;
            
                data.Premios.Add(premio);

            }



            data.SaveChanges();


            return Ok();
        }






        [HttpPost]
        [Route("api/Presupuesto/InsertDetalle/{idConcepto}")]
        public IHttpActionResult postPresupuesto(Presupuesto_Detalle detalle, int idConcepto )
        {

            var MontoActual = data.Presupuesto_Detalle.Sum(x => x.Monto);
            if (MontoActual == null)
            {
                MontoActual = 0;
            }
            var MontoSumado = MontoActual + detalle.Monto;
            DateTime startDate = DateTime.Now;
            var Limite = data.Presupuesto_Header.FirstOrDefault(i => i.Id_Categoria_Presupuesto == detalle.Id_Presupuesto_Header && (i.Activo == "Activo" || startDate<=i.Hasta));
            // var DataLimite = data.Presupuesto_Header.Where(i => i.id == 1);

            if (Limite == null)
            {
                return BadRequest("El presupuesto para este pago aun no se ha creado, por favor establezca un presupuesto para este Concepto");
            }

            else if (MontoSumado > Limite.Limite)
            {
                return BadRequest("El valor del pago supera al límite");

            }

            else if (detalle.Monto > Limite.MontoDisponible)
            {
                return BadRequest("El valor del pago supera al disponible");

            }

            else
            {
                Presupuesto_Detalle presupuestoDetalle = new Presupuesto_Detalle();

                var HeaderId = data.Presupuesto_Header.FirstOrDefault(i => i.Id_Categoria_Presupuesto == detalle.Id_Presupuesto_Header && (i.Activo == "Activo"||startDate<=i.Hasta)).id;
                var Nombre = data.Concepto_Presupuesto.FirstOrDefault(i => i.id == idConcepto).Nombre;
                presupuestoDetalle.Descripcion = Nombre;
                presupuestoDetalle.Monto = detalle.Monto;
                var categoriaId = data.Concepto_Presupuesto.FirstOrDefault(i => i.id == idConcepto);
                presupuestoDetalle.Fecha = detalle.Fecha;
                presupuestoDetalle.Usuario = detalle.Usuario;
                presupuestoDetalle.Id_Presupuesto_Header = HeaderId;
                presupuestoDetalle.TipoPago = detalle.TipoPago;
                presupuestoDetalle.Observaciones = detalle.Observaciones;
                presupuestoDetalle.IdConcepto = idConcepto;
                presupuestoDetalle.Id_CategoriaConcepto = categoriaId.idCategoria;

                data.Presupuesto_Detalle.Add(presupuestoDetalle);
                Limite.MontoDisponible = Limite.MontoDisponible - detalle.Monto;

                data.SaveChanges();
                return Ok();

            }
         



        }


        // PUT: api/Presupuesto/5
        [HttpPut]
        [Route("api/Presupuesto/PutPresupuesto/{id}")]
        public IHttpActionResult ActualizarPresupuesto(int id, Presupuesto_Header presupuesto)

        {
            if (id != presupuesto.id)
            {
                return BadRequest();
            }

            var Presupuesto = data.Presupuesto_Header.FirstOrDefault(i => i.id == presupuesto.id);
            Presupuesto.MontoDisponible = Presupuesto.MontoDisponible + (presupuesto.Limite - Presupuesto.Limite);
            Presupuesto.Hasta = presupuesto.Hasta;
            Presupuesto.Desde = presupuesto.Desde;
            Presupuesto.Limite = presupuesto.Limite;

            

            data.SaveChanges();
            return Ok();
        }



      
        [HttpPut]
        [Route("api/Presupuesto/PutPagoInfluencer/{id}/{IdHeader}")]
        public IHttpActionResult PutPagoInfluencer(int id, Presupuesto_Detalle presupuestoDetalle, int IdHeader)

        {
            DateTime startDate = DateTime.Now;

            var HeaderPresupuesto = data.Presupuesto_Header.FirstOrDefault(i => i.Id_Categoria_Presupuesto == IdHeader && (i.Activo=="Activo" || startDate<= i.Hasta));
            var Presupuesto = data.Presupuesto_Detalle.FirstOrDefault(i => i.id == presupuestoDetalle.id);
            HeaderPresupuesto.MontoDisponible = HeaderPresupuesto.MontoDisponible + (Presupuesto.Monto - presupuestoDetalle.Monto);
            Presupuesto.Descripcion = presupuestoDetalle.Descripcion;
            Presupuesto.Monto = presupuestoDetalle.Monto;
            Presupuesto.Fecha = presupuestoDetalle.Fecha;
            Presupuesto.TipoPago = presupuestoDetalle.TipoPago;

            data.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("api/Presupuesto/DeletePago/{id}/{Monto}/{idHeader}")]
        public IHttpActionResult DeletePagoInfluencer(int id, int monto, int idHeader)
        {

            try
            {
                Presupuesto_Detalle pagoInfluencer = data.Presupuesto_Detalle.Find(id);
                if (pagoInfluencer == null)
                {
                    return NotFound();
                }
                //presupuesto.Activo = "Inactivo";
                var Presupuesto = data.Presupuesto_Header.FirstOrDefault(i => i.id == idHeader && i.Activo=="Activo");
                Presupuesto.MontoDisponible = Presupuesto.MontoDisponible + monto;
                data.Presupuesto_Detalle.Remove(pagoInfluencer);
                data.SaveChanges();

                return Ok(pagoInfluencer);


            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        [HttpDelete]
        [Route("api/Presupuesto/DeletePresupuesto/{id}")]
        public IHttpActionResult DeletePresupuesto(int id)
        {
         
            try
            {
                   Presupuesto_Header presupuesto = data.Presupuesto_Header.Find(id);
                    if (presupuesto == null)
                {
                    return NotFound();
                }
                presupuesto.Activo = "Inactivo";
                //data.Presupuesto_Header.Remove(presupuesto);
                data.SaveChanges();
             
                return Ok(presupuesto);
              

            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}

