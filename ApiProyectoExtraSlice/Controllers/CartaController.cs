using ApiProyectoExtraSlice.Models;
using ApiProyectoExtraSlice.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiProyectoExtraSlice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaController : ControllerBase
    {
        private RepositoryRestaurante repo;
        public CartaController(RepositoryRestaurante repo)
        {
            this.repo = repo;
        }
        //todos los restaurantes
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Restaurante>>> GetAllResuartentes()
        {
            return await this.repo.GetRestaurantesAsync();
        }

        //todos los productos
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Producto>>> GetAllProductos()
        {
            return await this.repo.GetProductosAsync();
        }

        //no sé si furula
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<List<Producto>>> GetProductosFromSession(List<int> ids)
        {
            return await this.repo.GetProductosSessionAsync(ids);
        }

        //[HttpGet]
        //[Route("[action]")]
        //public async Task<ActionResult<List<Producto>>> GetProductosFromSession([FromQuery(Name = "ids")] string ids)
        //{
        //    var idList = ids.Split(',').Select(int.Parse).ToList();
        //    return await this.repo.GetProductosSessionAsync(idList);
        //}

        //para recuperar la lista de los productos de un restaurante
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<List<Producto>>> FindProductosByRestaurantId(int id)
        {
            return await this.repo.FindProductosAsync(id);
        }

        //para encontrar un restaurante en concreto
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Restaurante>> GetResuartente(int id)
        {
            return await this.repo.FindRestauranteAsync(id);
        }

        //para encontrar un producto en concreto
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Producto>> FindProducto(int id)
        {
            return await this.repo.FindProductoAsync(id);
        }

        //para registrar al usuario
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RegisterUser(Usuario user)
        {
            await this.repo.RegisterUserAsync(user.Nombre_cliente, user.Direccion, user.Telefono, user.Email, user.Password);
            return Ok();
        }

        //para comprobar si existe el usuario
        [HttpGet]
        [Route("[action]/{email}/{password}")]
        public async Task<ActionResult<Usuario>> ExisteUsuario(string email, string password)
        {
            return await this.repo.ExisteUsuarioAsync(email, password);
        }

        //para encontrar un usuario 
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Usuario>> FindUserById(int id)
        {
            return await this.repo.FindUsuarioAsync(id);
        }

        //crearPedido
        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<ActionResult> CrearPedido(int id)
        {
            await this.repo.CrearPedidoAsync(id);
            return Ok();
        }

        //finalizar pedido
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> FinalizarPedido(PedidoFinal pedido)
        {
            await this.repo.FinalizarPedidoAsync(pedido.IdCliente,pedido.IdsProducto, pedido.Cantidad);
            return Ok();
        }

        /*///////////////////////////////////////////////CATEGORIAS//////////*/
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Categoria>>> GetAllCategorias()
        {
            return await this.repo.GetAllCategoriasAsync();
        }

        //Restaurantes por categoria///////////////////////////////////////////////
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<List<Restaurante>>> FindRestaurantesByCategoría(int id)
        {
            return await this.repo.FindRestauranteOnCategoriaAsync(id);
        }

        //Restaurante por dinero
        [HttpGet]
        [Route("[action]/{cantidad}")]
        public async Task<ActionResult<List<Restaurante>>> RestaurantesByMoney(int cantidad)
        {
            return await this.repo.GetRestaurantesByDineroAsync(cantidad);
        }

        //Restaurante por nombre
        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<ActionResult<Restaurante>> RestaurantesByName(string name)
        {
            return await this.repo.GetRestauranteByNameAsync(name);
        }
    }
}
