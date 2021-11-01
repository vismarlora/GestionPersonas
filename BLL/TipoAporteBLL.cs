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
    public class TipoAporteBLL
    {
        public static bool Guardar(TipoAporte tipoAporte)
        {
            if (!Existe(tipoAporte.TipoAporteId))
            {
                return Insertar(tipoAporte);
            }
            else
            {
                return Modificar(tipoAporte);
            }
        }
        private static bool Insertar(TipoAporte tipoAporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //Agregar la entidad que se desea insertar al contexto
                contexto.TipoAporte.Add(tipoAporte);
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
        public static bool Modificar(TipoAporte tipoAporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(tipoAporte).State = EntityState.Modified;
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
                var tipoAporte = contexto.TipoAporte.Find(id);

                if (tipoAporte != null)
                {
                    contexto.TipoAporte.Remove(tipoAporte);
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
        public static TipoAporte Buscar(int id)
        {
            Contexto contexto = new Contexto();
            TipoAporte tiposAporte;

            try
            {
                tiposAporte = contexto.TipoAporte.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return tiposAporte;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.TipoAporte.Any(t => t.TipoAporteId == id);
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
        public static bool ExisteDescripcion(string descripcion)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.TipoAporte.Any(r => r.Descripcion == descripcion);
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
        public static List<TipoAporte> GetList(Expression<Func<TipoAporte, bool>> criterio)
        {
            List<TipoAporte> lista = new List<TipoAporte>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.TipoAporte.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }

        public static List<TipoAporte> GetTiposAportes()
        {
            List<TipoAporte> lista = new List<TipoAporte>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.TipoAporte.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}
