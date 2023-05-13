using ApiProyectoExtraSlice.Models;
using ApiProyectoExtraSlice.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProyectoExtraSlice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantesController : ControllerBase
    {
        private RepositoryRestaurante repo;

        public RestaurantesController(RepositoryRestaurante repo)
        {
            this.repo = repo;
        }

        //todos los restaurantes
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Restaurante>>> GetAllRestaurantes()
        {
            return await this.repo.GetRestaurantesAsync();
        }

        //para encontrar un restaurante en concreto
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Restaurante>> GetOneRestaurante(int id)
        {
            return await this.repo.FindRestauranteAsync(id);
        }

        //Restaurantes por categoria///////////////////////////////////////////////
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<List<Restaurante>>> RestaurantesByCategoria(int id)
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
        public async Task<ActionResult<Restaurante>> RestauranteByName(string name)
        {
            return await this.repo.GetRestauranteByNameAsync(name);
        }
    }
}
