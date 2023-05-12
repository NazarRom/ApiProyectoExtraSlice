using ApiProyectoExtraSlice.Models;
using ApiProyectoExtraSlice.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiProyectoExtraSlice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private RepositoryRestaurante repo;

        public UsuariosController(RepositoryRestaurante repo)
        {
            this.repo = repo;
        }

        //para registrar al usuario
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RegisterUser(Usuario user)
        {
            await this.repo.RegisterUserAsync(user.Nombre_cliente, user.Direccion, user.Telefono, user.Email, user.Password);
            return Ok();
        }


        //metodo para el perfil del usuario
        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> PerfilUser()
        {
            //DEBEMOS BUSCAR EL CLAIM DEL Usuario
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
            return usuario;
        }
    }
}
