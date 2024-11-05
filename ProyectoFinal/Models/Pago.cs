using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }

        public Pedido pedido { get; set; }

        [ForeignKey("Pedido")]
        public string Codigo { get; set; }   
    }
}
