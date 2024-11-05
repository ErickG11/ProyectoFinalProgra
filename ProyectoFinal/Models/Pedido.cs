using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class Pedido
    {

        [Key]
        public string Codigo { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<Producto> producto { get; set; }

    }
}
