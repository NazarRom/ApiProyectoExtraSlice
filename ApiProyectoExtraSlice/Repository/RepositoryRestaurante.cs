using ApiProyectoExtraSlice.Data;
using ApiProyectoExtraSlice.Helpers;
using ApiProyectoExtraSlice.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiProyectoExtraSlice.Repository
{
    public class RepositoryRestaurante
    {
        private RestauranteContext context;
        public RepositoryRestaurante(RestauranteContext context)
        {
            this.context = context;
        }
        //para devolcer todos los restaurantes
        public async Task<List<Restaurante>> GetRestaurantesAsync()
        {
            var consulta = from data in this.context.Restaurantes
                           select data;
            return await consulta.ToListAsync();
        }
        //para devolver todos los productos
        public async Task<List<Producto>> GetProductosAsync()
        {
            var consulta = from data in this.context.Productos
                           select data;
            return await consulta.ToListAsync();
        }
        //para recuperar los productos con la session
        public async Task<List<Producto>> GetProductosSessionAsync(List<int> ids)
        {
            var consulta = from datos in this.context.Productos
                           where ids.Contains(datos.IdProducto)
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            return await consulta.ToListAsync();
        }

        //para recuperar la lista de los productos de un restaurante
        public async Task<List<Producto>> FindProductosAsync(int idrestaurante)
        {
            var consulta = from data in this.context.Productos
                           where data.IdRestaurante == idrestaurante
                           select data;
            return await consulta.ToListAsync();
        }
        //para encontrar un restaurante en concreto
        public async Task<Restaurante> FindRestauranteAsync(int idrestaunrante)
        {
            return await this.context.Restaurantes.FirstOrDefaultAsync(x => x.IdRestaurante == idrestaunrante);
        }
        //para encontrar un producto en concreto
        public async Task<Producto> FindProductoAsync(int idproducto)
        {
            return await this.context.Productos.FirstOrDefaultAsync(x => x.IdProducto == idproducto);
        }
        //para sacar el maximo de los usuarios
        private int GetMaxIdusuario()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Usuarios.Max(z => z.IdUser) + 1;
            }
        }
        //para registrar al usuario
        public async Task RegisterUserAsync(string nombre, string direccion, string telefono, string email, string pass)
        {


            Usuario user = new Usuario();
            user.IdUser = this.GetMaxIdusuario();
            user.Nombre_cliente = nombre;
            user.Direccion = direccion;
            user.Telefono = telefono;
            user.Email = email;
            user.Salt = HelperCryptograhy.GenerateSalt();
            user.PasswordCifrado = HelperCryptograhy.EncryptPassword(pass, user.Salt);
            user.Password = pass;
            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
        }

        //para comprobar si existe el usuario
        public async Task<Usuario> ExisteUsuarioAsync(string email, string password)
        {
            var consulta = this.context.Usuarios.Where(x => x.Email == email && x.Password == password);
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<Usuario> FindUsuarioAsync(int iduser)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(x => x.IdUser == iduser);
        }

        //crearPedido
        public async Task CrearPedidoAsync(int idcliente)
        {
            string sql = "sp_pedido_clientes @fechahora, @idcliente";
            DateTime fecha = DateTime.Today;
            SqlParameter pamfecha = new SqlParameter("@fechahora", fecha);
            SqlParameter pamidcliente = new SqlParameter("@idcliente", idcliente);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamfecha, pamidcliente);

        }
        //finalizar pedido
        public async Task FinalizarPedidoAsync(int idcliente, List<int> idsproducto, List<int> cantidad)
        {
            await this.CrearPedidoAsync(idcliente);

            for (var i = 0; i < idsproducto.Count(); i++)
            {
                int id = idsproducto[i];
                int cant = cantidad[i];
                string sql = "sp_producto_pedidos @idproducto, @cantidad";
                SqlParameter pamidproducto = new SqlParameter("@idproducto", id);
                SqlParameter pamCantidad = new SqlParameter("cantidad", cant);
                await this.context.Database.ExecuteSqlRawAsync(sql, pamidproducto, pamCantidad);
            }


        }

        //eliminar el ultipo pedido insertado
        public async Task CancelarPedidoAsync()
        {
            string sql = "sp_delete_last";
            await this.context.Database.ExecuteSqlRawAsync(sql);

        }

        /*///////////////////////////////////////////////CATEGORIAS//////////*/
        public async Task<List<Categoria>> GetAllCategoriasAsync()
        {
            return await this.context.Categorias.ToListAsync();
        }



        //VISTAS PARCIALES///////////////////////////////////////////////
        public async Task<List<Restaurante>> FindRestauranteOnCategoriaAsync(int idcategoria)
        {
            var consulta = from data in this.context.Restaurantes
                           where data.Id_categoria == idcategoria
                           select data;
            return await consulta.ToListAsync();
        }
        //Restaurante por dinero
        public async Task<List<Restaurante>> GetRestaurantesByDineroAsync(int cantidad)
        {
            string sql = "sp_filtro_dinero @dinero";
            SqlParameter pamdinero = new SqlParameter("@dinero", cantidad);
            var consulta = this.context.Restaurantes.FromSqlRaw(sql, pamdinero);
            return  await consulta.ToListAsync();
        }
        //Restaurante por nombre
        public async Task<Restaurante> GetRestauranteByNameAsync(string name)
        {
            return await this.context.Restaurantes.FirstOrDefaultAsync(z => z.Nombre_restaurante == name);
        }


    }
}
