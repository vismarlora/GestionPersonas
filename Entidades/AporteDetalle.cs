using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPersonas.Entidades
{
    public class AporteDetalle
    {
        [Key]
        public int IdDetalle { get; set; }
        public int AporteId { get; set; }
        public int TipoAporteId { get; set; }
        public float Monto { get; set; }
        public Personas Persona { get; set; }

        [ForeignKey("TipoAporteId")]
        public TipoAporte TipoAporte { get; set; }
    }
}
