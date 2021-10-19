using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPersonas.Entidades
{
    public class TiposAportes
    {
        public int TipoAporteId { get; set; }
        public string Descripcion { get; set; } 
        public float Meta { get; set; } 
        public float Logrado { get; set; }

    }
}
