using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContenedorDB
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public double Total { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }
    }
}
