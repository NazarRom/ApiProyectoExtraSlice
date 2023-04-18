using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiProyectoExtraSlice.Models
{
    [Table("pedidos")]
    public class Pedido
    {
        [Key]
        [Column("Key")]
        public int IdPedido { get; set; }
        [Column("fecha_hora")]
        public DateTime Fecha_hora { get; set; }
        [Column("id_cliente")]
        public int IdCliente { get; set; }
    }
}
