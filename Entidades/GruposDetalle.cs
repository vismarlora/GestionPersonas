﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
