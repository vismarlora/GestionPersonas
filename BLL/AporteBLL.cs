using GestionPersonas.DAL;
using GestionPersonas.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionPersonas.BLL
{
    public class AporteBLL
    {
        public static bool Guardar(Aportes aporte)
        {
            if (!Existe(aporte.AporteId))
                return Insertar(aporte);
            else
                return Modificar(aporte);
        }
        private static bool Insertar(Aportes aporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();


            try
            {

                contexto.Aportes.Add(aporte);

                foreach (var detalle in aporte.AporteDetalle)
                {
                    contexto.Entry(detalle.TipoAporte).State = EntityState.Modified;

                    contexto.Entry(detalle.Persona).State = EntityState.Modified;

                    detalle.TipoAporte.MetaMonto += aporte.Monto;

                    detalle.Persona.TotalAportado += detalle.Monto;
                }

                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        private static bool Modificar(Aportes aporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var aporteAnterior = contexto.Aportes
                    .Where(x => x.AporteId == aporte.AporteId)
                    .Include(x => x.AporteDetalle)
                    .ThenInclude(x => x.Persona)
                    .AsNoTracking()
                    .SingleOrDefault();


                foreach (var detalle in aporteAnterior.AporteDetalle)
                {
                    contexto.Entry(detalle.TipoAporte).State = EntityState.Modified;

                    contexto.Entry(detalle.Persona).State = EntityState.Modified;

                    detalle.Persona.TotalAportado -= aporte.Monto;

                    detalle.TipoAporte.MetaMonto -= detalle.Monto;
                }

                contexto.Database.ExecuteSqlRaw($"Delete FROM AporteDetalle Where Id={aporte.AporteId}");

                foreach (var item in aporte.AporteDetalle)
                {
                    contexto.Entry(item.TipoAporte).State = EntityState.Modified;

                    contexto.Entry(item.Persona).State = EntityState.Modified;

                    item.Persona.TotalAportado += aporte.Monto;

                    item.TipoAporte.MetaMonto += item.Monto;

                    contexto.Entry(item).State = EntityState.Added;
                }


                contexto.Entry(aporte).State = EntityState.Modified;

                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {

                var aporte = AporteBLL.Buscar(id);

                if (aporte != null)
                {

                    foreach (var detalle in aporte.AporteDetalle)
                    {
                        contexto.Entry(detalle.TipoAporte).State = EntityState.Modified;

                        contexto.Entry(detalle.Persona).State = EntityState.Modified;

                        detalle.Persona.TotalAportado -= aporte.Monto;

                        detalle.TipoAporte.MetaMonto -= detalle.Monto;
                    }

                    contexto.Aportes.Remove(aporte);

                    paso = contexto.SaveChanges() > 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static Aportes Buscar(int id)
        {
            Aportes aporte = new Aportes();

            Contexto contexto = new Contexto();

            try
            {
                aporte = contexto.Aportes.Include(x => x.AporteDetalle)
                    .Where(x => x.AporteId == id)
                    .Include(x => x.AporteDetalle)
                    .ThenInclude(x => x.Persona)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return aporte;
        }
        public static List<Aportes> GetList(Expression<Func<Aportes, bool>> criterio)
        {
            List<Aportes> Lista = new List<Aportes>();

            Contexto contexto = new Contexto();

            try
            {

                Lista = contexto.Aportes.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();

            bool encontrado = false;

            try
            {
                encontrado = contexto.Aportes.Any(e => e.AporteId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
    }
}
