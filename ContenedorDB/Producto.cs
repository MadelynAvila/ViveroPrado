using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContenedorDB
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; } 
        public string Nombre { get; set; } 
        public string Descripcion { get; set; } 
        public double Existencia { get; set; }
        public double Precio { get; set; }

    }
}
