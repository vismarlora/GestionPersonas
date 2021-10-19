using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPersonas.Entidades
{
    public class GruposDetalle
    {
        //Id, GrupoId,PersonaId,Orden
        [Key]
        public int Id { get; set; }
        public int GrupoId { get; set; }
        public int PersonaId { get; set; }
        public string Orden { get; set; }

        [ForeignKey("PersonaId")]
        public Personas Persona { get; set; }


    }
}
