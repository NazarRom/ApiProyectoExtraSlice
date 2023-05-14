using ApiProyectoExtraSlice.Repository;
using ExtraSliceV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCApiExtraSlice.Models;
using Newtonsoft.Json;
using System.Security.Claims;

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

        //finalizar pedido
        [HttpPost]
        [Authorize]
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
      
    }
}
