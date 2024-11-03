using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Categoria? categoria { get; set; }

        [ForeignKey("categoriaId")]
        public int CategoriaId { get; set; }

        public float Precio { get; set; }
        public string ImagenUrl { get; set; }
    }
}
