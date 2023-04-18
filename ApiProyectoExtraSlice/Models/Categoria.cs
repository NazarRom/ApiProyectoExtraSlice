using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiProyectoExtraSlice.Models
{
    [Table("categorias")]
    public class Categoria
    {
        [Key]
        [Column("id")]
        public int IdCategoria { get; set; }
        [Column("nombre_categoria")]
        public string NombreCategoria { get; set; }
        [Column("imagen")]
        public string Imagen { get; set; }
    }
}
