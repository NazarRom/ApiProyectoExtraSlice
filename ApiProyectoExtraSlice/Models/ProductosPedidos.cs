using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiProyectoExtraSlice.Models
{
    [Table("productos_pedidos")]
    public class ProductosPedidos
    {
        [Key]
        [Column("id_pedido")]
        public int IdPedido { get; set; }
        [Column("id_producto")]
        public int IdProducto { get; set; }
        [Column("cantidad")]
        public int Cantidad { get; set; }
    }
}
