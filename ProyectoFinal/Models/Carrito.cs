using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class Carrito
    {
        [Key]
        public int IdCarrito { get; set; }

        public Cliente? Cliente { get; set; }
        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }

        public Producto? Producto { get; set; }
        [ForeignKey("ProductoId")]
        public int ProductoId { get; set; }

        public int Cantidad { get; set; }


    }
}
