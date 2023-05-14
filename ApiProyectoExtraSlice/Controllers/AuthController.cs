using ApiProyectoExtraSlice.Helpers;
using ApiProyectoExtraSlice.Repository;
using ExtraSliceV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVCApiExtraSlice.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiProyectoExtraSlice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private RepositoryRestaurante repo;
        private HelperOAuthToken helper;
        public AuthController(RepositoryRestaurante repo, HelperOAuthToken helper)
        {
            this.repo = repo;
            this.helper = helper;
        }
        //necesimaos un metodo para validar a nuetro usuario
        //y devolver el token de acceso
        //dicho metodo siempre debe ser post
        [HttpPost("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            Usuario user = await this.repo.ExisteUsuarioAsync(model.UserName, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                //debemos creare credenciales dentro del token
                SigningCredentials credentials = new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);
                string jsonUser = JsonConvert.SerializeObject(user);
                Claim[] informacion = new[]
                {
                    new Claim("UserData", jsonUser)
                };
                //el token se genera con una clase y debemos indicar 
                //los datos que conforman dicho token
                JwtSecurityToken token = new JwtSecurityToken(
                    claims: informacion,
                    issuer: this.helper.Issuer,
                    audience: this.helper.Audience,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    notBefore: DateTime.UtcNow
                    );
                return Ok(new
                {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        }
    }
}
