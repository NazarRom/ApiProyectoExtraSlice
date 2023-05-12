using ApiProyectoExtraSlice.Models;
using ApiProyectoExtraSlice.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProyectoExtraSlice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private RepositoryRestaurante repo;

        public ProductosController(RepositoryRestaurante repo)
        {
            this.repo = repo;
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
        [Authorize]
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

        //para encontrar un producto en concreto
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Producto>> FindProducto(int id)
        {
            return await this.repo.FindProductoAsync(id);
        }
    }
}
