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
    public class GruposBLL
    {
        public static bool Guardar(Grupos grupo)
        {
            if (!Existe(grupo.GrupoId))//si no existe insertamos
                return Insertar(grupo);
            else
                return Modificar(grupo);
        }
        private static bool Insertar(Grupos grupo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //Agregar la entidad que se desea insertar al contexto
                contexto.Grupos.Add(grupo);

                foreach (var detalle in grupo.Detalle)
                {
                    detalle.Persona.CantidadGrupos += 1;
                }

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
        private static bool Modificar(Grupos grupo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var grupoAnterior = contexto.Grupos
                    .Where(x => x.GrupoId == grupo.GrupoId)
                    .Include(x => x.Detalle)
                    .ThenInclude(x => x.Persona)
                    .AsNoTracking()
                    .SingleOrDefault();

                //busca la entidad en la base de datos y la elimina
                foreach (var detalle in grupoAnterior.Detalle)
                {
                    detalle.Persona.CantidadGrupos -= 1;
                }

                contexto.Database.ExecuteSqlRaw($"Delete FROM GruposDetalle Where GrupoId={grupo.GrupoId}");

                foreach (var item in grupo.Detalle)
                {
                    item.Persona.CantidadGrupos += 1;
                    contexto.Entry(item).State = EntityState.Added;
                }

                //marcar la entidad como modificada para que el contexto sepa como proceder
                contexto.Entry(grupo).State = EntityState.Modified;
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
                //buscar la entidad que se desea eliminar
                var grupo = GruposBLL.Buscar(id);

                if (grupo != null)
                {
                    //busca la entidad en la base de datos y la elimina
                    foreach (var detalle in grupo.Detalle)
                    {
                        detalle.Persona.CantidadGrupos -= 1;
                    }

                    contexto.Grupos.Remove(grupo); //remover la entidad
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
        public static Grupos Buscar(int id)
        {
            Grupos grupo = new Grupos();
            Contexto contexto = new Contexto();

            try
            {
                grupo = contexto.Grupos.Include(x => x.Detalle)
                    .Where(x => x.GrupoId == id)
                     .Include(x => x.Detalle)
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
            return grupo;
        }
        public static List<Grupos> GetList(Expression<Func<Grupos, bool>> criterio)
        {
            List<Grupos> Lista = new List<Grupos>();
            Contexto contexto = new Contexto();

            try
            {
                //obtener la lista y filtrarla según el criterio recibido por parametro.
                Lista = contexto.Grupos.Where(criterio).ToList();
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
                encontrado = contexto.Grupos.Any(e => e.GrupoId == id);
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
