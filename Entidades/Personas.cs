using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPersonas.Entidades
{
    public class Personas
    {
        [Key]
        public int PersonaId { get; set; }
        public string Nombres { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public int CantidadGrupos { get; set; }

        [ForeignKey("RolId")]
        public virtual Roles Rol { get; set; }
    }
}
