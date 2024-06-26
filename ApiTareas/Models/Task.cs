using System;
using System.Collections.Generic;

namespace ApiTareas.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }
}
